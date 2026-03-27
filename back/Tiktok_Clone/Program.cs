using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using Serilog;
using Serilog.Events;
using System.Text;
using Tiktok_Clone.BLL;
using Tiktok_Clone.BLL.Seeder;
using Tiktok_Clone.BLL.Services.Images;
using Tiktok_Clone.BLL.Services.ImageService;
using Tiktok_Clone.BLL.Services.Token;
using Tiktok_Clone.BLL.Services.User;
using Tiktok_Clone.DAL;
using Tiktok_Clone.DAL.Entities.Identity;
using Tiktok_Clone.Middleware;


Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateBootstrapLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog((context, services, configuration) => configuration
        .ReadFrom.Configuration(context.Configuration)
        .ReadFrom.Services(services)
    );

    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                ValidAudience = builder.Configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!
                    ))
            };

            options.Events = new JwtBearerEvents
            {
                OnAuthenticationFailed = context =>
                {
                    Console.WriteLine($"Auth failed: {context.Exception.Message}");
                    return Task.CompletedTask;
                }
            };
        });

    builder.Services.AddAuthorization();

    builder.Services.AddDbContext<AppDbContext>(options =>
    {
        options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
    });

    builder.Services.AddIdentityCore<UserEntity>(options =>
    {
        options.Password.RequiredLength = 6;
        options.Password.RequireDigit = false;
        options.Password.RequireLowercase = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireNonAlphanumeric = false;
        options.User.RequireUniqueEmail = true;
    })
        .AddRoles<RoleEntity>()
        .AddEntityFrameworkStores<AppDbContext>()
        .AddDefaultTokenProviders();


    builder.Services.AddAutoMapper(cfg =>
    {
        cfg.LicenseKey = builder.Configuration.GetConnectionString("AutoMapper");
    }, AppDomain.CurrentDomain.GetAssemblies());

    // Add services to the container.

    builder.Services.AddControllers();

    builder.Services.AddScoped<IUserService, UserService>();
    builder.Services.AddScoped<IImageService, ImageService>();
    builder.Services.AddScoped<IJWTTokenService, JWTTokenService>();



    builder.Services.AddMediatR(opt =>
    {
        opt.RegisterServicesFromAssembly(typeof(AssemblyReference).Assembly);
    });


    builder.Services.AddSwaggerGen(opt =>
    {
        opt.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
        {
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT",
            Description = "JWT Authorization header using the Bearer scheme."
        });

        opt.AddSecurityRequirement(document => new OpenApiSecurityRequirement
        {
            [new OpenApiSecuritySchemeReference("bearer", document)] = []
        });
    });

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Tiktok-Clone");
        });
    }
    app.UseMiddleware<GlobalExceptionHandler>();
    app.UseHttpsRedirection();

    app.UseSerilogRequestLogging();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Database.Migrate();

    try
    {
        await app.SeedDataAsync();
    }
    catch (Exception ex)
    {
        Log.Error(ex, "An error occurred while seeding the database");
    }

    app.Run();

}
catch (Exception ex)
{
    Log.Fatal(ex, "Application failed to start");
}
finally
{
    Log.CloseAndFlush();
}

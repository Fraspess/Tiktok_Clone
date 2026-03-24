using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Tiktok_Clone.BLL.Services.Comment;
using Tiktok_Clone.BLL.Services.HashTag;
using Tiktok_Clone.BLL.Services.Message;
using Tiktok_Clone.BLL.Services.Report;
using Tiktok_Clone.BLL.Services.User;
using Tiktok_Clone.BLL.Services.Video;
using Tiktok_Clone.DAL;
using Tiktok_Clone.DAL.Entities.User;
using Tiktok_Clone.DAL.Repositories;


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

    builder.Services.AddDbContext<AppDbContext>(options =>
    {
        options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
    });

    builder.Services.AddIdentity<UserEntity, RoleEntity>(options =>
    {
        options.Password.RequiredLength = 6;
        options.Password.RequireDigit = false;
        options.Password.RequireLowercase = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireNonAlphanumeric = false;
        options.User.RequireUniqueEmail = true;
    })
        .AddEntityFrameworkStores<AppDbContext>()
        .AddDefaultTokenProviders();

    builder.Services.AddAutoMapper(cfg =>
    {
        cfg.LicenseKey = builder.Configuration.GetConnectionString("AutoMapper");
    }, AppDomain.CurrentDomain.GetAssemblies());

    // Add services to the container.

    builder.Services.AddControllers();

builder.Services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IVideoService, VideoService>();
builder.Services.AddScoped<IMessageService, MessageService>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddScoped<IHashTagService, HashTagService>();

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

    app.UseHttpsRedirection();

    app.UseSerilogRequestLogging();
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

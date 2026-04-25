using Api.DependencyInjection;
using Api.Middleware;
using Application.DependencyInjection;
using Infrastructure.DependencyInjection;
using Infrastructure.SignalR.Hubs;
using Persistence.DependencyInjection;
using Persistence.Seeder;
using Serilog;
using Serilog.Events;

Console.OutputEncoding = System.Text.Encoding.UTF8;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateBootstrapLogger();

try
{
    DotNetEnv.Env.Load("../.env");
    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog((context, services, configuration) => configuration
    .ReadFrom.Configuration(context.Configuration)
    .ReadFrom.Services(services)
    );

    builder.Services.AddApplication(builder.Configuration);
    builder.Services.AddPersistence(builder.Configuration);
    builder.Services.AddInfrastructure(builder.Configuration);
    builder.Services.AddApi(builder.Configuration, builder.Environment);

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

    app.UseSerilogRequestLogging();

    app.UseCors();
    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();
    app.MapHub<ChatHub>("/hubs/chat");

    // also auto-migrates
    await app.SeedDataAsync();

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

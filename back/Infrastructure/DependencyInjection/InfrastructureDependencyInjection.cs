using Application.Interfaces;
using Infrastructure.RabbitMQ;
using Infrastructure.Services.Email;
using Infrastructure.Services.Images;
using Infrastructure.Services.TempStorage;
using Infrastructure.Services.Token;
using Infrastructure.SignalR;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.DependencyInjection
{
    public static class InfrastructureDependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
        this IServiceCollection services, IConfiguration config)
        {
            services.AddSignalR();
            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<IJWTTokenService, JWTTokenService>();
            services.AddScoped<IEmailService, EmailService>();

            services.AddScoped<IJWTTokenService, JWTTokenService>();
            services.AddScoped<IEmailService, EmailService>();

            services.AddScoped<IChatNotifier, ChatNotifier>();
            services.AddScoped<ITempVideoStorage, TempVideoStorage>();
            services.AddScoped(typeof(IEventBus<>), typeof(EventBus<>));

            services.AddMassTransit(x =>
            {
                //x.AddConsumer<VideoProcessedConsumer>();

                x.UsingRabbitMq((ctx, cfg) =>
                {

                    cfg.Host(config["RabbitMQ:HostName"], h =>
                    {
                        h.Username(config["RabbitMQ:UserName"]!);
                        h.Password(config["RabbitMQ:Password"]!);
                    });

                    cfg.ConfigureEndpoints(ctx);
                });
            });
            return services;
        }
    }
}

using Application.Interfaces;
using Contracts;
using Infrastructure.RabbitMQ;
using Infrastructure.RabbitMQ.Consumers;
using Infrastructure.Services.Email;
using Infrastructure.Services.Images;
using Infrastructure.Services.TempVideoStorage;
using Infrastructure.Services.Token;
using Infrastructure.SignalR;
using MassTransit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Infrastructure.DependencyInjection
{
    public static class InfrastructureDependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services, IConfiguration config, IWebHostEnvironment env)
        {
            services.AddSignalR();
            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<IJWTTokenService, JWTTokenService>();
            services.AddScoped<IEmailService, EmailService>();

            services.AddScoped<IJWTTokenService, JWTTokenService>();
            services.AddScoped<IEmailService, EmailService>();

            services.AddScoped<IChatNotifier, ChatNotifier>();
            services.AddScoped<IVideoProcessingNotifier, VideoProcessingNotifier>();
            services.AddScoped<ITempVideoStorage, TempVideoStorage>();
            services.AddScoped(typeof(IEventBus<>), typeof(EventBus<>));

            services.AddMassTransit(x =>
            {
                x.AddConsumer<VideoProcessedConsumer>();
                x.AddConsumer<VideoProcessingProgressConsumer>();
                x.AddConsumer<VideoProcessingFailedConsumer>();

                if (env.IsDevelopment())
                {
                    x.UsingInMemory((ctx, cfg) => { cfg.ConfigureEndpoints(ctx); });
                }
                else
                {
                    x.UsingRabbitMq((ctx, cfg) =>
                    {
                        cfg.Host(config["RabbitMQ:HostName"], h =>
                        {
                            h.Username(config["RabbitMQ:UserName"]!);
                            h.Password(config["RabbitMQ:Password"]!);
                        });

                        cfg.ConfigureEndpoints(ctx);
                    });
                }
            });
            return services;
        }
    }
}
using Application.Behaviors;
using Application.Features.Video.Shared;
using Application.Services.HashTag;
using Application.Services.Message;
using Application.Settings;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Application.DependencyInjection
{
    public static class ApplicationDependencyInjection
    {
        public static IServiceCollection AddApplication(
        this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<IHashTagService, HashTagService>();

            services.Configure<EmailSettings>(config.GetSection("SMTP"));


            services.AddAutoMapper(cfg =>
            {
                cfg.LicenseKey = config["AutoMapper:Key"];
            }, AppDomain.CurrentDomain.GetAssemblies());

            services.AddValidatorsFromAssembly(typeof(AssemblyReference).Assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddMediatR(opt =>
            {
                opt.RegisterServicesFromAssembly(typeof(AssemblyReference).Assembly);
                opt.LicenseKey = config["AutoMapper:Key"];
            });


            services.AddScoped<IDescriptionParser, DescriptionParser>();

            return services;
        }




    }

}

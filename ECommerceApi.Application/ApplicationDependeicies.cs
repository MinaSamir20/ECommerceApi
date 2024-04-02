using ECommerceApi.Application.Service.AuthService;
using ECommerceApi.Application.Service.ImageService;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ECommerceApi.Application
{
    public static class ApplicationDependeicies
    {
        public static IServiceCollection AddApplicationDependeicies(this IServiceCollection services)
        {
            services.AddTransient<AuthService>();
            services.AddScoped<IImageService, ImageService>();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            // Get Validators
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}

using ECommerceApi.Infrastructure.Repositories.CategoryRepo;
using ECommerceApi.Infrastructure.Repositories.GenericRepo;
using ECommerceApi.Infrastructure.Repositories.ProductRepo;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerceApi.Infrastructure
{
    public static class InfrastructureDependencies
    {
        public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services)
        {
            services.AddTransient(typeof(IGenericRepo<>), typeof(GenericRepo<>));
            services.AddTransient<IProductRepo, ProductRepo>();
            services.AddTransient<ICategoryRepo, CategoryRepo>();
            return services;
        }
    }
}

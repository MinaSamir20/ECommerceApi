using ECommerceApi.Domain.Entities;
using ECommerceApi.Infrastructure.Repositories.GenericRepo;

namespace ECommerceApi.Infrastructure.Repositories.ProductRepo
{
    public interface IProductRepo : IGenericRepo<Product>
    {
    }
}

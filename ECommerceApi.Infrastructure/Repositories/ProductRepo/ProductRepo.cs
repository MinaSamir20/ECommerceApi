using ECommerceApi.Domain.Entities;
using ECommerceApi.Infrastructure.Database;
using ECommerceApi.Infrastructure.Repositories.GenericRepo;

namespace ECommerceApi.Infrastructure.Repositories.ProductRepo
{
    public class ProductRepo : GenericRepo<Product>, IProductRepo
    {
        public ProductRepo(AppDbContext db) : base(db) { }
    }
}

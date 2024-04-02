using ECommerceApi.Domain.Entities;
using ECommerceApi.Infrastructure.Database;
using ECommerceApi.Infrastructure.Repositories.GenericRepo;

namespace ECommerceApi.Infrastructure.Repositories.CategoryRepo
{
    public class CategoryRepo : GenericRepo<Category>, ICategoryRepo
    {
        public CategoryRepo(AppDbContext db) : base(db) { }
    }
}

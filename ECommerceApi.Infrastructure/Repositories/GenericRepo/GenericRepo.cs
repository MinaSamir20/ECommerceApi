using ECommerceApi.Domain.Entites;
using ECommerceApi.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ECommerceApi.Infrastructure.Repositories.GenericRepo
{
    public class GenericRepo<T> : IGenericRepo<T> where T : BaseEntity
    {
        private readonly AppDbContext _db;
        public GenericRepo(AppDbContext db)
        {
            _db = db;
        }
        public async Task<string> CreateAsync(T entity)
        {
            await _db.Set<T>().AddAsync(entity);
            await _db.SaveChangesAsync();
            return entity.Id.ToString();
        }
        public async Task<T> UpdateAsync(T entity)
        {
            _db.Entry(entity).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return entity;
        }

        public async Task<string> DeleteAsync(int id)
        {
            var entity = await _db.Set<T>().FindAsync(id);

            if (entity != null)
            {
                entity.IsDeleted = true;
                entity.DeletedDate = DateTime.Now;
                await _db.SaveChangesAsync();
                return "Deleted successfully";
            }
            return "fails";
        }

        public async Task<ICollection<T>> GetAllAsync(string[]? includes = null)
        {
            IQueryable<T> query = _db.Set<T>().Where(a => a.IsDeleted == false);
            if (includes != null)
            {
                foreach (var include in includes)
                    query = query.Include(include);
            }
            return await query.ToListAsync();
        }

        public async Task<ICollection<T>> GetByCriteriaAsync(Expression<Func<T, bool>> criteria, string[]? includes = null)
        {
            IQueryable<T> query = _db.Set<T>().Where(criteria).Where(a => a.IsDeleted == false);
            if (includes is not null)
            {
                foreach (var include in includes)
                    query = query.Include(include);
            }
            return await query.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id, string[]? includes = null)
        {
            IQueryable<T> query = _db.Set<T>().Where(a => a.IsDeleted == false);
            if (includes != null)
            {
                foreach (var include in includes!)
                {
                    query = query.Include(include);
                }
            }
            var entity = await query.FirstOrDefaultAsync(a => a.Id == id);
            return entity!;
        }

    }
}

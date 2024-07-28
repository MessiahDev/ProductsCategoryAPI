using ProductsCategoryAPI.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ProductsCategoryAPI.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly AppDbContext _context;

        public Repository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> predicate)
        {
            var entity = await _context.Set<T>().FirstOrDefaultAsync(predicate);

            if (entity == null)
            {
                throw new InvalidOperationException("Item not found!");
            }

            return entity;
        }

        public async Task CreateAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public Task UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);

            return Task.CompletedTask;
        }

        public Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);

            return Task.CompletedTask;
        }
    }
}
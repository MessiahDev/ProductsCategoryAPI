using ProductsCategoryAPI.Entities;
using System.Linq.Expressions;

namespace ProductsCategoryAPI.Repositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category> GetAsync(Expression<Func<Category, bool>> predicate);
        Task CreateAsync(Category entity);
        Task UpdateAsync(Category entity);
        Task DeleteAsync(Category entity);
    }
}

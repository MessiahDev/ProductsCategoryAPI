using ProductsCategoryAPI.Context;
using ProductsCategoryAPI.Entities;

namespace ProductsCategoryAPI.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {
        }
    }
}

using ProductsCategoryAPI.Context;
using ProductsCategoryAPI.Entities;

namespace ProductsCategoryAPI.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context)
        {
        }
    }
}

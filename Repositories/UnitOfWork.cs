using ProductsCategoryAPI.Context;

namespace ProductsCategoryAPI.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public ICategoryRepository? _categoryRepo;
        public IProductRepository? _productRepo;
        public readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public ICategoryRepository CategoryRepository
        {
            get
            {
                return _categoryRepo = _categoryRepo ?? new CategoryRepository(_context);
            }
        }

        public IProductRepository ProductRepository
        {
            get
            {
                return _productRepo = _productRepo ?? new ProductRepository(_context);
            }
        }

        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

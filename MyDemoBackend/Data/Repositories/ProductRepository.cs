using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.Entities;

namespace Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly DemoBackendDbContext _context;

        private IQueryable<Product> _query;

        public ProductRepository(DemoBackendDbContext context)
        {
            _context = context;
            _query = _context.Product;
        }

        public async Task<Product> GetActiveProductAsync(int id)
        {
            return await _query.AsNoTracking()
                .Where(x => x.IsActive && x.Id == id)
                .SingleOrDefaultAsync();
        }

    }
} 
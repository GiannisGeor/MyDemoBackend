using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.Entities;


namespace Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly DemoBackendDbContext _context;

        private IQueryable<Product> _productQuery;

        public ProductRepository(DemoBackendDbContext context)
        {
            _context = context;
            _productQuery = _context.Product;
        }

        public async Task<List<Product>> GetIdProducts(List<int> requestedProducts)
        {
            return await _productQuery.AsNoTracking()
                .Where(x => x.IsActive && requestedProducts.Contains(x.Id) )
                .ToListAsync();
        }
    }
}

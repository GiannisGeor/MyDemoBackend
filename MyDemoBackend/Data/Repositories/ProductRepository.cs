using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using Models.Projections;

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

        
        public async Task<AllProductDataProjection> GetAllProductData(int id)
        {
            return await _productQuery.AsNoTracking()
                .Where(x => x.Id == id)
                .Include(x => x.ProductOptionsGroups)
                    .ThenInclude(x => x.OptionsGroup)
                .Include(x => x.ProductOptionsGroups)
                    .ThenInclude(x => x.Options)
                    .ThenInclude(x => x.BaseOptions)
                 .Select(x => new AllProductDataProjection
                 {
                     Id = x.Id,
                     Name = x.Name,
                     Price = x.Price,
                     Description = x.Description,
                     ProductOptionsGroups = x.ProductOptionsGroups.Select(p => new ProductOptionsGroupProjection
                     {
                         Id = p.Id,
                         OptionsGroup = new OptionsGroupProjection 
                         { 
                             Id = p.OptionsGroup.Id,
                             Name = p.OptionsGroup.Name,
                             IsMulti = p.OptionsGroup.IsMulti
                         },
                         Options = p.Options.Select(o => new OptionsProjection
                         { 
                             Id = o.Id,
                             ExtraCost = o.ExtraCost,
                             BaseOptions = new BaseOptionsProjection
                             {
                                 Id = o.BaseOptions.Id,
                                 Name = o.BaseOptions.Name,
                                 IsAvailable = o.BaseOptions.IsAvailable
                             }
                         }).ToList(),
                     }).ToList()

                 }).SingleOrDefaultAsync();
                 
        }
    }
}

using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using Models.Projections;

namespace Data.Repositories
{
    public class StoreRepository : IStoreRepository
    {
        private readonly DemoBackendDbContext _context;

        private IQueryable<Store> _storeQuery;

        public StoreRepository(DemoBackendDbContext context)
        {
            _context = context;
            _storeQuery = _context.Store;
        }

        public async Task<List<StoreStoreCategoryAddressProjection>> GetStoreStoreCategoryAddress()
        {
            return await _storeQuery.AsNoTracking()
                .Include(x => x.Address)
                .Include(x => x.StoreCategory)
                .Select(x => new StoreStoreCategoryAddressProjection
                {
                    Id = x.Id,
                    Name = x.Name,
                    IsOpen = x.IsOpen,
                    Address = new AddressProjection
                    {
                        PostalCode = x.Address.PostalCode,
                        FullAddress = x.Address.FullAddress,
                    },
                    StoreCategory = new StoreCategoryProjection
                    {
                        Name = x.StoreCategory.Name
                    }
                }).ToListAsync();
        }

        public async Task<AllInitialDataProjection> GetAllInitialData(int id)
        {
            return await _storeQuery.AsNoTracking()
                .Where(x => x.Id == id)
                .Include(x => x.Address)
                .Include(x => x.StoreCategory)
                .Include(x => x.ProductCategories)
                    .ThenInclude(x => x.Products)
                .Include(x => x.OptionsGroup)
                    .ThenInclude(x => x.Options)
                    .ThenInclude(x => x.BaseOptions)
                .Select(x => new AllInitialDataProjection
                {
                    Id = x.Id,
                    Name = x.Name,
                    IsOpen = x.IsOpen,
                    Address = new AddressProjection
                    {
                        PostalCode = x.Address.PostalCode,
                        FullAddress = x.Address.FullAddress,
                    },
                    StoreCategory = new StoreCategoryProjection
                    {
                        Name = x.StoreCategory.Name
                    },
                    ProductCategories = x.ProductCategories.Select(e => new ProductCategoryProjection
                    {
                        Id = e.Id,
                        Name = e.Name,
                        Products = e.Products.Select(p => new ProductProjection
                        {
                            Id = p.Id,
                            Name = p.Name,
                            IsAvailable = p.IsAvailable,
                            Price = p.Price,
                            Description = p.Description,
                        }).ToList()
                    }).ToList(),
                })
                .SingleOrDefaultAsync();
        }
    }
}
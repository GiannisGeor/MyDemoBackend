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
                        Phone = x.Address.Phone,
                        Type = x.Address.Type
                    },
                    StoreCategory = new StoreCategoryProjection
                    {
                        Name = x.StoreCategory.Name
                    }
                }).ToListAsync();
        }
    }
}

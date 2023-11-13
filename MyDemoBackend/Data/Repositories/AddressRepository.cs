using Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using Services.Interfaces;

namespace Data.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        private readonly DemoBackendDbContext _context;

        private IQueryable<Address> _addressQuery;

        public AddressRepository(DemoBackendDbContext context)
        {
            _context = context;
            _addressQuery = _context.Address;
        }
        public async Task<List<Address>> GetCustomerAddress(int customerId)
        {
            return await _addressQuery.Include(x => x.Order).AsNoTracking()
                .Where(x => x.CustomerId == customerId)
                .ToListAsync();
        }

        public async Task<Address> AddNewAddress(Address candidate)
        {
            await _context.AddAsync(candidate);
            await _context.SaveChangesAsync();
            return candidate;
        }

        public async Task<Address> GetAddressTrackedById(int id)
        {
            return await GetAddressById(id, true);
        }

        public async Task<Address> GetAddressUnTrackedById(int id)
        {
            return await GetAddressById(id);
        }

        private async Task<Address> GetAddressById(int id, bool tracked = false)
        {
            return await _addressQuery.Include(x => x.Order)
                        .AsTrackingIf(tracked)
                        .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Address> EditAddress(Address candidate)
        {
            var entity = await GetAddressTrackedById(candidate.Id);
            entity.FullAddress = candidate.FullAddress;
            entity.PostalCode = candidate.PostalCode;
            entity.Floor = candidate.Floor;
            entity.DoorbellName = candidate.DoorbellName;
            entity.CustomerId = candidate.CustomerId;
            _context.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Address> GetAddressId(int customerId)
        {
            return await _addressQuery.AsNoTracking()
                .Where(x => x.CustomerId == customerId)
                .SingleOrDefaultAsync();
        }

        public async Task<int?> GetCustomerIdByAddressId(int addressId)
        {
            return await _addressQuery.AsNoTracking()
                .Where(x => x.Id == addressId)
                .Select(x => x.CustomerId)
                .FirstOrDefaultAsync();
        }

        public async Task<Address> DeleteAddress(Address candidate)
        {
            var entity = await GetAddressTrackedById(candidate.Id);
            entity.MarkDeleted(candidate.DeletedBy);
            _context.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}

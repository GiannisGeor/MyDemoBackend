using Messages;
using Services.Dtos;

namespace Services.Interfaces
{
    public interface IStoreService
    {

        Task<ListResponse<StoreStoreCategoryAddressDto>> GetStoreStoreCategoryAddress();
    }
}

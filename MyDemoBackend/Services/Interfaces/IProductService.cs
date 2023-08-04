using Messages;
using Services.Dtos;

namespace Services.Interfaces
{
    public interface IProductService
    {
        Task<ObjectResponse<ProductDto>> GetActiveProduct(int id);
    }
}

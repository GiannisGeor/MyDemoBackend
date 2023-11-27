using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Messages;
using Services.Dtos;

namespace Services.Interfaces
{
    public interface IProductService
    {
        Task<ObjectResponse<AllProductDataDto>> GetAllProductData(int id);
    }
}

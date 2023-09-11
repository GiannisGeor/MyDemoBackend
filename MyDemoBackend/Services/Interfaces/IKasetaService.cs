using Messages;
using Services.Dtos;

namespace Services.Interfaces
{
    public interface IKasetaService
    {
        Task<ObjectResponse<List<int>>> GetKasetaId();
        Task<ObjectResponse<List<int>>> GetKasetaIdAscend();
        Task<ObjectResponse<List<int>>> GetKasetaIdDescend();
        Task<ObjectResponse<List<int>>> GetIdDioKaseton();
    }
}


namespace Data.Interfaces
{
    public interface IKasetaRepository
    {
        Task<List<int>> GetKasetaId();
        Task<List<int>> GetKasetaIdAscend();
        Task<List<int>> GetKasetaIdDescend();
        Task<List<int>> GetIdDioKaseton();
    }
}

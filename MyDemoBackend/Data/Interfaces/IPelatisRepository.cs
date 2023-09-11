using Messages;
using Models.Entities;

namespace Data.Interfaces
{
    public interface IPelatisRepository
    {
        Task<List<string>> GetOnomata();
        Task<List<string>> GetTilefona();
        Task<List<Pelatis>> GetPelates();
        Task<List<string>> GetTilefonaMeKodikous();
        Task<List<Tuple<string, List<int>, List<int>>>> GetOnomataIdPelatonKaiTimiKaseton();


    }
}

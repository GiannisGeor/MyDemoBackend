using Messages;
using Microsoft.AspNetCore.Mvc;
using Services.Dtos;

namespace Services.Interfaces
{
    public interface IPelatisService
    {
        Task<ListResponse<string>> GetOnomata();
        Task<ListResponse<string>> GetTilefona();
        Task<ListResponse<PelatisDto>> GetPelates();
        Task<ListResponse<string>> GetTilefonaMeKodikous1();
        Task<ListResponse<string>> GetTilefonaMeKodikous2();
        Task<ListResponse<string>> GetOnomataApoK();
        Task<ListResponse<Tuple<string, List<int>, List<int>>>> GetOnomataIdPelatonKaiTimiKaseton();

    }
}

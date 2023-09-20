using Messages;
using Services.Dtos;

namespace Services.Interfaces
{
    public interface ITainiaService
    {
        Task<ObjectResponse<NeaTainiaResponseDto>> NeaTainia(NeaTainiaDto neaTainiaDto);
        Task<ObjectResponse<MetaboliTainiasResponseDto>> MetaboliTainias(int id, MetaboliTainiasDto metaboliTainiasDto);
        Task<ObjectResponse<NeaTainiaKaiSintelestesResponseDto>> NeaTainiaKaiSintelestes(NeaTainiaKaiSintelestesDto neaTainiaKaiSintelestesDto);
        Task<ObjectResponse<ProsthikiSintelestonResponseDto>> ProsthikiSinteleston(int id, ProsthikiSintelestonDto prosthikiSintelestonDto);
    }
}

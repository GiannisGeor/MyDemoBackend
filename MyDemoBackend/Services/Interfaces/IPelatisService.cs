using Messages;
using Services.Dtos;

namespace Services.Interfaces
{
    public interface IPelatisService
    {
        /// <summary>
        /// fernei mia lista apo ola ta onomata olon ton pelaton
        /// </summary>
        /// <returns></returns>
        Task<ListResponse<string>> GetOnomata();

        /// <summary>
        /// fernei mia lista apo ta tilefona olon ton pelaton
        /// </summary>
        /// <returns></returns>
        Task<ListResponse<string>> GetTilefona();

        /// <summary>
        /// fernei mia lista apo ola ta stoixeia ton pelaton
        /// </summary>
        /// <returns></returns>
        Task<ListResponse<PelatisDto>> GetPelates();

        /// <summary>
        /// fernei mia lista apo ta tilefona olon ton pelaton mazi me to prothema '2310'
        /// </summary>
        /// <returns></returns>
        Task<ListResponse<string>> GetTilefonaMeKodikous1();

        /// <summary>
        /// fernei mia lista apo ta tilefona olon ton pelaton mazi me to prothema '2310'
        /// </summary>
        /// <returns></returns>
        Task<ListResponse<string>> GetTilefonaMeKodikous2();

        /// <summary>
        /// fernei mia lista apo ola ta onomata pou arxizoun apo K
        /// </summary>
        /// <returns></returns>
        Task<ListResponse<string>> GetOnomataApoK();

        /// <summary>
        /// fernei mia lista apo onomata pelaton kai ta Ids kai oi times ton kaseton pou exei enoikiasei  
        /// </summary>
        /// <returns></returns>
        Task<ListResponse<StoixeiaPelatiKaiEnoikiasisDto>> GetOnomataIdPelatonKaiTimiKaseton();
    }
}

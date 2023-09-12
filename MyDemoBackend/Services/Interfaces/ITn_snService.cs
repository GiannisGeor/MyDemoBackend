using Messages;
using Services.Dtos;

namespace Services.Interfaces
{
    public interface ITn_snService
    {
        /// <summary>
        /// fernei mia lista apo onomata kai rolous ton anitstoixon sinteleston
        /// </summary>
        /// <returns></returns>
        Task<ListResponse<OnomaKaiRolosSintelestiDto>> GetOnomataRolousSinteleston();

        /// <summary>
        /// fernei mia lista apo Ids tainion opou simetexei o Alfred Hitchcock
        /// </summary>
        /// <returns></returns>
        Task<ObjectResponse<List<int>>> GetTainiaIdAlfred();
    }
}

using Models.Projections;

namespace Data.Interfaces
{
    public interface ITn_snRepository
    {
        /// <summary>
        /// girnaei apo thn basi mia lista apo onomata kai rolous ton anitstoixon sinteleston
        /// </summary>
        /// <returns></returns>
        Task<List<OnomaKaiRolosSintelestiProjection>> GetOnomataRolousSinteleston();

        /// <summary>
        /// girnaei apo thn basi mia lista apo Ids tainion opou simetexei o Alfred Hitchcock
        /// </summary>
        /// <returns></returns>
        Task<List<int>> GetTainiaIdAlfred();
    }
}

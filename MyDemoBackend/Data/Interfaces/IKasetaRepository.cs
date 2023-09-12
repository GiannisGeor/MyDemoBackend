
namespace Data.Interfaces
{
    public interface IKasetaRepository
    {
        /// <summary>
        /// girnaei apo thn basi mia lista apo Id kaseton pou exoun Tipo VHS me diathesimi posotita megaliteri tou 2 h timi megaliteri tou 2
        /// </summary>
        /// <returns></returns>
        Task<List<int>> GetKasetaIdVhs();

        /// <summary>
        /// girnaei apo thn basi mia lista apo Id kaseton taksinomimeni me auksonta tropo os pros thn posotita
        /// </summary>
        /// <returns></returns>
        Task<List<int>> GetKasetaIdAscend();

        /// <summary>
        /// girnaei apo thn basi mia lista apo Id kaseton taksinomimeni me fthinonta tropo os pros thn timi
        /// </summary>
        /// <returns></returns>
        Task<List<int>> GetKasetaIdDescend();

        /// <summary>
        /// girnaei apo thn basi mia lista apo 2 Id kaseton me thn megaliteri posotita
        /// </summary>
        /// <returns></returns>
        Task<List<int>> GetIdDioKaseton();
    }
}

using Messages;

namespace Services.Interfaces
{
    public interface IKasetaService
    {
        /// <summary>
        /// fernei mia lista apo Id kaseton pou exoun Tipo VHS me diathesimi posotita megaliteri tou 2 h timi megaliteri tou 2
        /// </summary>
        /// <returns></returns>
        Task<ObjectResponse<List<int>>> GetKasetaIdVhs();

        /// <summary>
        /// fernei mia lista apo Id kaseton taksinomimeni me auksonta tropo os pros thn posotita
        /// </summary>
        /// <returns></returns>
        Task<ObjectResponse<List<int>>> GetKasetaIdAscend();

        /// <summary>
        /// fernei mia lista apo Id kaseton taksinomimeni me fthinonta tropo os pros thn timi
        /// </summary>
        /// <returns></returns>
        Task<ObjectResponse<List<int>>> GetKasetaIdDescend();

        /// <summary>
        /// fernei mia lista apo 2 Id kaseton me thn megaliteri posotita
        /// </summary>
        /// <returns></returns>
        Task<ObjectResponse<List<int>>> GetIdDioKaseton();
    }
}

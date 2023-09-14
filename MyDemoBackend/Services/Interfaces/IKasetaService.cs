using Messages;
using Services.Dtos;

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

        ///// <summary>
        ///// fernei mia lista apo onomata pelaton kai ta Ids kai oi times ton kaseton pou exei enoikiasei alla kai ta ID kai tis times
        ///// pou den exoun enoikiastei
        ///// </summary>
        ///// <returns></returns>
        //Task<ListResponse<StoixeiaPelatiKaiEnoikiasisDto>> GetOnomataIdPelatonNullKaiTimiKaseton();

        /// <summary>
        /// gyrnaei apo thn basi to Id ths kasetas tipou VHS me megaliteri posotita apo thn kaseta tipou DVDs
        /// </summary>
        /// <returns></returns>
        Task<ObjectResponse<List<int>>> GetIdVhsMegaliterisPosotitas();  
        
        /// <summary>
        /// fernei thn megaliteri timi enoikiasis mias kasetas
        /// </summary>
        /// <returns></returns>
        Task<ValueResponse<decimal>> GetMegistiTimiKasetas();

    }
}

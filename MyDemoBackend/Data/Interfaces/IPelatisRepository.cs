using Models.Entities;
using Models.Projections;

namespace Data.Interfaces
{
    public interface IPelatisRepository
    {
        /// <summary>
        /// gyrnaei apo thn basi mia lista apo ola ta onomata olon ton pelaton
        /// </summary>
        /// <returns></returns>
        Task<List<string>> GetOnomata();

        /// <summary>
        /// gyrnaei apo thn basi mia lista apo ta tilefona olon ton pelaton
        /// </summary>
        /// <returns></returns>
        Task<List<string>> GetTilefona();

        /// <summary>
        /// gyrnaei apo thn basi mia lista apo ola ta stoixeia ton pelaton
        /// </summary>
        /// <returns></returns>
        Task<List<Pelatis>> GetPelates();

        /// <summary>
        /// gyrnaei apo thn basi mia lista apo ta tilefona olon ton pelaton mazi me to prothema '2310'
        /// </summary>
        /// <returns></returns>
        Task<List<string>> GetTilefonaMeKodikous();

        /// <summary>
        /// gyrnaei apo thn basi mia lista apo onomata pelaton kai ta Ids kai oi times ton kaseton pou exei enoikiasei  
        /// </summary>
        /// <returns></returns>
        Task<List<StoixeiaPelatiKaiEnoikiasisProjection>> GetOnomataIdPelatonKaiTimiKaseton();

        /// <summary>
        /// gyrnaei apo thn basi mia lista apo onomata pelaton kai ta Ids kai oi times ton kaseton pou exei enoikiasei  alla kai ta onomata
        /// apo autous pou den exoun enoikiasei
        /// </summary>
        /// <returns></returns>
        Task<List<StoixeiaPelatiKaiEnoikiasisProjection>> GetOnomataIdPelatonKaiTimiKasetonNull();
    }
}

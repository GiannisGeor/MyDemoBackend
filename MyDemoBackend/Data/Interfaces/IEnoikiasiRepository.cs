using Models.Entities;
using Models.Projections;

namespace Data.Interfaces
{
    public interface IEnoikiasiRepository
    {
        /// <summary>
        /// gyrnaei apo thn basi mia lista apo Id Kaseton, Id Pelaton kai tis imerominies opou enoikiastike h kaseta kai
        /// null stis imerominies epistrofis
        /// </summary>
        /// <returns></returns>
        Task<List<Enoikiasi>> GetEnoikiaseisNull();

        /// <summary>
        /// gyrnaei apo thn basi mia lista apo Id Kaseton kai tis imerominies opou epistrafikan oi kasetes
        /// </summary>
        /// <returns></returns>
        Task<List<EpistrofiKasetasProjection>> GetIdKasetonEnoikiasmenon();
    }
}

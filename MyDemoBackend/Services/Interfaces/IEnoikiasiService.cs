using Messages;
using Services.Dtos;

namespace Services.Interfaces
{
    public interface IEnoikiasiService
    {
        /// <summary>
        /// fernei mia lista apo Id Kaseton, Id Pelaton kai tis imerominies opou enoikiastike h kaseta kai
        /// null stis imerominies epistrofis
        /// </summary>
        /// <returns></returns>
        Task<ListResponse<EnoikiasiDto>> GetEnoikiaseisNull();

        /// <summary>
        /// fernei mia lista apo Id Kaseton kai tis imerominies opou epistrafikan oi kasetes
        /// </summary>
        /// <returns></returns>
        Task<ListResponse<EpistrofiKasetasDto>> GetIdKasetonEnoikiasmenon();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Messages;
using Services.Dtos;

namespace Services.Interfaces
{
    public interface IEnoikiasiService
    {
        Task<ListResponse<EnoikiasiDto>> GetEnoikiaseis();
        Task<ObjectResponse<List<Tuple<int, int, int, int>>>> GetIdKasetonEnoikiasmenon();
    }
}

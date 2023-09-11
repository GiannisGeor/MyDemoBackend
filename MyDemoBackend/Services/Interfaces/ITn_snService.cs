using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Messages;

namespace Services.Interfaces
{
    public interface ITn_snService
    {
        Task<ListResponse<Tuple<string ,string>>> GetOnomataRolousSinteleston();
        Task<ObjectResponse<List<int>>> GetTainiaIdAlfred();


    }
}

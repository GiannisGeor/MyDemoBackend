using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface ITn_snRepository
    {
        Task<List<Tuple<string, string>>> GetOnomataRolousSinteleston();
        Task<List<int>> GetTainiaIdAlfred();


    }
}

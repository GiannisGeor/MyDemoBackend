using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Interfaces;
using Messages;
using Microsoft.EntityFrameworkCore;
using Models.Entities;

namespace Data.Repositories
{
    public class Tn_snRepository : ITn_snRepository
    {
        private readonly DemoBackendDbContext _context;

        private IQueryable<Tn_sn> _tn_snQuery;

        public Tn_snRepository(DemoBackendDbContext context)
        {
            _context = context;
            _tn_snQuery = _context.Tn_sn;
        }

        public async Task<List<Tuple<string, string>>> GetOnomataRolousSinteleston()
        {
            return await _tn_snQuery
                .Include(x => x.Sintelestis)
                .Where(x => x.IDSintelesti == x.Sintelestis.Id)
                .Select(x => new Tuple<string, string> 
                (x.Sintelestis.Onoma , x.Rolos))
                .ToListAsync();
        }

        public async Task<List<int>> GetTainiaIdAlfred()

        {
            return await _tn_snQuery
                .Include(x => x.Sintelestis)
                .Where(x => x.IDSintelesti == x.Sintelestis.Id && x.Sintelestis.Onoma == "Alfred Hitchcock")
                .Select(x => x.IDTainias)
                .ToListAsync();
        }
    }
}

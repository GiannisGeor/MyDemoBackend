using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using Models.Projections;

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

        public async Task<List<OnomaKaiRolosSintelestiProjection>> GetOnomataRolousSinteleston()
        {
            return await _tn_snQuery
                .Include(x => x.Sintelestis)
                .Where(x => x.IDSintelesti == x.Sintelestis.Id)
                .Select(x => new OnomaKaiRolosSintelestiProjection
                {
                    OnomaSintelesti = x.Sintelestis.Onoma,
                    RolosSintelesti = x.Rolos
                })
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

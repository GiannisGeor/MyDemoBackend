using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using Models.Projections;

namespace Data.Repositories
{
    public class KasetaRepository : IKasetaRepository
    {
        private readonly DemoBackendDbContext _context;

        private IQueryable<Kaseta> _kasetaQuery;

        public KasetaRepository(DemoBackendDbContext context)
        {
            _context = context;
            _kasetaQuery = _context.Kaseta;
        }
        public async Task<List<int>> GetKasetaIdVhs()
        {
            return await _kasetaQuery
                .AsNoTracking()
                .Where(x => (x.Posotita >= 1 && x.Posotita <= 2)
                            || x.Timi > 2)
                .Select(x => x.Id)
                .ToListAsync();
        }

        public async Task<List<int>> GetKasetaIdAscend()
        {
            return await _kasetaQuery.AsNoTracking()
                .OrderBy(x => x.Posotita)
                .Select(x => x.Id)
                .ToListAsync();
        }

        public async Task<List<int>> GetKasetaIdDescend()
        {
            return await _kasetaQuery.AsNoTracking()
                .OrderByDescending(x => x.Timi)
                .ThenBy(x => x.Posotita)
                .Select(x => x.Id)
                .ToListAsync();
        }

        public async Task<List<int>> GetIdDioKaseton()
        {
            return await _kasetaQuery.AsNoTracking()
                .OrderBy(x => x.Posotita)
                .Select(x => x.Id)
                .Take(2)
                .ToListAsync();
        }

        //public async Task<List<StoixeiaPelatiKaiEnoikiasisProjection>> GetOnomataIdPelatonNullKaiTimiKaseton()
        //{
        //    var test = await _kasetaQuery.AsNoTracking()
        //          .Include(x => x.Enoikiasis.GroupBy(x => x.IDPelati))
        //          .DefaultIfEmpty()
        //          .ToListAsync(); ;

        //    return null;

        //    //return await _kasetaQuery.AsNoTracking()
        //    //      .Include(x => x.Enoikiasis.GroupBy(x=> x.IDPelati))
        //    //      .Select(x => new StoixeiaPelatiKaiEnoikiasisProjection
        //    //      {
        //    //          OnomaPelati = x.Enoikiasis.Select(x => x.Pelatis.Onoma)
        //    //          KasetesTimes = 
        //    //      })
        //    //      .DefaultIfEmpty()
        //    //      .ToListAsync();
        //}


        public async Task<List<int>> GetIdVhsMegaliterisPosotitas()
        {

           return await _kasetaQuery.AsNoTracking()
                .Join(_context.Kaseta,
                a => a.IDTainias,
                b => b.IDTainias,
                (a, b) => new { Α = a, Β = b })
                .Where(pair => pair.Α.Tipos == "VHS" && pair.Β.Tipos == "DVD" && pair.Α.Posotita > pair.Β.Posotita)
                .Select(pair => pair.Α.IDTainias)
                .Distinct()
                .ToListAsync();

        }

        public async Task<decimal> GetMegistiTimiKasetas()
        {

            return await _kasetaQuery.AsNoTracking().MaxAsync(x => x.Timi);

        }



    }
}

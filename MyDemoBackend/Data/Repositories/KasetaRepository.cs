using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.Entities;

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
        public async Task<List<int>> GetKasetaId()
        {
            return await _kasetaQuery
                .AsNoTracking()
                .Where(x => (x.Posotita >= 1 && x.Posotita <= 2) 
                            || x.Timi > 2)
                .Select(x => x.Id)
                .ToListAsync();


            //return await _kasetaQuery
            //    .Include(x => x.Tainia)
            //        .ThenInclude(x => x.TainiesSintelestes)
            //            .ThenInclude(x => x.Sintelestis)
            //    .Include(x => x.Enoikiasis)
            //    .AsNoTracking()
            //    .Where(x => (x.Posotita >= 1 && x.Posotita <= 2)
            //                || x.Timi > 2)
            //    .Select(x => x.Id)
            //    .ToListAsync();
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
    }
}

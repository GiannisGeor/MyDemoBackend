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
    public class EnoikiasiRepository : IEnoikiasiRepository
    {
        private readonly DemoBackendDbContext _context;

        private IQueryable<Enoikiasi> _enoikiasiQuery;

        public EnoikiasiRepository(DemoBackendDbContext context)
        {
            _context = context;
        _enoikiasiQuery = _context.Enoikiasi;
        }
        

        public async Task<List<Enoikiasi>> GetEnoikiaseis()
        {
            return await _enoikiasiQuery.AsNoTracking()
                .Where(x => x.Eos == null)
                .ToListAsync();
        }

        public async Task<List<Tuple<int, int, int, int>>> GetIdKasetonEnoikiasmenon()
        {
            return await _enoikiasiQuery.AsNoTracking()
                .Where(x => x.Eos != null)
                .OrderBy(x => x.Eos)
                .Select(x => new Tuple<int, int, int, int>(
                    x.IDKasetas,
                    x.Eos.Value.Day,
                    x.Eos.Value.Month,
                    x.Eos.Value.Year))
                .ToListAsync();
        }
    }
}

using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using Models.Projections;

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


        public async Task<List<Enoikiasi>> GetEnoikiaseisNull()
        {
            return await _enoikiasiQuery.AsNoTracking()
                .Where(x => x.Eos == null)
                .ToListAsync();
        }

        public async Task<List<EpistrofiKasetasProjection>> GetIdKasetonEnoikiasmenon()
        {

            return await _enoikiasiQuery.AsNoTracking()
                .Where(x => x.Eos != null)
                .OrderBy(x => x.Eos)
                .Select(x => new EpistrofiKasetasProjection
                {
                    IdKasetas = x.IDKasetas,
                    ImerominiaEpistrofis = (DateTime)x.Eos
                })
                .ToListAsync();
        }
    }
}

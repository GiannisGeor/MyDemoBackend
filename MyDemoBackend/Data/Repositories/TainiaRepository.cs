using Data.Extensions;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.Entities;

namespace Data.Repositories
{
    public class TainiaRepository : ITainiaRepository
    {
        private readonly DemoBackendDbContext _context;

        private IQueryable<Tainia> _tainiaQuery;

        public TainiaRepository(DemoBackendDbContext context)
        {
            _context = context;
            _tainiaQuery = _context.Tainia;
        }

        public async Task<Tainia> AddNewTainia(Tainia candidate)
        {
            await _context.AddAsync(candidate);
            await _context.SaveChangesAsync();
            return candidate;
        }

        public async Task<Tainia> MetaboliTainias(Tainia candidate)
        {
            var entity = await GetTainiaTrackedById(candidate.Id);
            entity.Xronia = candidate.Xronia;
            entity.Titlos = candidate.Titlos;
            _context.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Tainia> GetTainiaTrackedById(int id)
        {
            return await GetTainiaById(id, true);
        }

        public async Task<Tainia> GetTainiaUnTrackedById(int id)
        {
            return await GetTainiaById(id);
        }

        private async Task<Tainia> GetTainiaById(int id, bool tracked = false)
        {
            return await _tainiaQuery
                        .AsTrackingIf(tracked)
                        .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Tainia> AddNewTainiaKaiSintelestes(Tainia candidate)
        {
            _context.Add(candidate);
            await _context.SaveChangesAsync();
            return candidate;
        }

        public async Task<Tainia> AddSintelestesSeTainies(Tainia candidate)
        {
            var entity = await GetTainiaTrackedById(candidate.Id);
            entity.TainiesSintelestes = candidate.TainiesSintelestes;
            _context.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}

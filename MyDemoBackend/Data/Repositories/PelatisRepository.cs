using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.Entities;

namespace Data.Repositories
{
    public class PelatisRepository : IPelatisRepository
    {
          private readonly DemoBackendDbContext _context;
          
          private IQueryable<Pelatis> _pelatisQuery;
          
          public PelatisRepository(DemoBackendDbContext context)
          {
              _context = context;
              _pelatisQuery = _context.Pelatis;
          }
          
          public async Task<List<string>> GetOnomata()
          {
              return await _pelatisQuery.AsNoTracking()
                  .Where(x => x.IsActive).Select(x => x.Onoma).ToListAsync();
          }

          public async Task<List<string>> GetTilefona()
          {
              return await _pelatisQuery.AsNoTracking()
                  .Where(x => x.IsActive).Select(x => x.Tilefono).Distinct().ToListAsync();
          }

          public async Task<List<Pelatis>> GetPelates()
          {
            return await _pelatisQuery.AsNoTracking()
                .Where(x => x.IsActive)
                .ToListAsync();
          }

          public async Task<List<string>> GetTilefonaMeKodikous()
          {
              return await _pelatisQuery.AsNoTracking()
                  .Where(x => x.IsActive).Select(x => "2310" + x.Tilefono).ToListAsync();
          }

          public async Task<List<Tuple<string, List<int>, List<int>>>> GetOnomataIdPelatonKaiTimiKaseton()
          {
              return await _pelatisQuery
                  .Include(x => x.Enoikiasis)
                    .ThenInclude(x => x.Kaseta)
                  .Select(x => new Tuple<string, int, int>
                  (x.Onoma 
                  .Select(x.Enoikiasis.Kaseta.Id), x.Enoikiasis.Kaseta.Timi))
                  .ToListAsync();
          }
    }
}



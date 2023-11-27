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
    public class OptionRepository : IOptionRepository
    {
        private readonly DemoBackendDbContext _context;

        private IQueryable<Options> _optionsQuery;

        public OptionRepository(DemoBackendDbContext context)
        {
            _context = context;
            _optionsQuery = _context.Options;
        }

        public async Task<List<Options>> GetIdOptions(List<int> requestedOptions)
        {
            var test = await _optionsQuery.AsNoTracking().SingleOrDefaultAsync(x => x.Id == 1);

            return await _optionsQuery.AsNoTracking()
                .Where(x => x.IsActive && requestedOptions.Contains(x.Id))
                .ToListAsync();
        }
    }
}

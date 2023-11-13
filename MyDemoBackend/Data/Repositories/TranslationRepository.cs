using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.Entities.Translations;

namespace Data.Repositories
{
    public class TranslationRepository : ITranslationRepository
    {
        private readonly DemoBackendDbContext _context;

        private IQueryable<Translations> _query;

        public TranslationRepository(DemoBackendDbContext context)
        {
            _context = context;
            _query = _context.Translations;
        }

        public async Task<IQueryable<Translations>> GetAllTranslations()
        {
            return _query;
        }

        public async Task<Translations> GetTranslation(string key, string languageIdentifier)
        {
            return await _query.AsNoTracking()
                .Where(x => x.Key == key && x.LanguageIdentifier == languageIdentifier)
                .SingleOrDefaultAsync();
        }

    }
}

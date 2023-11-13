using Models.Entities.Translations;

namespace Data.Interfaces
{
    public interface ITranslationRepository
    {

        /// <summary>
        /// Gets the IQueryable of all translations
        /// </summary>
        /// <returns></returns>
        Task<IQueryable<Translations>> GetAllTranslations();

        /// <summary>
        /// Get a specifi translation based on the key and languageidentifier given
        /// </summary>
        /// <param name="key"></param>
        /// <param name="languageIdentifier"></param>
        /// <returns></returns>
        Task<Translations> GetTranslation(string key, string languageIdentifier);

    }
}

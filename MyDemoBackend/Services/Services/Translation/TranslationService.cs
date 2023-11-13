using FluentValidation.Results;
using Common.Configuration;
using Data.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Serilog;
using Services.Interfaces.Translations;

namespace Services.Services.Translation
{
    public class TranslationService : ITranslationService
    {
        private readonly IMemoryCache _cache;
        private readonly ITranslationRepository _translationRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;

        public TranslationService(
            IMemoryCache cache,
            ITranslationRepository translationRepository,
            IHttpContextAccessor httpContextAccessor,
            IConfiguration configuration
            )
        {
            _cache = cache;
            _translationRepository = translationRepository;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }

        public async Task<string> GetTranslatedText(string key)
        {
            var translatedText = String.Empty;
            var languageIdentifierFromRequest = String.Empty;
            try
            {
                languageIdentifierFromRequest = GetLanguageIdentifierFromRequestHeader(_httpContextAccessor?.HttpContext?.Request);

                var translationsFromCacheOrDb = await GetTranslationsFromCache();
                return translationsFromCacheOrDb[new Tuple<string, string>(key, languageIdentifierFromRequest)]; ;
            }
            catch (Exception e)
            {
                Log.Error(e, $"Error on GetTranslatedText for translation key: {key} and language identifier :{languageIdentifierFromRequest}");
                return translatedText;
            }
            return null;
        }

        public async Task<ValidationResult> TranslateValidationResultErrors(ValidationResult validationResult)
        {
            validationResult.Errors.ForEach(async x => x.ErrorMessage = await GetTranslatedText(x.ErrorMessage));
            return validationResult;
        }

        public string GetLanguageIdentifierFromRequestHeader(HttpRequest Request)
        {
            string defaultLanguageIdentifier = _configuration.GetSection("Translations:DefaultLanguageIdentifier").Get<string>();
            var acceptedLanguages = _configuration.GetSection("Translations:AcceptedLanguages").Get<List<string>>();

            // TODO: when we add the user's preference logic, refactor this condition
            if (Request == null)
            {
                return defaultLanguageIdentifier;
            }

            try
            {
                var languageFromHeader = Request.Headers.SingleOrDefault(x => x.Key == GlobalConstants.Localization.RequestLanguageHeaderKey).Value;
                return acceptedLanguages.Contains(languageFromHeader) ? languageFromHeader : defaultLanguageIdentifier;
            }
            catch (Exception e)
            {
                Log.Error(e, $"Error on GetLanguageIdentifierFromRequestHeader");
                return defaultLanguageIdentifier;
            }
            return null;
        }

        /// <summary>
        /// Get the dictionary of translation from cache memory. If memory is empty sets it from database entries.
        /// </summary>
        /// <returns>Translation dictionary(Key: Tupple consists of Translations.key, Translations.LanguageIdentifier, Value: TranslatedText)</returns>
        private async Task<Dictionary<Tuple<string, string>, string>> GetTranslationsFromCache()
        {
            try
            {
                // Try to get translations from cache.
                var translationsDictionary = _cache.Get<Dictionary<Tuple<string, string>, string>>(GlobalConstants.CacheConstants.Translations);

                // If cache is empty refill cache with results
                if (translationsDictionary == null)
                {
                    // Get translation list from database and fill cache(For specific hour) with the list converted to dictionary if there are records on database

                    var allTranslations = await _translationRepository.GetAllTranslations();

                    translationsDictionary = allTranslations.ToDictionary(x => new Tuple<string, string>(x.Key, x.LanguageIdentifier), x => x.TranslatedText);
                    if (translationsDictionary.Count != 0)
                    {
                        _cache.Set<Dictionary<Tuple<string, string>, string>>(GlobalConstants.CacheConstants.Translations, translationsDictionary, TimeSpan.FromHours(Int32.Parse(_configuration["Cache:HourInterval"])));
                        translationsDictionary = _cache.Get<Dictionary<Tuple<string, string>, string>>(GlobalConstants.CacheConstants.Translations);
                    }
                }

                // Return translations dictionary
                return translationsDictionary;
            }
            catch (Exception e)
            {
                Log.Error(e, $"Error on GetTranslationsFromCache");
                return new Dictionary<Tuple<string, string>, string>();
            }
            return null;
        }

    }
}

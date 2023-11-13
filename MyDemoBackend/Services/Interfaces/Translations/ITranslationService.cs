using FluentValidation.Results;
using Microsoft.AspNetCore.Http;

namespace Services.Interfaces.Translations
{
    public interface ITranslationService
    {

        /// <summary>
        /// Get the translatedtext from the cached translation entries. If cache expires resets the cache entries from database.
        /// </summary>
        /// <param name="key">Translation key</param>
        /// <returns>Translation Text based on Key and Request language identifier</returns>
        Task<string> GetTranslatedText(string key);

        /// <summary>
        /// Translates the validationResult object error messages
        /// </summary>
        /// <param name="validationResult"></param>
        /// <returns>The same object with the error message translated</returns>
        Task<ValidationResult> TranslateValidationResultErrors(ValidationResult validationResult);

        /// <summary>
        /// Get the language identifier from the request header
        /// </summary>
        /// <param name="Request"></param>
        /// <returns>language identifier</returns>
        string GetLanguageIdentifierFromRequestHeader(HttpRequest Request);
    }
}

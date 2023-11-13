namespace Models.Entities.Translations
{
    public class Translations
    {
        /// <summary>
        /// Translation Key
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Language identifier sent from frontend
        /// </summary>
        public string LanguageIdentifier { get; set; }

        /// <summary>
        /// Translated Text
        /// </summary>
        public string TranslatedText { get; set; }
    }
}
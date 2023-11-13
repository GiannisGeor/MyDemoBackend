namespace Common.Configuration
{
    public static class GlobalConstants
    {
        public static class Authentication
        {
            public static DateTimeOffset MaxDateTime { get; } = new DateTimeOffset(2038, 1, 1, 0, 0, 0, TimeSpan.FromHours(0));

            public static class Cookies
            {
                public static string RefreshTokenCookie { get; } = "RefreshToken";

            }

            public static class CustomClaims
            {
                public static string TwoFactorEnforced { get; } = "TwoFactorEnforced";
                public static string HasTwoFactorEnabled { get; } = "HasTwoFactorEnabled";
                public static string IsTwoFactorAuthenticated { get; } = "IsTwoFactorAuthenticated";
                public static string Role { get; } = "role";
                public static string Email { get; } = "email";
                public static string RefreshTokenExp { get; } = "refreshtokenexp";
                public static string CustomerId { get; } = "customerId";

            }

            public static class Roles
            {
                public const string Admin = "admin";
                public const string Customer = "customer";

            }

            public static class TwoFactor
            {
                public static string AuthenticatorUriFormat { get; } = "otpauth://totp/{0}:{1}?secret={2}&issuer={0}&digits=6";
                public static string SetupMfaRedirectionPage { get; } = $"/setupMFA";
                public static string MfaAuthenticationRedirectionPage { get; } = $"/mfa";

            }
            public static class ExternalProviders
            {
                public const string AzureAd = "AzureAd";

            }

        }

        public static class CustomErrorCodes
        {
            public static string EmailNotConfirmed = "EMAIL_NOT_CONFIRMED";

        }

        public static class MultiFactorMiddlewareRelatedControllerNames
        {

            public const string LoginController = "login";

            public const string MultiFactorController = "multi-factor";
        }

        public static class Localization
        {
            public static string RequestLanguageHeaderKey { get; } = "Accept-Language";
        }

        public static class CacheConstants
        {
            public static string Translations { get; } = "Translations";
        }

        public static class FolderHandling
        {
            // Assembly folder: \WebClient\bin\Debug\net6.0
            private static string App = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

            // Path to english translated email templates
            public static string EmailTemplatesEn = Path.Combine(App, "EmailTranslations/en");

            // Path to german translated email templates
            public static string EmailTemplatesDe = Path.Combine(App, "EmailTranslations/de");

        }

        public static class Database
        {
            public const string DefaultMarkUser = "Application";
        }
    }
}

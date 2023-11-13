namespace Common.Settings
{
    public class PasswordRequirements
    {
        public string RequireDigit { get; set; }
        public string RequireLowercase { get; set; }
        public string RequireUppercase { get; set; }
        public string RequireNonAlphanumeric { get; set; }
        public string RequiredLength { get; set; }
        public string RequiredUniqueChars { get; set; }
    }
}

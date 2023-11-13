namespace Services.ResponseDtos.Settings
{
    public class PasswordRequirementsResponseDto
    {
        public string RequireDigit { get; set; }
        public string RequireLowercase { get; set; }
        public string RequireUppercase { get; set; }
        public string RequireNonAlphanumeric { get; set; }
        public string RequiredLength { get; set; }
        public string RequiredUniqueChars { get; set; }
    }
}

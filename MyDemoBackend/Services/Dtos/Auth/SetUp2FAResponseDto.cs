namespace Services.ResponseDtos.Auth
{
    public class SetUp2FAResponseDto
    {
        /// <summary>
        /// Jwt token containing login information
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// The code in case of lost device
        /// </summary>
        public string RecoveryCodes { get; set; }

    }
}

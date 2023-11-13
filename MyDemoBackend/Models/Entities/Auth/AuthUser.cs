using Microsoft.AspNetCore.Identity;

namespace Models.Entities.Auth
{
    public class AuthUser : IdentityUser
    {
        /*
              TODO: Check if needed, maybe we can solve it with a special JWT as refresh token, which contains all data
             /// <summary>
             /// The refresh token to load new JWT tokens
             /// </summary>
             public string RefreshToken { get; set; }
            
             /// <summary>
             /// The date on which the refresh token expires
             /// </summary>
             public DateTime RefreshTokenExpiration { get; set; }
         */
    }
}

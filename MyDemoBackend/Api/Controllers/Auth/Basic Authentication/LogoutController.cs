using Common.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Auth.Basic_Authentication
{
    [Route("api/auth/logout")]
    [ApiController]
    public class LogoutController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;

        public LogoutController(IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }

        /// <summary>
        /// Logouts the current logged in user
        /// </summary>
        /// <returns>Admin JWT token and a refresh token</returns>
        [Produces("application/json")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize]
        public ActionResult<string> Logout()
        {
            // Remove auth cookie
            // The reason CookieOptions are needed is, that in the case of SameSiteMode.None, the secure flag has to be set to true, otherwise the browser will reject the cookie.
            // This is because to delete a cookie, the backend overrides the current cookie with a new cookie which is already expired. This means, to delete a cookie
            // with config SameSiteMode.None, we need to explicitly tell the delete process to use secure, otherwise the override cookie cannot be saved.
            var context = _httpContextAccessor.HttpContext;
            context?.Response.Cookies.Delete(GlobalConstants.Authentication.Cookies.RefreshTokenCookie, new CookieOptions()
            {
                HttpOnly = bool.Parse(_configuration["Auth:Cookies:Refresh:HttpOnly"]),
                SameSite = (SameSiteMode)int.Parse(_configuration["Auth:Cookies:Refresh:SameSite"]),
                Secure = bool.Parse(_configuration["Auth:Cookies:Refresh:Secure"]),
            });

            return Ok();
        }
    }
}

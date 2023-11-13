using Common.Configuration;
using Messages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Features;
using System.Text.Json;

namespace Api.Helpers.Middlewares
{
    public class MultiFactorMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        public MultiFactorMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            // Check for 2FA on all Authorized endpoints except login and multi-factor related endpoints
            if (httpContext.Request.Path.HasValue && httpContext.Request.Path.Value.ToLower().StartsWith("/api") 
                && !httpContext.Request.Path.Value.ToLower().Contains(GlobalConstants.MultiFactorMiddlewareRelatedControllerNames.LoginController) 
                && !httpContext.Request.Path.Value.ToLower().Contains(GlobalConstants.MultiFactorMiddlewareRelatedControllerNames.MultiFactorController))
            {
                // Check if route is marked as [Authorize]
                var hasAuthorizeAttribute = httpContext.Features.Get<IEndpointFeature>().Endpoint.Metadata.Any(m => m is AuthorizeAttribute);
                if (hasAuthorizeAttribute)
                {
                    // Check if 2FA is enabled for the this specific user
                    if (httpContext.User.Claims.First(x =>
                                x.Type.Equals(GlobalConstants.Authentication.CustomClaims.HasTwoFactorEnabled)).Value
                                .Equals("True"))
                    {
                        // 2FA is enabled for the user so check if user is 2FA authenticated
                        if (!httpContext.User.Claims.First(x =>
                                x.Type.Equals(GlobalConstants.Authentication.CustomClaims.IsTwoFactorAuthenticated)).Value
                                .Equals("True"))
                        {
                            // If we reach this point it means that User has his 2FA enabled but is not 2FA authenticated
                            // Construct Unauthorized Response with the mfa authentication page for redirect and return it
                            ConstructUnauthorizedResponseAndSetHttpResponse(httpContext.Response, GlobalConstants.Authentication.TwoFactor.MfaAuthenticationRedirectionPage);
                            return;
                        }
                    }
                    else
                    {
                        // Two factor is not enabled, check if it has to be enabled?
                        if (bool.Parse(_configuration["Auth:TwoFactor:EnforceTwoFactor"]))
                        {
                            // If we reach this point means that Backend Enforces 2FA and the user has not setup 2FA yet
                            // Construct Unauthorized Response with the mfa setup page for redirect and return it
                            ConstructUnauthorizedResponseAndSetHttpResponse(httpContext.Response, GlobalConstants.Authentication.TwoFactor.SetupMfaRedirectionPage);
                            return;
                        }
                    }
                }
            }

            await _next(httpContext);
        }

        /// <summary>
        /// Constructs the httpContext.Reposne Object adding the necessary redirection information for the front end
        /// </summary>
        /// <param name="httpResponseObj"></param>
        /// <param name="redirectPage"></param>
        private async void ConstructUnauthorizedResponseAndSetHttpResponse(HttpResponse httpResponseObj, string redirectPage)
        {
            httpResponseObj.Clear();
            httpResponseObj.StatusCode = StatusCodes.Status401Unauthorized;

            // Construct ObjectResponse with the route given as parameter.
            ObjectResponse<string> response = new ObjectResponse<string>();
            response.SetSuccess(redirectPage);

            var serializedResponse = JsonSerializer.Serialize(response);
            await httpResponseObj.WriteAsync(serializedResponse);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class MultiFactorMiddlewareExtensions
    {
        public static IApplicationBuilder UseMultiFactorMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MultiFactorMiddleware>();
        }
    }
}

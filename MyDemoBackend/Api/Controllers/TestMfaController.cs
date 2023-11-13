using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/test-mfa-middleware")]
    [ApiController]
    public class TestMfaController : ControllerBase
    {

        /// <summary>
        /// Test method with authorize attribute for checking if auth works (With MultiFactorMiddleware interference)
        /// </summary>
        /// <returns></returns>
        [Produces("application/json")]
        [HttpGet("ping-endpoint-with-authorize-atribute")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize]
        public async Task<ActionResult> PingEndpointWithAuthorizeAttribute()
        {
            return Ok();
        }
    }
}

using Messages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Tn_snController : ControllerBase
    {
        ITn_snService _service;

        public Tn_snController(ITn_snService service)
        {
            _service = service;
        }

        [Produces("application/json")]
        [HttpGet("onomata-roloi-sinteleston")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ListResponse<Tuple<string, string>>>> GetOnomataRolousSinteleston()
        {
            ListResponse<Tuple<string, string>> response = await _service.GetOnomataRolousSinteleston();
            if (response.Success)
            {
                return Ok(response);
            }

            return StatusCode((int)response.HttpResultCode, response);
        }

        [Produces("application/json")]
        [HttpGet("IdTainion-alfred-hitchcock")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ObjectResponse<List<int>>>> GetTainiaIdAlfred()
        {
            ObjectResponse<List<int>> response = await _service.GetTainiaIdAlfred();
            if (response.Success)
            {
                return Ok(response);
            }

            return StatusCode((int)response.HttpResultCode, response);
        }
    }
}

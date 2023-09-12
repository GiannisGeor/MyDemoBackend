using Messages;
using Microsoft.AspNetCore.Mvc;
using Models.Projections;
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

        /// <summary>
        /// 3.2.1 Q1 “Να δοθεί για κάθε συντελεστή το όνομα του και οι ρόλοι με τους οποίους αυτός έχει συμμετάσχει σε
        /// ταινίες”
        /// </summary>
        /// <returns></returns>
        [Produces("application/json")]
        [HttpGet("onomata-roloi-sinteleston")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ListResponse<OnomaKaiRolosSintelestiProjection>>> GetOnomataRolousSinteleston()
        {
            var response = await _service.GetOnomataRolousSinteleston();
            if (response.Success)
            {
                return Ok(response);
            }

            return StatusCode((int)response.HttpResultCode, response);
        }

        /// <summary>
        /// 3.2.1 Q2 “Να δοθούν οι κωδικοί των ταινιών στις οποίες έχει συμμετάσχει ο Alfred Hitchcock”
        /// </summary>
        /// <returns></returns>
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

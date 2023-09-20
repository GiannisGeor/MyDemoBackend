using Messages;
using Microsoft.AspNetCore.Mvc;
using Services.Dtos;
using Services.Interfaces;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TainiaController : ControllerBase
    {
        ITainiaService _service;

        public TainiaController(ITainiaService service)
        {
            _service = service;
        }

        [Produces("application/json")]
        [HttpPost("tainia-nea-kataxorisi")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ObjectResponse<NeaTainiaResponseDto>>> NeaTainia([FromBody] NeaTainiaDto neaTainiaDto)
        {
            ObjectResponse<NeaTainiaResponseDto> response = await _service.NeaTainia(neaTainiaDto);
            if (response.Success)
            {
                return Ok(response);
            }

            return StatusCode((int)response.HttpResultCode, response);

        }

        [Produces("application/json")]
        [HttpPut("tainia-metaboli/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ObjectResponse<MetaboliTainiasResponseDto>>> MetaboliTainias([FromRoute] int id, [FromBody] MetaboliTainiasDto metaboliTainiasDto)
        {
            ObjectResponse<MetaboliTainiasResponseDto> response = await _service.MetaboliTainias(id, metaboliTainiasDto);
            if (response.Success)
            {
                return Ok(response);
            }

            return StatusCode((int)response.HttpResultCode, response);

        }

        [Produces("application/json")]
        [HttpPost("tainia-sintelestes-nea-kataxorisi")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ObjectResponse<NeaTainiaKaiSintelestesDto>>> NeaTainiaKaiSintelestes([FromBody] NeaTainiaKaiSintelestesDto neaTainiaKaiSintelestesDto)
        {
            ObjectResponse<NeaTainiaKaiSintelestesResponseDto> response = await _service.NeaTainiaKaiSintelestes(neaTainiaKaiSintelestesDto);
            if (response.Success)
            {
                return Ok(response);
            }

            return StatusCode((int)response.HttpResultCode, response);

        }

        [Produces("application/json")]
        [HttpPut("prosthiki-sinteleston-se-tainia/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ObjectResponse<ProsthikiSintelestonDto>>> ProsthikiSinteleston([FromRoute] int id, [FromBody] ProsthikiSintelestonDto prosthikiSintelestonDto)
        {
            ObjectResponse<ProsthikiSintelestonResponseDto> response = await _service.ProsthikiSinteleston(id, prosthikiSintelestonDto);
            if (response.Success)
            {
                return Ok(response);
            }

            return StatusCode((int)response.HttpResultCode, response);
        }
    }
}

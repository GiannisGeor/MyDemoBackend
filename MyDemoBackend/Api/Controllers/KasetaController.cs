using Messages;
using Microsoft.AspNetCore.Mvc;
using Services.Dtos;
using Services.Interfaces;
using Services.Services;

namespace Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class KasetaController : ControllerBase
    {
        IKasetaService _service;

        public KasetaController(IKasetaService service)
        {
            _service = service;
        }

        [Produces("application/json")]
        [HttpGet("IdKaseton")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ObjectResponse<List<int>>>> GetKasetaId()
        {
            ObjectResponse<List<int>> response = await _service.GetKasetaId();
            if (response.Success)
            {
                return Ok(response);
            }

            return StatusCode((int)response.HttpResultCode, response);
        } 
        
        [Produces("application/json")]
        [HttpGet("IdKaseton-kata-afksousa-tropo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ObjectResponse<List<int>>>> GetKasetaIdAscend()
        {
            ObjectResponse<List<int>> response = await _service.GetKasetaIdAscend();
            if (response.Success)
            {
                return Ok(response);
            }

            return StatusCode((int)response.HttpResultCode, response);
        }
        
        [Produces("application/json")]
        [HttpGet("IdKaseton-kata-fthinousa-tropo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ObjectResponse<List<int>>>> GetKasetaIdDescend()
        {
            ObjectResponse<List<int>> response = await _service.GetKasetaIdDescend();
            if (response.Success)
            {
                return Ok(response);
            }

            return StatusCode((int)response.HttpResultCode, response);
        }

        [Produces("application/json")]
        [HttpGet("IdKaseton-2-kaseton-megaliteris-posotitas")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ObjectResponse<List<int>>>> GetIdDioKaseton()
        {
            ObjectResponse<List<int>> response = await _service.GetIdDioKaseton();
            if (response.Success)
            {
                return Ok(response);
            }

            return StatusCode((int)response.HttpResultCode, response);
        }
    }
}
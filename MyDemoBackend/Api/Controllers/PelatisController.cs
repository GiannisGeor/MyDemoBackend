using Messages;
using Microsoft.AspNetCore.Mvc;
using Services.Dtos;
using Services.Interfaces;
using Services.Services;

namespace Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PelatisController : ControllerBase
    {
        IPelatisService _service;

        public PelatisController(IPelatisService service)
        {
            _service = service;
        }

        [Produces("application/json")]
        [HttpGet("onomata")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ListResponse<string>>> GetOnomata()
        {
            ListResponse<string> response = await _service.GetOnomata();
            if (response.Success)
            {
                return Ok(response);
            }

            return StatusCode((int)response.HttpResultCode, response);
        }

        [Produces("application/json")]
        [HttpGet("tilefona")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ListResponse<string>>> GetTilefona()
        {
            ListResponse<string> response = await _service.GetTilefona();
            if (response.Success)
            {
                return Ok(response);
            }

            return StatusCode((int)response.HttpResultCode, response);
        }

        [Produces("application/json")]
        [HttpGet("pelates")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ListResponse<PelatisDto>>> GetPelates()
        {
            ListResponse <PelatisDto> response = await _service.GetPelates();
            if (response.Success)
            {
                return Ok(response);
            }

            return StatusCode((int)response.HttpResultCode, response);
        }

        [Produces("application/json")]
        [HttpGet("tilefona-me-kodikous-1")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ListResponse<string>>> GetTilefonaMeKodikous1()
        {
            ListResponse<string> response = await _service.GetTilefonaMeKodikous1();
            if (response.Success)
            {
                return Ok(response);
            }

            return StatusCode((int)response.HttpResultCode, response);
        }

        [Produces("application/json")]
        [HttpGet("tilefona-me-kodikous-2")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ListResponse<string>>> GetTilefonaMeKodikous2()
        {
            ListResponse<string> response = await _service.GetTilefonaMeKodikous2();
            if (response.Success)
            {
                return Ok(response);
            }

            return StatusCode((int)response.HttpResultCode, response);
        }

        [Produces("application/json")]
        [HttpGet("onomata-apo-k")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ListResponse<string>>> GetOnomataApoK()
        {
            ListResponse<string> response = await _service.GetOnomataApoK();
            if (response.Success)
            {
                return Ok(response);
            }

            return StatusCode((int)response.HttpResultCode, response);
        }

        [Produces("application/json")]
        [HttpGet("onomata-id-pelaton-kai-timi-kaseton-pou-exoun-enoikiasei")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ListResponse<Tuple<string, List<int>, List<int>>>>> GetOnomataIdPelatonKaiTimiKaseton()
        {
            ListResponse<Tuple<string, List<int>, List<int>>> response = await _service.GetOnomataIdPelatonKaiTimiKaseton();
            if (response.Success)
            {
                return Ok(response);
            }

            return StatusCode((int)response.HttpResultCode, response);
        }
    }
}




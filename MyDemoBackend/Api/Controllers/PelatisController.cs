using Messages;
using Microsoft.AspNetCore.Mvc;
using Services.Dtos;
using Services.Interfaces;

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

        /// <summary>
        /// 3.1.2 Q1 “Να δοθούν τα ονόματα όλων των πελατών ”
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// 3.1.2 Q2 “Να δοθούν τα τηλέφωνα όλων των πελατών ”
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// 3.1.2 Q4 “Να δοθούν όλα τα στοιχεία των πελατών”
        /// </summary>
        /// <returns></returns>
        [Produces("application/json")]
        [HttpGet("pelates")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ListResponse<PelatisDto>>> GetPelates()
        {
            ListResponse<PelatisDto> response = await _service.GetPelates();
            if (response.Success)
            {
                return Ok(response);
            }

            return StatusCode((int)response.HttpResultCode, response);
        }

        /// <summary>
        /// 3.1.2 Q5 “Στο  πεδίο  Τηλέφωνο  να  εμφανίζεται και  το  πρόθεμα  2310”
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// 3.1.2 Q5 “Στο  πεδίο  Τηλέφωνο  να  εμφανίζεται και  το  πρόθεμα  2310”
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// 3.1.2 Q8 “Να βρεθούν ποια ονόματα πελατών αρχίζουν από Κ”.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// 3.2.1 Q4 “Να βρεθούν για κάθε πελάτη το όνομα του, ο κωδικός και η τιμή των κασετών που έχει ενοικιάσει” 
        /// </summary>
        /// <returns></returns>
        [Produces("application/json")]
        [HttpGet("onomata-id-pelaton-kai-timi-kaseton-pou-exoun-enoikiasei")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ListResponse<StoixeiaPelatiKaiEnoikiasisDto>>> GetOnomataIdPelatonKaiTimiKaseton()
        {
            var response = await _service.GetOnomataIdPelatonKaiTimiKaseton();
            if (response.Success)
            {
                return Ok(response);
            }

            return StatusCode((int)response.HttpResultCode, response);
        }
    }
}




using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Messages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Dtos;
using Services.Interfaces;

namespace Services.Services
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EnoikiasiController : ControllerBase
    {
        IEnoikiasiService _service;

        public EnoikiasiController(IEnoikiasiService service)
        {
            _service = service;
        }

        [Produces("application/json")]
        [HttpGet("stoixeia-enoikiasis")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ListResponse<EnoikiasiDto>>> GetEnoikiaseis()
        {
            ListResponse<EnoikiasiDto> response = await _service.GetEnoikiaseis();
            if (response.Success)
            {
                return Ok(response);
            }

            return StatusCode((int)response.HttpResultCode, response);
        }
        
        [Produces("application/json")]
        [HttpGet("IdKaseton-enoikiasmenon")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ObjectResponse<List<Tuple<int, int, int, int>>>>> GetIdKasetonEnoikiasmenon()
        {
            ObjectResponse<List<Tuple<int, int, int, int>>> response = await _service.GetIdKasetonEnoikiasmenon();
            if (response.Success)
            {
                return Ok(response);
            }

            return StatusCode((int)response.HttpResultCode, response);
        }

    }
}

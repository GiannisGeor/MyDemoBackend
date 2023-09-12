using Messages;
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

        /// <summary>
        /// 3.1.2 Q9 “Να βρεθούν τα στοιχεία των ενοικιάσεων που δεν έχει ορισθεί ημερομηνία επιστροφής”. 
        /// </summary>
        /// <returns></returns>
        [Produces("application/json")]
        [HttpGet("stoixeia-enoikiasis-null")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ListResponse<EnoikiasiDto>>> GetEnoikiaseisNull()
        {
            ListResponse<EnoikiasiDto> response = await _service.GetEnoikiaseisNull();
            if (response.Success)
            {
                return Ok(response);
            }

            return StatusCode((int)response.HttpResultCode, response);
        }

        /// <summary>
        /// 3.1.3 Q12 “Να  δοθούν  οι  κωδικοί  των  κασετών  που  έχουν  ενοικιαστεί,  καθώς  και  οι  ημερομηνίες  επιστροφής τους,
        /// ταξινομημένες ως προς τις ημερομηνίες επιστροφής”.
        /// </summary>
        /// <returns></returns>
        [Produces("application/json")]
        [HttpGet("IdKaseton-enoikiasmenon-kai-imerominies")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ListResponse<EpistrofiKasetasDto>>> GetIdKasetonEnoikiasmenon()
        {
            var response = await _service.GetIdKasetonEnoikiasmenon();
            if (response.Success)
            {
                return Ok(response);
            }

            return StatusCode((int)response.HttpResultCode, response);
        }
    }
}

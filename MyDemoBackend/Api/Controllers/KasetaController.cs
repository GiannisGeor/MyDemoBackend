using Messages;
using Microsoft.AspNetCore.Mvc;
using Services.Dtos;
using Services.Interfaces;

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

        /// <summary>
        /// 3.1.2 Q7 “Να βρεθούν οι κωδικοί από τις κασέτες που είναι τύπου VHS και, επιπλέον, η διαθέσιμη ποσότητα
        /// τους είναι μεγαλύτερη του 2 ή η τιμή τους είναι μεγαλύτερη του 2”
        /// </summary>
        /// <returns></returns>
        [Produces("application/json")]
        [HttpGet("IdKaseton-vhs")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ObjectResponse<List<int>>>> GetKasetaIdVhs()
        {
            ObjectResponse<List<int>> response = await _service.GetKasetaIdVhs();
            if (response.Success)
            {
                return Ok(response);
            }

            return StatusCode((int)response.HttpResultCode, response);
        }

        /// <summary>
        /// 3.1.2 Q10 “Να δοθούν οι κωδικοί των κασετών ταξινομημένοι ως προς τη διαθέσιμη ποσότητά τους, κατά
        /// αύξοντα τρόπο”
        /// </summary>
        /// <returns></returns>
        [Produces("application/json")]
        [HttpGet("IdKaseton-kata-afksonta-tropo")]
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

        /// <summary>
        /// 3.1.2 Q11 “Να δοθούν οι κωδικοί των κασετών ταξινομημένοι κατά φθίνοντα τρόπο ως προς την τιμή ενοικίασης. 
        /// Στην περίπτωση ίσων τιμών ενοικίασης, η ταξινόμηση να γίνει κατά αύξουσα ταξινόμηση ως προς την
        /// ποσότητα”
        /// </summary>
        /// <returns></returns>
        [Produces("application/json")]
        [HttpGet("IdKaseton-kata-fthinonta-tropo")]
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

        /// <summary>
        /// 3.1.2 Q13 “Να δοθούν οι κωδικοί των 2 κασετών με τη μεγαλύτερη διαθέσιμη ποσότητα”
        /// </summary>
        /// <returns></returns>
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

        ///// <summary>
        ///// 3.2.1 Q6 “Να βρεθούν για κάθε πελάτη το όνομα του, ο κωδικός και η τιμή των κασετών που έχει ενοικιάσει. Να
        ///// εμφανίζονται οι κωδικοί και οι τιμές των κασετών που δεν έχουν ενοικιαστεί από κάποιον πελάτη”
        ///// </summary>
        ///// <returns></returns>
        //[Produces("application/json")]
        //[HttpGet("onomata-id-pelaton-kai-timi-kaseton-pou-exoun-enoikiasei-kai-onoma-null")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //public async Task<ActionResult<ListResponse<StoixeiaPelatiKaiEnoikiasisDto>>> GetOnomataIdPelatonNullKaiTimiKaseton()
        //{
        //    var response = await _service.GetOnomataIdPelatonNullKaiTimiKaseton();
        //    if (response.Success)
        //    {
        //        return Ok(response);
        //    }

        //    return StatusCode((int)response.HttpResultCode, response);
        //}

        /// <summary>
        /// 3.2.2 “Να βρεθεί ο κωδικός κάθε ταινίας για την οποία η κασέτα τύπου VHS είναι σε μεγαλύτερη ποσότητα
        /// από την αντίστοιχη κασέτα τύπου DVD”
        /// </summary>
        /// <returns></returns>
        [Produces("application/json")]
        [HttpGet("id-kasetas-vhs-se-megaliteri-posotita-apo-dvd")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ObjectResponse<List<int>>>> GetIdVhsMegaliterisPosotitas()
        {
            var response = await _service.GetIdVhsMegaliterisPosotitas();
            if (response.Success)
            {
                return Ok(response);
            }

            return StatusCode((int)response.HttpResultCode, response);
        }

        /// <summary>
        /// 3.3.1 “Να βρεθεί η μεγαλύτερη τιμή ενοικίασης μια κασέτας”
        /// </summary>
        /// <returns></returns>
        [Produces("application/json")]
        [HttpGet("megisti-timi-kaseton")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ValueResponse<decimal>>> GetMegistiTimiKasetas()
        {
            var response = await _service.GetMegistiTimiKasetas();
            if (response.Success)
            {
                return Ok(response);
            }

            return StatusCode((int)response.HttpResultCode, response);
        }

        /// <summary>
        /// Endpoint για  Καταχώρηση νέας κασέτας
        /// </summary>
        /// <param name="neaKasetaDto"></param>
        /// <returns></returns>
        [Produces("application/json")]
        [HttpPut("kaseta-nea-kataxorisi")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ObjectResponse<NeaKasetaResponseDto>>> NeaKaseta([FromBody] NeaKasetaDto neaKasetaDto)
        {
            ObjectResponse<NeaKasetaResponseDto> response = await _service.NeaKaseta(neaKasetaDto);
            if (response.Success)
            {
                return Ok(response);
            }

            return StatusCode((int)response.HttpResultCode, response);
        }
    }
}
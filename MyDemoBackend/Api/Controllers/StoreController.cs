using Messages;
using Microsoft.AspNetCore.Mvc;
using Services.Dtos;
using Services.Interfaces;

namespace Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class StoreController : Controller
    {
        IStoreService _storeService;

        public StoreController(IStoreService storeService)
        {
            _storeService = storeService;
        }

        [Produces("application/json")]
        [HttpGet("store-store-category-and-address")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ListResponse<StoreStoreCategoryAddressDto>>> GetStoreStoreCategoryAddress()
        {
            var response = await _storeService.GetStoreStoreCategoryAddress();
            if (response.Success)
            {
                return Ok(response);
            }

            return StatusCode((int)response.HttpResultCode, response);
        }

        [Produces("application/json")]
        [HttpGet("initial-store-data/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ObjectResponse<AllInitialDataDto>>> GetAllInitialData([FromRoute] int id)
        {
            var response = await _storeService.GetAllInitialData(id);
            if (response.Success)
            {
                return Ok(response);
            }

            return StatusCode((int)response.HttpResultCode, response);
        }
    }
}

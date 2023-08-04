using Messages;
using Microsoft.AspNetCore.Mvc;
using Services.Dtos;
using Services.Interfaces;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        IProductService _service;
        public ProductController(IProductService service)
        {
            _service = service;
        }

        [Produces("application/json")]
        [HttpGet("product/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ListResponse<ProductDto>>> GetActiveProduct([FromRoute] int id)
        {
            ObjectResponse<ProductDto> response = await _service.GetActiveProduct(id);
            if (response.Success)
            {
                return Ok(response);
            }

            return StatusCode((int)response.HttpResultCode, response);
        }

    }
}

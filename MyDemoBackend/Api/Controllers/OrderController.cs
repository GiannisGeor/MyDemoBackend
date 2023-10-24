using Messages;
using Microsoft.AspNetCore.Mvc;
using Services.Dtos;
using Services.Interfaces;
using Services.Services;

namespace Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }


        [Produces("application/json")]
        [HttpPost("new-order")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ObjectResponse<NewOrderResponseDto>>> NewOrder([FromBody] NewOrderDto newOrderDto )
        {
            var response = await _orderService.NewOrder(newOrderDto);
            if (response.Success)
            {
                return Ok(response);
            }

            return StatusCode((int)response.HttpResultCode, response);
        }
    }
}

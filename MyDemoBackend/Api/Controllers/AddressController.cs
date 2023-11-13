using Common.Configuration;
using Messages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Dtos;
using Services.Interfaces;
using Services.Services;
using static Common.Configuration.GlobalConstants.Authentication;

namespace Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AddressController : Controller
    {
        IAddressService _addressService;

        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }


        [Produces("application/json")]
        [HttpGet("all-customer-addresses")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = GlobalConstants.Authentication.Roles.Customer)]
        public async Task<ActionResult<ListResponse<CustomerAddressDto>>> GetCustomerAddress()
        {
            var response = await _addressService.GetCustomerAddress();
            if (response.Success)
            {
                return Ok(response);
            }

            return StatusCode((int)response.HttpResultCode, response);
        }

        [Produces("application/json")]
        [HttpPost("add-addresses")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = GlobalConstants.Authentication.Roles.Customer)]
        public async Task<ActionResult<ObjectResponse<AddressResponseDto>>> NewAddress([FromBody] AddressDto addressDto)
        {
            var response = await _addressService.NewAddress(addressDto);
            if (response.Success)
            {
                return Ok(response);
            }

            return StatusCode((int)response.HttpResultCode, response);
        }

        [Produces("application/json")]
        [HttpPut("edit-addresses/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = GlobalConstants.Authentication.Roles.Customer)]
        public async Task<ActionResult<ObjectResponse<AddressResponseDto>>> EditAddress([FromRoute] int id, [FromBody] AddressDto addressDto)
        {
            var response = await _addressService.EditAddress(id, addressDto);
            if (response.Success)
            {
                return Ok(response);
            }

            return StatusCode((int)response.HttpResultCode, response);
        }

        [Produces("application/json")]
        [HttpDelete("delete-addresses/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = GlobalConstants.Authentication.Roles.Customer)]
        public async Task<ActionResult<ValueResponse<bool>>> DeleteAddress([FromRoute] int id)
        {
            var response = await _addressService.DeleteAddress(id);
            if (response.Success)
            {
                return Ok(response);
            }

            return StatusCode((int)response.HttpResultCode, response);
        }
    }
}

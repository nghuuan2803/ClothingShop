using Application.Features.Carts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Endpoints
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController(ISender sender) : ControllerBase
    {
        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetCartItems(string customerId)
        {
            try
            {
                var result = await sender.Send(new GetCartQuery(customerId));
                return Ok(result);
            }
            catch (ApplicationException e)
            {
                return BadRequest(e.Message);
            }

        }
        [HttpPost]
        public async Task<IActionResult> AddToCart([FromBody] AddCartItemCommand command)
        {
            var result = await sender.Send(command);
            if (result == "Success")
                return Ok(result);
            return BadRequest(result);
        }
    }
}

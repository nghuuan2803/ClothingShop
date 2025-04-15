using Application.Features.Orders.Commands;
using Application.Features.Orders.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Endpoints
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController(ISender _sender) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOrderCommand command)
        {
            try
            {
                var result = await _sender.Send(command);
                return Ok(result);
            }
            catch (ApplicationException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _sender.Send(new GetAllOrdersQuery());
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAll(string id)
        {
            var data = await _sender.Send(new GetOrderQuery(id));
            return data != null ? Ok(data) : BadRequest("Order not found!");
        }
    }
}

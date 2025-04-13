using Application.Features.Products.Commands;
using Application.Features.Products.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Products;

namespace WebApp.Endpoints
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(ISender sender) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddProductReq req)
        {
            var command = new AddProductCommand { Payload = req };
            int id = await sender.Send(command);
            if (id > 0)
                return Ok(id);
            return BadRequest("Create Fail!");
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Add(int id, [FromBody] UpdateProductCommand command)
        {
            var res = await sender.Send(command);
            if (res == "Success")
                return Ok(res);
            return BadRequest(res);

        }
        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var data = await sender.Send(new GetProductListQuery(), cancellationToken);
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var data = await sender.Send(new GetProductQuery(id));
                return Ok(data);
            }
            catch (ApplicationException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var res = await sender.Send(new DeleteProductCommand(id));
            if (res == "Success")
                return Ok(res);
            else
                return BadRequest(res);
        }

        [HttpPost("variant")]
        public async Task<IActionResult> AddVariant([FromBody] AddVariantCommand command)
        {
            try
            {
                int id = await sender.Send(command);
                if (id > 0)
                    return Ok(id);
                return BadRequest("Create Fail!");
            }
            catch (ApplicationException e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPut("variant/{id}")]
        public async Task<IActionResult> UpdateVariant(int id, [FromBody] UpdateVariantCommand command)
        {
            var res = await sender.Send(command);
            if (res == "Success")
                return Ok(res);
            return BadRequest(res);
        }

        [HttpDelete("variant/{id}")]
        public async Task<IActionResult> DeleteVariant(int id)
        {
            var res = await sender.Send(new DeleteVariantCommand(id));
            if (res == "Success")
                return Ok(res);
            else
                return BadRequest(res);
        }
    }
}

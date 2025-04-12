using Application.Features.Categories.Commands;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Categories;

namespace WebApp.Endpoints
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController(IMediator _mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddCategoryReq req)
        {
            try
            {
                var command = new AddCategoryCommand(req);
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Errors.Select(e => e.ErrorMessage));
            }
        }

    }
}

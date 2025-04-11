using Application.Categories.Commands;
using Contracts.Repositories;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Shared.Requests;

namespace WebApp.Endpoints
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController(IRepository<Category> repo) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddCategoryReq req)
        {
            var command = new AddCategoryCommand(repo);
            int id = await command.Execute(req);
            return Ok(id);
        }
    }
}

using Application.Features.Users.Commands;
using Application.Features.Users.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Endpoints
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var res = await _mediator.Send(new GetAllUsersQuery(),cancellationToken);
            return Ok(res);
        }

        [Authorize]
        [HttpGet("customers")]
        public async Task<IActionResult> GetAllGuest(CancellationToken cancellationToken)
        {
            var res = await _mediator.Send(new GetAllGuestQuery(), cancellationToken);
            return Ok(res);
        }

        [HttpPost("guests")]
        public async Task<IActionResult> AddGuest([FromBody] AddGuestCommand command)
        {
            var res = await _mediator.Send(command);
            return Ok(res);
        }
    }
}

using Application.Features.Auth;
using Application.Features.Users.Commands;
using Application.Services.Auth;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Auth;
using Shared.Users;

namespace WebApp.Endpoints
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IMediator _mediator;

        public AuthController(IAuthService authService, IMediator mediator)
        {
            _authService = authService;
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<IActionResult> DefaultLogin([FromBody] DefaultLoginReq request)
        {
            var command = new LoginCommand
            {
                GuestId = request.GuestId,
                LoginType = "default", 
                Credential = $"{request.UserName}|{request.Password}" 
            };
            var result = await _mediator.Send(command);
            if (!result.Success)
            {
                return BadRequest(new { Error = "Login failed. Invalid credentials or login type." });
            }

            return Ok(result);
        }

        [HttpPost("login/google-web")]
        public async Task<IActionResult> GoogleWebLogin([FromBody] GoogleWebLoginReq request)
        {
            var command = new LoginCommand
            {
                GuestId = request.GuestId,
                LoginType = "google-web",
                Credential = request.AuthCode
            };
            var result = await _mediator.Send(command);
            if (!result.Success)
            {
                return BadRequest(new { Error = "Login failed. Invalid credentials or login type." });
            }

            return Ok(result);
        }
        [HttpPost("login/google-mobile")]
        public async Task<IActionResult> GoogleMobileLogin([FromBody] GoogleMobileLoginReq request)
        {
            var command = new LoginCommand
            {
                GuestId = request.GuestId,
                LoginType = "google-mobile",
                Credential = request.IdToken
            };
            var result = await _mediator.Send(command);
            if (!result.Success)
            {
                return BadRequest(new { Error = "Login failed. Invalid credentials or login type." });
            }

            return Ok(result);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] AddUserReq request, CancellationToken cancellationToken)
        {
            if (request == null || string.IsNullOrEmpty(request.FullName) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest(new { Error = "Invalid registration request" });
            }

            var command = new CreateUserCommand { Request = request };
            var result = await _mediator.Send(command, cancellationToken);
            if (!result.Success)
            {
                return BadRequest(new { Error = "Registration failed. Email may already exist." });
            }

            return Ok(result);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] string userName, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(userName))
            {
                return BadRequest(new { Error = "Username is required" });
            }

            var success = await _authService.LogoutAsync(userName);
            if (!success)
            {
                return BadRequest(new { Error = "Logout failed. User not found." });
            }

            return Ok(new { Message = "Logged out successfully" });
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] string refreshToken, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(refreshToken))
            {
                return BadRequest(new { Error = "Refresh token is required" });
            }

            var result = await _authService.RefreshTokensAsync(refreshToken);
            if (!result.Success)
            {
                return BadRequest(new { Error = "Invalid or expired refresh token" });
            }

            return Ok(result);
        }
    }
}

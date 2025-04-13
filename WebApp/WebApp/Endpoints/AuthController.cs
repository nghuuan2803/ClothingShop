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
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        /// <summary>
        /// Đăng nhập bằng mật khẩu, Google web, hoặc Google mobile.
        /// </summary>
        /// <param name="request">Thông tin đăng nhập (GuestId, LoginType, Credential).</param>
        /// <param name="cancellationToken">Token hủy bỏ.</param>
        /// <returns>Access token và refresh token nếu thành công.</returns>
        [HttpPost("login")]
        [ProducesResponseType(typeof(AuthRes), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login([FromBody] LoginReq request, CancellationToken cancellationToken)
        {
            if (request == null || string.IsNullOrEmpty(request.LoginType) || string.IsNullOrEmpty(request.Credential))
            {
                return BadRequest(new { Error = "Invalid login request" });
            }

            var result = await _authService.LoginAsync(request);
            if (!result.Success)
            {
                return BadRequest(new { Error = "Login failed. Invalid credentials or login type." });
            }

            return Ok(result);
        }

        /// <summary>
        /// Đăng ký tài khoản mới.
        /// </summary>
        /// <param name="request">Thông tin đăng ký (GuestId, FullName, Email, Password, ...).</param>
        /// <param name="cancellationToken">Token hủy bỏ.</param>
        /// <returns>Access token và refresh token nếu thành công.</returns>
        [HttpPost("register")]
        [ProducesResponseType(typeof(AuthRes), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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

        /// <summary>
        /// Đăng xuất người dùng.
        /// </summary>
        /// <param name="userName">Tên người dùng (username).</param>
        /// <param name="cancellationToken">Token hủy bỏ.</param>
        /// <returns>Trạng thái đăng xuất.</returns>
        [HttpPost("logout")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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

        /// <summary>
        /// Làm mới access token bằng refresh token.
        /// </summary>
        /// <param name="refreshToken">Refresh token hiện tại.</param>
        /// <param name="cancellationToken">Token hủy bỏ.</param>
        /// <returns>Access token và refresh token mới nếu thành công.</returns>
        [HttpPost("refresh-token")]
        [ProducesResponseType(typeof(AuthRes), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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

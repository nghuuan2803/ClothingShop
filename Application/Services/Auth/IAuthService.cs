using Domain.Entities;
using Shared.Auth;

namespace Application.Services.Auth
{
    public interface IAuthService
    {
        Task<AuthRes> LoginAsync(LoginReq request);
        Task<bool> LogoutAsync(string userName);
        Task<AuthRes> RegisterAsync(RegisterReq request);
        Task<AuthRes> RefreshTokensAsync(string refreshToken);
        Task<string> GenerateAccessTokenAsync(IUser user);
        string GenerateRefreshToken();
    }
}

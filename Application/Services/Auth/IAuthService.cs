using Application.Features.Auth;
using Domain.Entities;
using Shared.Auth;

namespace Application.Services.Auth
{
    public interface IAuthService
    {
        Task<AuthRes> LoginAsync(LoginCommand request);
        Task<bool> LogoutAsync(string userName);
        Task<AuthRes> RefreshTokensAsync(string refreshToken);
        Task<string> GenerateAccessTokenAsync(IUser user);
        string GenerateRefreshToken();
    }
}

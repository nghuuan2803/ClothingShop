using Application.Features.Auth;
using Application.Services.Auth;
using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Persistence;
using Infrastructure.Services.Auth.LoginStrategies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Shared.Auth;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly IServiceProvider _serviceProvider;
        private readonly UserManager<AppUser> _userManager;
        private readonly AppDbContext _dbContext;

        public AuthService(IConfiguration configuration, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _configuration = configuration;
            _userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
            _dbContext = serviceProvider.GetRequiredService<AppDbContext>();
        }

        public async Task<AuthRes> LoginAsync(LoginCommand request)
        {
            // Chọn strategy dựa trên LoginType
            ILoginStragy strategy = request.LoginType switch
            {
                "default" => new DefaultLoginStrategy(_serviceProvider),
                "google_web" => new LoginByGoogleOnWebStrategy(request.GuestId,_serviceProvider),
                "google_mobile" => new LoginByGoogleOnMobileStrategy(request.GuestId,_serviceProvider),
                _ => throw new NotSupportedException($"Login type '{request.LoginType}' is not supported.")
            };

            // Thực thi strategy để lấy user
            var user = await strategy.Execute(request);
            if (user == null)
            {
                var authRes = new AuthRes { Success = false };
            }

            // Tạo access token và refresh token
            string accessToken = await GenerateAccessTokenAsync(user);
            string refreshToken = GenerateRefreshToken();

            // Lưu refresh token
            await _userManager.SetAuthenticationTokenAsync(user as AppUser, "MyApp", "RefreshToken", refreshToken);        

            return new AuthRes
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                Success = true,
                UserId = user.Id,
            };
        }

        public async Task<bool> LogoutAsync(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                return false;
            }

            await _userManager.RemoveAuthenticationTokenAsync(user, "MyApp", "RefreshToken");
            return true;
        }

        public async Task<AuthRes> RefreshTokensAsync(string refreshToken)
        {

            var userToken = await _dbContext.UserTokens.FirstOrDefaultAsync(p =>p.Value == refreshToken);
            if (userToken == null)
                return new AuthRes { Success = false };

            var user = await _userManager.FindByIdAsync(userToken.UserId);
            if (user == null)
                return new AuthRes { Success = false };

            var newAccessToken = await GenerateAccessTokenAsync(user);
            var newRefreshToken = GenerateRefreshToken();
            await _userManager.SetAuthenticationTokenAsync(user, "MyApp", "RefreshToken", newRefreshToken);

            return new AuthRes
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken,
                Success = true
            };
        }

        public async Task<string> GenerateAccessTokenAsync(IUser user)
        {
            var appUser = user as AppUser;
            var roles = await _userManager.GetRolesAsync(appUser);
            var role = roles.FirstOrDefault() ?? "Customer";

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email ?? user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Role, role)
            };

            var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                expires: DateTime.UtcNow.AddMinutes(int.Parse(_configuration["JWT:Expire"])),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authKey, SecurityAlgorithms.HmacSha256Signature)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GenerateRefreshToken()
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
        }

    }
}

using Application.Services.Auth;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using Google.Apis.Auth;
using Shared.Auth;
using Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;
using Application.Features.Auth;

namespace Infrastructure.Services.Auth.LoginStrategies
{
    public class LoginByGoogleOnWebStrategy : ILoginStragy
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly UserManager<AppUser> _userManager;
        private string _guestId;
        private IRepository<Customer> _customerRepo;

        public LoginByGoogleOnWebStrategy(string guestId, IServiceProvider serviceProvider)
        {
            _guestId = guestId;
            _httpClient = serviceProvider.GetRequiredService<HttpClient>();
            _userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
            _customerRepo = serviceProvider.GetRequiredService<IRepository<Customer>>();
            _configuration = serviceProvider.GetRequiredService<IConfiguration>();
        }

        public async Task<IUser> Execute(LoginCommand request)
        {
            var config = _configuration.GetSection("Authentication:Google");
            string clientId = config["ClientId"];
            string clientSecret = config["ClientSecret"];
            string redirectUri = config["RedirectUri"]; // Cấu hình trong appsettings.json

            // Trao đổi auth code lấy token
            var tokenRequestParams = new Dictionary<string, string>
            {
                { "code", request.Credential },
                { "client_id", clientId },
                { "client_secret", clientSecret },
                { "redirect_uri", redirectUri },
                { "grant_type", "authorization_code" }
            };

            var requestContent = new FormUrlEncodedContent(tokenRequestParams);
            var tokenResponse = await _httpClient.PostAsync("https://oauth2.googleapis.com/token", requestContent);
            if (!tokenResponse.IsSuccessStatusCode)
            {
                return null;
            }

            var jsonResponse = await tokenResponse.Content.ReadAsStringAsync();
            using var jsonDoc = JsonDocument.Parse(jsonResponse);
            if (!jsonDoc.RootElement.TryGetProperty("id_token", out var idTokenElement))
            {
                return null;
            }
            var idToken = idTokenElement.GetString();
            if (string.IsNullOrEmpty(idToken))
            {
                return null;
            }

            // Xác thực id_token
            var payload = await GoogleJsonWebSignature.ValidateAsync(idToken, new GoogleJsonWebSignature.ValidationSettings
            {
                Audience = new[] { clientId }
            });

            // Tìm hoặc tạo user
            var user = await _userManager.FindByEmailAsync(payload.Email);
            if (user == null)
            {
                user = new AppUser
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = payload.Email,
                    Email = payload.Email,
                    FullName = payload.Name,
                    AvatarUrl = payload.Picture,
                    EmailConfirmed = true
                };

                var createResult = await _userManager.CreateAsync(user);
                if (!createResult.Succeeded)
                {
                    return null;
                }

                await _userManager.AddToRoleAsync(user, "Customer");

                //map customer
                var customer = await _customerRepo.GetByIdAsync(_guestId);
                if (customer is null)
                {
                    customer = new Customer
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = user.FullName,
                        UserId = user.Id,
                        IsRegistered = true
                    };
                    await _customerRepo.AddAsync(customer);
                }
                else
                {
                    customer.Name = user.FullName;
                    customer.IsRegistered = true;
                    customer.UserId = user.Id;

                    _customerRepo.Update(customer);
                }
                await _customerRepo.SaveChangesAsync();
            }

            return user;
        }
    }
}
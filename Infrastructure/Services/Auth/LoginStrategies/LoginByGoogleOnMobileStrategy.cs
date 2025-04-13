using Application.Services.Auth;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Google.Apis.Auth;
using Shared.Auth;
using Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Services.Auth.LoginStrategies
{
    public class LoginByGoogleOnMobileStrategy : ILoginStragy
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<AppUser> _userManager;
        private string _guestId;
        private IRepository<Customer> _customerRepo;

        public LoginByGoogleOnMobileStrategy(string guestId, IServiceProvider serviceProvider)
        {
            _guestId = guestId;
            _userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
            _customerRepo = serviceProvider.GetRequiredService<IRepository<Customer>>();
            _configuration = serviceProvider.GetRequiredService<IConfiguration>();
        }

        public async Task<IUser> Execute(LoginReq request)
        {
            var config = _configuration.GetSection("Authentication:Google");
            string clientId = config["ClientId"];

            // Credential là id_token từ Google Sign-In trên mobile
            var idToken = request.Credential;

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
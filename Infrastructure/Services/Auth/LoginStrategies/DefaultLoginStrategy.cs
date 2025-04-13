using Application.Services.Auth;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using Shared.Auth;
using Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;
using Application.Features.Auth;

namespace Infrastructure.Services.Auth.LoginStrategies
{
    public class DefaultLoginStrategy : ILoginStragy
    {
        private readonly UserManager<AppUser> _userManager;

        public DefaultLoginStrategy(IServiceProvider serviceProvider)
        {
            _userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
        }

        public async Task<IUser> Execute(LoginCommand request)
        {
            // Credential là "username|password"
            var credentials = request.Credential.Split('|');
            if (credentials.Length != 2)
            {
                return null;
            }

            var email = credentials[0];
            var password = credentials[1];

            var user = await _userManager.FindByEmailAsync(email);
            if(user == null)
                user = await _userManager.FindByNameAsync(email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, password))
            {
                return null;
            }
            return user;
        }
    }
}
using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Services.Auth
{
    public class AuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IRepository<IUser> _userRepo;
        private readonly IConfiguration _configuration;

    }
}

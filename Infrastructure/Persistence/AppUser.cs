using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Shared.Enums;

namespace Infrastructure.Persistence
{
    public class AppUser : IdentityUser, IUser
    {
        public DateTime? DoB { get; set; }
        public Gender? Gender { get; set; }
        public string? FullName { get; set; }
        public string? AvatarUrl { get; set; }
        public int RewardPoint { get; set; }
    }
}

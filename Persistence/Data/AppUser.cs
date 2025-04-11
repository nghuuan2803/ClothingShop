using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Shared.Enums;

namespace Persistence.Data
{
    public class AppUser : IdentityUser, IUser
    {
        public DateTime? DoB { get; set; }
        public Gender? Gender { get; set; }
        public string? AvatarUrl { get; set; }
        public int RewardPoint { get; set; }
    }
}

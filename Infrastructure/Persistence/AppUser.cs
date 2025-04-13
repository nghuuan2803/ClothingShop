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
        public DateTimeOffset CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
    }
}

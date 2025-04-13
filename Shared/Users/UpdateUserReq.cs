using Shared.Enums;

namespace Shared.Users
{
    public class UpdateUserReq
    {
        public string CustomerId { get; set; }
        public string FullName { get; set; }
        public DateTime? DoB { get; set; }
        public Gender? Gender { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? AvatarUrl { get; set; }
    }
}

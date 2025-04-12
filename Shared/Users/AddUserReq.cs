namespace Shared.Users
{
    public class AddUserReq
    {
        public required string GuestId { get; set; }
        public required string FullName { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public required string Password { get; set; }
        public string? AvatarUrl { get; set; }
    }
}

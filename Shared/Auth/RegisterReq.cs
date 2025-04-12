namespace Shared.Auth
{
    public class RegisterReq
    {
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? UserName { get; set; }
        public required string FullName { get; set; }
        public string? Password { get; set; }
        public string? AvatarUrl { get; set; }
    }
}

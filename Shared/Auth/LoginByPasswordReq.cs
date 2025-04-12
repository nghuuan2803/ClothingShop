namespace Shared.Auth
{
    public class LoginByPasswordReq
    {
        public required string UserName { get; set; }
        public required string Password { get; set; }
    }
}

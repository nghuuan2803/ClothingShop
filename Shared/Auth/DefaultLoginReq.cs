namespace Shared.Auth
{
    public class DefaultLoginReq
    {
        public required string GuestId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}

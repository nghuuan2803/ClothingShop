namespace Shared.Auth
{
    public class LoginReq
    {
        public required string GuestId { get; set; }
        public required string LoginType { get; set; }  // "google", "password", "facebook"
        public required string Credential { get; set; } // AuthCode, Email|Password, IDToken
    }
}

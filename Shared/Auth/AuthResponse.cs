namespace Shared.Auth
{
    public class AuthResponse
    {
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
        public bool Success { get; set; }
        public string? CustomerId { get; set; }
    }
}

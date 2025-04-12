namespace Shared.Auth
{
    public class AuthRes
    {
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
        public bool Success { get; set; }
    }
}

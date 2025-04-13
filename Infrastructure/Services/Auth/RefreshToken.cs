using Infrastructure.Persistence;

namespace Infrastructure.Services.Auth
{
    public class RefreshToken
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string UserId { get; set; }
        public string Token { get; set; }
        public DateTime IssuedAt { get; set; }
        public DateTime ExpiresAt { get; set; }
        public bool IsRevoked { get; set; }
        public string? DeviceInfo { get; set; } // Lưu thông tin thiết bị (tùy chọn)
        public AppUser? User { get; set; }
    }
}

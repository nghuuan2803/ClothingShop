namespace Domain.Entities
{
    //Người dùng đã có tài khoản
    public interface IUser : IEntity
    {
        string Id { get; set; }
        string? Email { get; set; }
        string? PhoneNumber { get; set; }
        string? FullName { get; set; }
        string? UserName { get; set; }
        DateTime? DoB { get; set; }
        Gender? Gender { get; set; }
        string? AvatarUrl { get; set; }
        public int RewardPoint { get; set; }
    }
}

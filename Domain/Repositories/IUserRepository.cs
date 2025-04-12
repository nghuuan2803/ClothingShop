namespace Domain.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<IUser>> GetAllAsync();
        Task<IUser> GetByIdAsync(string id);
        Task<IUser> GetByUserNameAsync(string userName);
        Task<IUser> GetByEmailAsync(string email);
        Task<IUser> GetByPhoneNumber(string phoneNumber);
    }
}

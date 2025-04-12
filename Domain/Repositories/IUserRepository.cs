using Shared.Users;

namespace Domain.Repositories
{
    public interface IUserRepository
    {
        Task<IUser> GetByIdAsync(string userName);
        Task<IUser> GetByUserNameAsync(string userName);
        Task<IUser> GetByEmailAsync(string email);
        Task<IUser> GetByPhoneNumber(string phoneNumber);

        Task<IUser> AddUserAsync(AddUserReq request);
        Task UpdateUserAsync();
    }
}

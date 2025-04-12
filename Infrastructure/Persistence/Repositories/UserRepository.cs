using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _dbContext;

        public UserRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<IUser>> GetAllAsync()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<IUser> GetByEmailAsync(string email)
        {
            return await _dbContext.Users.SingleOrDefaultAsync(p => p.Email == email);
        }

        public async Task<IUser> GetByIdAsync(string id)
        {
            return await _dbContext.Users.FindAsync(id);
        }

        public async Task<IUser> GetByPhoneNumber(string phoneNumber)
        {
            return await _dbContext.Users.SingleOrDefaultAsync(p => p.PhoneNumber == phoneNumber);
        }

        public async Task<IUser> GetByUserNameAsync(string userName)
        {
            return await _dbContext.Users.SingleOrDefaultAsync(p => p.UserName == userName);
        }
    }
}

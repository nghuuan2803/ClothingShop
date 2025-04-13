using Domain.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shared.Users;

namespace Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly UserManager<AppUser> _userManager;

        public UserRepository(AppDbContext dbContext, UserManager<AppUser> userManager)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        public async Task<IEnumerable<IUser>> GetAllAsync()
        {
            return await _dbContext.Users.Where(p=>!p.IsDeleted).ToListAsync();
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
            return await _dbContext.Users.AsNoTracking().SingleOrDefaultAsync(p => p.PhoneNumber == phoneNumber);
        }

        public async Task<IUser> GetByUserNameAsync(string userName)
        {
            return await _dbContext.Users.AsNoTracking().SingleOrDefaultAsync(p => p.UserName == userName);
        }

        public async Task<IUser> AddUserAsync(AddUserReq request)
        {
            if (!string.IsNullOrEmpty(request.Email) && await _userManager.FindByEmailAsync(request.Email) != null)
            {
                throw new Exception("Email already exists");
            }
            // Tạo đối tượng AppUser từ request
            var appUser = new AppUser
            {
                Id = Guid.NewGuid().ToString(), // Tạo ID mới
                UserName = request.UserName ?? request.Email, // Nếu không có UserName, dùng Email
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                FullName = request.FullName,
                AvatarUrl = request.AvatarUrl
            };

            // Thêm user vào Identity
            var result = await _userManager.CreateAsync(appUser, request.Password);
            if (!result.Succeeded)
            {
                throw new Exception($"Failed to create user: {string.Join(", ", result.Errors.Select(e => e.Description))}");
            }

            // Gán role "customer" cho user
            var roleResult = await _userManager.AddToRoleAsync(appUser, "customer");
            if (!roleResult.Succeeded)
            {
                throw new Exception($"Failed to assign role: {string.Join(", ", roleResult.Errors.Select(e => e.Description))}");
            }


            return appUser;
        }

        public async Task UpdateUserAsync(IUser user)
        {
            // Tìm AppUser trong database
            var appUser = await _userManager.FindByIdAsync(user.Id);
            if (appUser == null)
            {
                throw new Exception("User not found");
            }

            // Cập nhật các thuộc tính
            appUser.Email = user.Email ?? appUser.Email;
            appUser.PhoneNumber = user.PhoneNumber ?? appUser.PhoneNumber;
            appUser.FullName = user.FullName ?? appUser.FullName;
            appUser.UserName = user.UserName ?? appUser.UserName;
            appUser.AvatarUrl = user.AvatarUrl ?? appUser.AvatarUrl;
            appUser.DoB = user.DoB ?? appUser.DoB;
            appUser.Gender = user.Gender ?? appUser.Gender;
            appUser.RewardPoint = user.RewardPoint;

            // Cập nhật user trong Identity
            var result = await _userManager.UpdateAsync(appUser);
            if (!result.Succeeded)
            {
                throw new Exception($"Failed to update user: {string.Join(", ", result.Errors.Select(e => e.Description))}");
            }

            // Cập nhật thông tin Customer nếu có
            var customer = await _dbContext.Customers
                .FirstOrDefaultAsync(c => c.UserId == appUser.Id);
            if (customer != null)
            {
                customer.Name = appUser.FullName;
                customer.PhoneNumber = appUser.PhoneNumber;
                _dbContext.Customers.Update(customer);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<IUser>> GetAllAsync(CancellationToken cancellation = default)
        {
            return await _dbContext.Users.ToListAsync();
        }
    }
}
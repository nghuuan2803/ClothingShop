using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;

public static class DbSeeder
{
    public static async Task SeedAdminAsync(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        string adminUserName = "admin";
        string adminEmail = "an28031998@gmail.com";
        string adminPassword = "Admin@123";

        // Tạo vai trò nếu chưa có
        if (!await roleManager.RoleExistsAsync("admin"))
        {
            await roleManager.CreateAsync(new IdentityRole("admin"));
        }

        // Tạo tài khoản admin nếu chưa tồn tại
        var adminUser = await userManager.FindByNameAsync(adminUserName);
        if (adminUser == null)
        {
            adminUser = new AppUser
            {
                UserName = adminUserName,
                NormalizedUserName = adminUserName.ToUpper(),
                Email = adminEmail,
                NormalizedEmail = adminEmail.ToUpper(),
                EmailConfirmed = true,
                CreatedAt = DateTimeOffset.UtcNow,
                CreatedBy = "System"
            };

            var result = await userManager.CreateAsync(adminUser, adminPassword);

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
            else
            {
                // Ghi log lỗi nếu cần
                throw new Exception("Không thể tạo tài khoản admin: " + string.Join(", ", result.Errors.Select(e => e.Description)));
            }
        }
    }
}

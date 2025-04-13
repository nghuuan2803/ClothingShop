using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Infrastructure.Persistence
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<Color> Colors { get; set; }
        public virtual DbSet<Banner> Banners { get; set; }

        public virtual DbSet<ShopBranch> ShopBranches { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductTag> ProductTags { get; set; }
        public virtual DbSet<ProductVariant> ProductVariants { get; set; }
        public virtual DbSet<Inventory> Inventories { get; set; }

        public virtual DbSet<CartItem> CartItems { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderItem> OrderItems { get; set; }
        public virtual DbSet<ShippingInfo> ShippingInfos { get; set; }
        public virtual DbSet<ShippingDetail> ShippingDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Customer>().HasOne<AppUser>().WithOne().HasForeignKey<Customer>(p => p.UserId).OnDelete(DeleteBehavior.Restrict);
            builder.Entity<ShopBranch>().HasOne<AppUser>().WithMany().HasForeignKey(p => p.ManagerId).OnDelete(DeleteBehavior.SetNull);
            builder.Entity<Order>().HasOne<AppUser>().WithMany().HasForeignKey(p => p.HandlerId).OnDelete(DeleteBehavior.SetNull);

            builder.Entity<CartItem>().HasKey(p => new { p.CustomerId, p.VariantId });
            builder.Entity<OrderItem>().HasKey(p => new { p.OrderId, p.VariantId });
            builder.Entity<ProductTag>().HasKey(p => new { p.ProductId, p.TagId });
            SeedData(builder);
        }

        private void SeedData(ModelBuilder builder)
        {
            string adminRoleId = "bdd06cc1-4b82-48ce-9aa2-2f574bd1896c";
            string customerRoleId = "7a4dff7f-3d6a-4883-800b-7103ce57af94";
            var adminRole = new IdentityRole
            {
                Id = adminRoleId,
                Name = "admin",
                NormalizedName = "ADMIN"
            };
            var customerRole = new IdentityRole
            {
                Id = customerRoleId,
                Name = "customer",
                NormalizedName = "CUSTOMER"
            };
            var adminAcc = new AppUser
            {
                Id = "8c18473e-f0be-4202-bc37-38ced67318cb",
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "an28031998@gmail.com",
                NormalizedEmail = "an28031998@gmail.com".ToUpper(),
                EmailConfirmed = true,
                CreatedAt = DateTimeOffset.UtcNow,
                CreatedBy = "System"
            };
            var passwordHasher = new PasswordHasher<AppUser>();
            adminAcc.PasswordHash = passwordHasher.HashPassword(adminAcc, "Admin@123");
            //builder.Entity<AppUser>().HasData(adminAcc);
            builder.Entity<IdentityRole>().HasData(adminRole);
            builder.Entity<IdentityRole>().HasData(customerRole);
        }
    }
}

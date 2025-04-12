using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

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
        }
    }
}

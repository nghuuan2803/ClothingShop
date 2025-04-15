using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public virtual DbSet<Color> Colors { get; set; }
        public virtual DbSet<Size> Sizes { get; set; }
        public virtual DbSet<Banner> Banners { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Brand> Brands { get; set; }
        public virtual DbSet<ProductCollection> ProductCollections { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductVariant> ProductVariants { get; set; }
        public virtual DbSet<Inventory> Inventories { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<CartItem> CartItems { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderItem> OrderItems { get; set; }
        //public virtual DbSet<ShippingInfo> ShippingInfos { get; set; }
        //public virtual DbSet<ShippingDetail> ShippingDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Customer>().HasOne<AppUser>().WithOne().HasForeignKey<Customer>(p => p.UserId).OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Order>().HasOne<AppUser>().WithMany().HasForeignKey(p => p.HandlerId).OnDelete(DeleteBehavior.SetNull);
            builder.Entity<CartItem>().HasKey(p => new { p.CustomerId, p.VariantId,p.SizeId });
            builder.Entity<OrderItem>().HasKey(p => new { p.OrderId, p.VariantId,p.SizeId });
            builder.Entity<Inventory>().HasKey(p => new { p.VariantId, p.SizeId });
            SeedRole(builder);
            SeedColors(builder);
            SeedSizes(builder);
            SeedCategories(builder);
            SeedBrands(builder);
        }

        private void SeedRole(ModelBuilder builder)
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

            builder.Entity<IdentityRole>().HasData(adminRole);
            builder.Entity<IdentityRole>().HasData(customerRole);
        }

        private void SeedSizes(ModelBuilder builder)
        {
            List<Size> sizes = new List<Size>();
            sizes.Add(new Size { Id = 1, Name = "S" });
            sizes.Add(new Size { Id = 2, Name = "M" });
            sizes.Add(new Size { Id = 3, Name = "L" });
            sizes.Add(new Size { Id = 4, Name = "XL" });
            sizes.Add(new Size { Id = 5, Name = "XXL" });
            sizes.Add(new Size { Id = 6, Name = "XXXL" });
            sizes.Add(new Size { Id = 7, Name = "4XL" });
            for (int i = 8; i <= 38; i++)
            {
                var size = new Size
                {
                    Id = i,
                    Name = (i + 8).ToString(),
                };
                sizes.Add(size);
            }
            builder.Entity<Size>().HasData(sizes);
        }

        private void SeedColors(ModelBuilder builder)
        {
            List<Color> colors = new List<Color>();
            colors.Add(new Color { Id = 1, Name = "Trắng", HexCode = "#FFFFFF" });
            colors.Add(new Color { Id = 2, Name = "Đen", HexCode = "#000000" });
            colors.Add(new Color { Id = 3, Name = "Đỏ", HexCode = "#000000" });
            colors.Add(new Color { Id = 4, Name = "Vàng", HexCode = "#000000" });
            colors.Add(new Color { Id = 5, Name = "Xanh lá", HexCode = "#000000" });
            colors.Add(new Color { Id = 6, Name = "Xanh dương", HexCode = "#000000" });
            colors.Add(new Color { Id = 7, Name = "Xám", HexCode = "#000000" });
            colors.Add(new Color { Id = 8, Name = "Ghi", HexCode = "#000000" });
            colors.Add(new Color { Id = 9, Name = "Camel", HexCode = "#000000" });
            colors.Add(new Color { Id = 10, Name = "Hồng", HexCode = "#000000" });
            colors.Add(new Color { Id = 11, Name = "Tím", HexCode = "#000000" });
            colors.Add(new Color { Id = 12, Name = "Nâu", HexCode = "#000000" });
            colors.Add(new Color { Id = 13, Name = "Nâu đen", HexCode = "#000000" });

            builder.Entity<Color>().HasData(colors);
        }

        private void SeedCategories(ModelBuilder builder)
        {
            List<Category> categories = new List<Category>();
            categories.Add(new Category { Id = 1, Name = "Áo", ParentId = null });
            categories.Add(new Category { Id = 2, Name = "Quần", ParentId = null });
            categories.Add(new Category { Id = 3, Name = "Giày", ParentId = null });
            categories.Add(new Category { Id = 4, Name = "Phụ kiện", ParentId = null });
            categories.Add(new Category { Id = 5, Name = "Áo khoác", ParentId = 1 });
            categories.Add(new Category { Id = 6, Name = "Áo thun", ParentId = 1 });
            categories.Add(new Category { Id = 7, Name = "Áo sơ mi", ParentId = 1 });
            categories.Add(new Category { Id = 8, Name = "Áo polo", ParentId = 1 });
            categories.Add(new Category { Id = 9, Name = "Áo sweater", ParentId = 1 });
            categories.Add(new Category { Id = 10, Name = "Quần tây", ParentId = 2 });
            categories.Add(new Category { Id = 11, Name = "Quần jean", ParentId = 2 });
            categories.Add(new Category { Id = 12, Name = "Quần kaki", ParentId = 2 });
            categories.Add(new Category { Id = 13, Name = "Quần jogger", ParentId = 2 });
            categories.Add(new Category { Id = 14, Name = "Quần short", ParentId = 2 });
            categories.Add(new Category { Id = 15, Name = "Quần vải", ParentId = 2 });
            categories.Add(new Category { Id = 16, Name = "Giày Tây", ParentId = 3 });
            categories.Add(new Category { Id = 17, Name = "Giày Sneaker", ParentId = 3 });
            categories.Add(new Category { Id = 18, Name = "Giày Thể thao", ParentId = 3 });
            categories.Add(new Category { Id = 19, Name = "Thắt lưng", ParentId = 4 });
            categories.Add(new Category { Id = 20, Name = "Ví", ParentId = 4 });
            categories.Add(new Category { Id = 21, Name = "Cặp", ParentId = 4 });
            categories.Add(new Category { Id = 22, Name = "Tất", ParentId = 4 });
            categories.Add(new Category { Id = 23, Name = "Khăn quàng", ParentId = 4 });

            builder.Entity<Category>().HasData(categories);
        }

        private void SeedBrands(ModelBuilder builder)
        {
            List<Brand> brands = new List<Brand>();
            brands.Add(new Brand { Id = 1, Name = "Zara" });
            brands.Add(new Brand { Id = 2, Name = "H&M" });
            brands.Add(new Brand { Id = 3, Name = "Uniqlo" });
            brands.Add(new Brand { Id = 4, Name = "Gucci" });
            brands.Add(new Brand { Id = 5, Name = "Luis Vuiton" });
            brands.Add(new Brand { Id = 6, Name = "Dior" });
            brands.Add(new Brand { Id = 7, Name = "Tommy Hilfiger" });
            brands.Add(new Brand { Id = 8, Name = "John Henry" });
            brands.Add(new Brand { Id = 9, Name = "Nike" });
            brands.Add(new Brand { Id = 10, Name = "Addidas" });
            brands.Add(new Brand { Id = 11, Name = "New Balance" });
            brands.Add(new Brand { Id = 12, Name = "Biti's Hunter" });
            brands.Add(new Brand { Id = 13, Name = "Frcnk" });

            builder.Entity<Brand>().HasData(brands);
        }
    }
}

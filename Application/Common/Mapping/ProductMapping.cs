using Domain.Entities;
using Shared.Products;

namespace Application.Common.Mapping
{
    public static class ProductMapping
    {
        public static Product ToProduct(this AddProductReq req)
        {
            var product = new Product
            {
                Name = req.Name,
                Description = req.Description,
                Price = req.Price,
                SalePrice = req.SalePrice,
                IsFeatured = req.IsFeatured,
                Material = req.Material,
                Style = req.Style,
                CollectionId = req.CollectionId,
                BrandId = req.BrandId,
                Gender = req.Gender,
                CategoryId = req.CategoryId,
                CreatedAt = DateTimeOffset.UtcNow,
                Variants = req.Variants?.Select(p => p.ToVariant()).ToList() ?? []
            };
            return product;
        }
        public static ProductVariant ToVariant(this AddProductVariantReq req)
        {
            var variant = new ProductVariant
            {
                ColorId = req.ColorId,
                CreatedAt = DateTimeOffset.UtcNow,
                ImageUrls = req.ImageUrls,
                Inventories = req.Stock.ToInventories()
            };
            return variant;
        }

        public static ProductDto ToProductDto(this Product product)
        {
            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Material = product.Material,
                Style = product.Style,
                SalePrice = product.SalePrice,
                Price = product.Price,
                CategoryId = product.CategoryId,
                Category = product.Category?.Name,
                CollectionId = product.CollectionId,
                Collection = product.Collection?.Name,
                BrandId = product.BrandId,
                Brand = product.Brand?.Name,
                BoxSize = product.BoxSize,
                BoxWeight = product.BoxWeight,
                Gender = product.Gender,
                IsFeatured = product.IsFeatured,
                Images = product.Images ?? [],
                Variants = product.Variants?.ToVariantDtos()
            };
        }

        public static VariantDto ToVariantDto(this ProductVariant variant)
        {
            return new VariantDto
            {
                Id = variant.Id,
                ProductId = variant.ProductId,
                ColorId = variant.ColorId,
                Color = variant.Color?.Name,
                ColorHex = variant.Color?.HexCode,
                ImageUrls = variant.ImageUrls,
                Images = variant.Images ?? [],
                Inventories = variant.Inventories?.Select(i => new InventoryDto
                {
                    SizeId = i.SizeId,
                    Size = i.Size?.Name ?? "",
                    Quantity = i.Quantity,
                    Price = i.Price
                }) ?? []
            };
        }

        public static List<VariantDto> ToVariantDtos(this ICollection<ProductVariant>? variants)
        {
            return variants?.Select(v => v.ToVariantDto()).ToList() ?? new List<VariantDto>();
        }

        public static List<Inventory> ToInventories(this Dictionary<int,int> stock)
        {
            List<Inventory> inventory = new List<Inventory>();
            foreach (var item in stock)
            {
                inventory.Add(new Inventory
                {
                    SizeId = item.Key,
                    Quantity = item.Value,
                });
            }
            return inventory;
        }
    }
}

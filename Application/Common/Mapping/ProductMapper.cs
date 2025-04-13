using Domain.Entities;
using Shared.Products;

namespace Application.Common.Mapping
{
    public static class ProductMapper
    {
        public static Product ToProduct(this AddProductReq req)
        {
            var product = new Product
            {
                Name = req.Name,
                Description = req.Description,
                Price = req.Price,
                IsFeatured = req.IsFeatured,
                Material = req.Material,
                Gender = req.Gender,
                BoxSize = req.BoxSize,
                BoxWeight = req.BoxWeight,
                CategoryId = req.CategoryId,
                CreatedAt = DateTimeOffset.UtcNow,
                Variants = req.Variants?.Select(p => p.ToVariant()).ToList()
            };
            return product;
        }
        public static ProductVariant ToVariant(this AddProductVariantReq req)
        {
            var variant = new ProductVariant
            {
                Size = req.Size,
                ColorId = req.ColorId,
                CreatedAt = DateTimeOffset.UtcNow,
                ImageUrls = req.ImageUrls
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
                Price = product.Price,
                CategoryId = product.CategoryId,
                CategoryName = product.Category?.Name,
                BoxSize = product.BoxSize,
                BoxWeight = product.BoxWeight,
                Gender = product.Gender,
                IsFeatured = product.IsFeatured,
                Images = product.Images ?? [],
                Tags = product.ProductTags?.Select(pt => new TagDto
                {
                    TagId = pt.TagId,
                    TagName = pt.Tag?.Name ?? string.Empty
                }) ?? [],
                Variants = product.Variants.ToVariantDtos()
            };
        }

        public static VariantDto ToVariantDto(this ProductVariant variant)
        {
            return new VariantDto
            {
                Id = variant.Id,
                ProductId = variant.ProductId,
                Size = variant.Size,
                ColorId = variant.ColorId,
                ColorName = variant.Color?.Name,
                ColorHex = variant.Color?.HexCode,
                ImageUrls = variant.ImageUrls,
                Images = variant.Images ?? [],
                Inventories = variant.Inventories?.Select(i => new InventoryDto
                {
                    BranchId = i.BranchId,
                    BranchName = i.Branch?.Name ?? "", 
                    Quantity = i.Quantity
                }) ?? []
            };
        }

        public static List<VariantDto> ToVariantDtos(this ICollection<ProductVariant>? variants)
        {
            return variants?.Select(v => v.ToVariantDto()).ToList() ?? new List<VariantDto>();
        }
    }
}

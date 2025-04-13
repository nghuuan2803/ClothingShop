using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.Features.Products.Commands
{
    public class AddVariantCommand : IRequest<int>
    {
        public int ProductId { get; set; }
        public int ColorId { get; set; }
        public required string Size { get; set; }
        public string? ImageUrls { get; set; }
    }

    public class AddVariantCommandHandler : IRequestHandler<AddVariantCommand, int>
    {
        private IRepository<ProductVariant> _variantRepo;
        private IRepository<Product> _productRepo;

        public AddVariantCommandHandler(IRepository<ProductVariant> variantRepo, IRepository<Product> productRepo)
        {
            _variantRepo = variantRepo;
            _productRepo = productRepo;
        }

        public async Task<int> Handle(AddVariantCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepo.GetByIdAsync(request.ProductId);
            if (product == null)
                throw new ApplicationException("Product not found!");

            var variant = await _variantRepo
                .GetSingleAsync(p => p.ProductId == request.ProductId
                && p.ColorId == request.ColorId
                && p.Size == request.Size);
            if (variant != null && !variant.IsDeleted)
                throw new ApplicationException("Variant already exist!");
            if (variant != null && variant.IsDeleted)
            {
                variant.IsDeleted = false;
                variant.UpdatedAt = DateTimeOffset.UtcNow;
                return variant.Id;
            }
            variant = new ProductVariant
            {
                ProductId = product.Id,
                ColorId = request.ColorId,
                Size = request.Size,
                ImageUrls = request.ImageUrls,
                CreatedAt = DateTimeOffset.UtcNow,
            };
            await _variantRepo.AddAsync(variant);
            await _variantRepo.SaveChangesAsync();
            return variant.Id;
        }
    }
}

using Domain.Entities;
using Domain.Repositories;
using MediatR;
using Shared.Enums;

namespace Application.Features.Products.Commands
{
    public class UpdateProductCommand : IRequest<string>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string Material { get; set; }
        public string Style { get; set; }
        public int? CollectionId { get; set; }
        public int? BrandId { get; set; }
        public decimal Price { get; set; }
        public decimal SalePrice { get; set; }
        public int CategoryId { get; set; }
        public Gender Gender { get; set; }
        public bool IsFeatured { get; set; }
        public IEnumerable<int>? Tags { get; set; }
    }
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, string>
    {
        private IRepository<Product> _productRepo;

        public UpdateProductCommandHandler(IRepository<Product> productRepo)
        {
            _productRepo = productRepo;
        }

        public async Task<string> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepo.GetByIdAsync(request.Id);
            if (product == null)
                 return "Product not found!";
            product.Name = request.Name;
            product.Description = request.Description;
            product.Gender = request.Gender;
            product.IsFeatured = request.IsFeatured;
            product.Material = request.Material;
            product.Style = request.Style;
            product.CollectionId = request.CollectionId;
            product.BrandId = request.BrandId;
            product.Price = request.Price;
            product.CategoryId = request.CategoryId;

            _productRepo.Update(product);
            await _productRepo.SaveChangesAsync();
            return "Success";
        }
    }
}

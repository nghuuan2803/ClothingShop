using Domain.Entities;
using Domain.Repositories;
using MediatR;
using Shared.Enums;

namespace Application.Features.Products.Commands
{
    public class UpdateProductCommand : IRequest<string>
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Material { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public int BoxSize { get; set; }
        public int BoxWeight { get; set; }
        public Gender Gender { get; set; }
        public bool IsFeatured { get; set; }
        public IEnumerable<int>? Tags { get; set; }
    }
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, string>
    {
        private IRepository<Product> _productRepo;
        private IRepository<Tag> _tagRepo;
        private IRepository<ProductTag> _productTagRepo;

        public UpdateProductCommandHandler(IRepository<Product> productRepo, IRepository<Tag> tagRepo, IRepository<ProductTag> productTagRepo)
        {
            _productRepo = productRepo;
            _tagRepo = tagRepo;
            _productTagRepo = productTagRepo;
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
            product.Price = request.Price;
            product.CategoryId = request.CategoryId;
            product.BoxSize = request.BoxSize;
            product.BoxWeight = request.BoxWeight;


            var tags = await _productTagRepo.GetAllAsync(p => p.ProductId == product.Id);
            _productTagRepo.DeleteRange(tags);
            product.ProductTags = [];
            foreach (int tagId in request.Tags)
            {
                var tag = await _tagRepo.GetByIdAsync(tagId);
                if (tag == null)
                    return "Tag not found!. Id: " + tagId;
                var proTag = new ProductTag
                {
                    ProductId = product.Id,
                    TagId = tagId
                };
            }
            _productRepo.Update(product);
            await _productRepo.SaveChangesAsync();
            return "Success";
        }
    }
}

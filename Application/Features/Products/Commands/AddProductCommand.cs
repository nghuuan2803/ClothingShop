using Application.Common.Mapping;
using Domain.Entities;
using Domain.Repositories;
using MediatR;
using Shared.Enums;
using Shared.Products;

namespace Application.Features.Products.Commands
{
    public class AddProductCommand : IRequest<int>
    {
        public AddProductReq Payload { get; set; }
    }
    public class AddProductCommandHandler : IRequestHandler<AddProductCommand, int>
    {
        private IRepository<Product> _productRepo;

        public AddProductCommandHandler(IRepository<Product> productRepo)
        {
            _productRepo = productRepo;
        }

        public async Task<int> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            var product = request.Payload.ToProduct();
            if (product.Variants.Count < 1)
                return -1;
            await _productRepo.AddAsync(product);
            await _productRepo.SaveChangesAsync();
            return product.Id;
        }
    }
}

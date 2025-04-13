using Application.Common.Mapping;
using Domain.Entities;
using Domain.Repositories;
using MediatR;
using Shared.Products;

namespace Application.Features.Products.Queries
{
    public record GetProductQuery(int id) : IRequest<ProductDto>
    {
    }
    public class GetProductQueryHandler : IRequestHandler<GetProductQuery, ProductDto>
    {
        private IRepository<Product> _repo;

        public GetProductQueryHandler(IRepository<Product> repo)
        {
            _repo = repo;
        }

        public async Task<ProductDto> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            var product = await _repo.GetSingleAsync(p => p.Id == request.id);
            if (product == null)
                throw new ApplicationException("Product not found!");
            var dto = product.ToProductDto();
            return dto;
        }
    }
}

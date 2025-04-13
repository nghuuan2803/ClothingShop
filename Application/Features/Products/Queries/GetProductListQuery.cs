using Application.Common.Mapping;
using Domain.Entities;
using Domain.Repositories;
using MediatR;
using Shared.Products;

namespace Application.Features.Products.Queries
{
    public class GetProductListQuery : IRequest<IEnumerable<ProductDto>>
    {
    }

    public class GetProductListQueryHandler : IRequestHandler<GetProductListQuery, IEnumerable<ProductDto>>
    {
        private IRepository<Product> _repo;

        public GetProductListQueryHandler(IRepository<Product> repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<ProductDto>> Handle(GetProductListQuery request, CancellationToken cancellationToken)
        {
            var products = await _repo.GetAllAsync(null!, cancellationToken);
            var data = products.Select(p => p.ToProductDto());

            return data;
        }
    }
}

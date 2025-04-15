using Application.Common.Mapping;
using Domain.Entities;
using Domain.Repositories;
using MediatR;
using Shared.Orders;

namespace Application.Features.Orders.Queries
{
    public class GetAllOrdersQuery : IRequest<IEnumerable<OrderDto>>
    {
    }

    public class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQuery, IEnumerable<OrderDto>>
    {
        private readonly IRepository<Order> _repository;

        public GetAllOrdersQueryHandler(IRepository<Order> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<OrderDto>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
        {
            var data = await _repository.GetAllAsync(null!, cancellationToken);
            if (data != null)
            {
                return data.Select(p => p.ToOrderDto());
            }
            return [];
        }
    }
}

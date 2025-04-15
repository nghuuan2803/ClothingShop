using Application.Common.Mapping;
using Domain.Entities;
using Domain.Repositories;
using MediatR;
using Shared.Orders;

namespace Application.Features.Orders.Queries
{
    public record GetOrderQuery(string Id) : IRequest<OrderDto>
    {
    }

    public class GetOrderQueryHandler : IRequestHandler<GetOrderQuery, OrderDto>
    {
        private readonly IRepository<Order> _repository;

        public GetOrderQueryHandler(IRepository<Order> repository)
        {
            _repository = repository;
        }

        public async Task<OrderDto> Handle(GetOrderQuery request, CancellationToken cancellationToken)
        {
            var order = await _repository.GetSingleAsync(p=>p.Id== request.Id);
            if (order == null)
                return null!;
            return order.ToOrderDto();
        }
    }
}

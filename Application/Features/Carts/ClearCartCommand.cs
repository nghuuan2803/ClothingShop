using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.Features.Carts
{
    public record ClearCartCommand(string CustomerId) : IRequest
    {
    }

    public class ClearCartCommandHandler : IRequestHandler<ClearCartCommand>
    {
        private readonly IRepository<CartItem> _repository;

        public ClearCartCommandHandler(IRepository<CartItem> repository)
        {
            _repository = repository;
        }

        public async Task Handle(ClearCartCommand request, CancellationToken cancellationToken)
        {
            var cart = await _repository.GetAllAsync(p => p.CustomerId == request.CustomerId);
            if (cart != null)
            {
                _repository.DeleteRange(cart);
                await _repository.SaveChangesAsync(cancellationToken);
            }
        }
    }
}

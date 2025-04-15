using Domain.Entities;
using Domain.Repositories;
using MediatR;
using Shared.Carts;

namespace Application.Features.Carts
{
    public record GetCartQuery(string CustomerId) : IRequest<CartDto>
    {
    }

    public class GetCartQueryHandler : IRequestHandler<GetCartQuery, CartDto>
    {
        private IRepository<CartItem> _cartRepo;

        public GetCartQueryHandler(IRepository<CartItem> cartRepo)
        {
            _cartRepo = cartRepo;
        }

        //private IRepository<ProductVariant> _variantRepo;
        public async Task<CartDto> Handle(GetCartQuery request, CancellationToken cancellationToken)
        {
            var cartItems = await _cartRepo.GetAllAsync(p => p.CustomerId == request.CustomerId);
            if (!cartItems.Any())
                throw new ApplicationException("Cart not found!");

            var cartDto = new CartDto
            {
                CustomerId = request.CustomerId,
                Items = cartItems.Select(p => new CartItemDto
                {
                    VariantId = p.VariantId,
                    SizeId = p.SizeId,
                    Size = p.Size?.Name,
                    Color = p.Variant?.Color?.Name,
                    Product = p.Variant?.Product?.Name,
                    Price = p.Variant?.Product?.Price ?? 0,
                    Quantity = p.Quantity,
                })
            };

            return cartDto;
        }
    }
}

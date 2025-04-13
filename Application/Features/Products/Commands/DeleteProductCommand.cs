using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.Features.Products.Commands
{
    public record DeleteProductCommand(int id) : IRequest<string>
    {
    }
    public class DeleteProductCommandHandler(IRepository<Product> repo) : IRequestHandler<DeleteProductCommand, string>
    {
        public async Task<string> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await repo.GetByIdAsync(request.id);
            if (product == null)
                return "Product not found!";
            product.IsDeleted = true;
            product.UpdatedAt = DateTimeOffset.UtcNow;
            repo.Update(product);
            await repo.SaveChangesAsync();
            return "Success";
        }
    }
}

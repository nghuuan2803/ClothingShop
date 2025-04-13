using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.Features.Products.Commands
{
    public record DeleteVariantCommand(int id): IRequest<string>
    {
    }

    public class DeleteVariantCommandHandler : IRequestHandler<DeleteVariantCommand, string>
    {
        private IRepository<ProductVariant> _repo;

        public DeleteVariantCommandHandler(IRepository<ProductVariant> repo)
        {
            _repo = repo;
        }

        public async Task<string> Handle(DeleteVariantCommand request, CancellationToken cancellationToken)
        {
            var variant = await _repo.GetByIdAsync(request.id);
            if (variant == null)
                return "Variant not found!";
            _repo.Delete(variant);
            await _repo.SaveChangesAsync();
            return "Success";
        }
    }
}

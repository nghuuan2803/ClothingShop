using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.Features.Products.Commands
{
    public class UpdateVariantCommand : IRequest<string>
    {
        public int Id { get; set; }
        public int ColorId { get; set; }
        public required string Size { get; set; }
        public string? ImageUrls { get; set; }
    }
    public class UpdateVariantCommandHandler : IRequestHandler<UpdateVariantCommand, string>
    {
        private IRepository<ProductVariant> _repo;

        public UpdateVariantCommandHandler(IRepository<ProductVariant> repo)
        {
            _repo = repo;
        }

        public async Task<string> Handle(UpdateVariantCommand request, CancellationToken cancellationToken)
        {
            var variant = await _repo.GetByIdAsync(request.Id);
            if (variant == null)
                return "Variant not found!";
            variant.ColorId = request.ColorId;
            variant.Size = request.Size;
            variant.ImageUrls = request.ImageUrls;

            _repo.Update(variant);
            await _repo.SaveChangesAsync();
            return "Success";
        }
    }
}

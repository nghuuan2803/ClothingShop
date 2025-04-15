using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.Features.Products.Commands
{
    public class UpdateVariantCommand : IRequest<string>
    {
        public int Id { get; set; }
        public int ColorId { get; set; }
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
            var otherVariants = await _repo.GetAllAsync(p=>p.Id == request.Id&&p.ColorId==request.ColorId);
            if (otherVariants.Any())
            {
                return "There is another varian of this color";                
            }
            variant.ColorId = request.ColorId;
            variant.ImageUrls = request.ImageUrls;

            _repo.Update(variant);
            await _repo.SaveChangesAsync();
            return "Success";
        }
    }
}

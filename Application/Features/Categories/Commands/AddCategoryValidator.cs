using FluentValidation;

namespace Application.Features.Categories.Commands
{
    public class AddCategoryValidator : AbstractValidator<AddCategoryCommand>
    {
        public AddCategoryValidator()
        {
            RuleFor(x => x.Request.Name)
                .NotEmpty().WithMessage("Name is required.");

            RuleFor(x => x.Request.ParentId)
                .GreaterThan(0).WithMessage("ParentId must be a positive value.");
        }
    }
}

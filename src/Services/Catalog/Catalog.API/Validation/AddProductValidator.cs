using Catalog.API.DTO.Request;
using FluentValidation;

namespace Catalog.API.Validation
{
    public class AddProductRequestValidator : AbstractValidator<AddProductRequest>
    {
        public AddProductRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .MinimumLength(5)
                .MaximumLength(30);

            RuleFor(x => x.Description)
                .NotNull()
                .NotEmpty()
                .MinimumLength(30)
                .MaximumLength(300);

            RuleFor(x => x.Category)
                .NotNull()
                .NotEmpty()
                .MinimumLength(5)
                .MaximumLength(15);

            RuleFor(x => x.ImageFile)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.Summary)
                .NotNull()
                .NotEmpty()
                .MinimumLength(5)
                .MaximumLength(50);

            RuleFor(x => x.Price)
                .NotNull()
                .GreaterThan(0);
        }
    }
}
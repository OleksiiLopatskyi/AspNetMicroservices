using Catalog.API.DTO.Request;
using FluentValidation;

namespace Catalog.API.Validation
{
    public class UpdateProductRequestValidator : AbstractValidator<UpdateProductRequest>
    {
        public UpdateProductRequestValidator()
        {
            RuleFor(x => x.Name)
             .MinimumLength(5)
             .MaximumLength(30);

            RuleFor(x => x.Description)
                .MinimumLength(30)
                .MaximumLength(300);

            RuleFor(x => x.Category)
                .MinimumLength(5)
                .MaximumLength(15);

            RuleFor(x => x.Summary)
                .MinimumLength(5)
                .MaximumLength(50);

            When(x => x.Price != null, () =>
            {
                RuleFor(x => x.Price).GreaterThan(0);
            });

            When(x => x.ImageFile != null, () =>
            {
                RuleFor(x => x.ImageFile)
                .NotEmpty();
            });
        }
    }
}

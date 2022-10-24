using Basket.Data.Entities;
using FluentValidation;

namespace Basket.Web.Validation
{
    public class ShoppingCartValidator : AbstractValidator<ShoppingCart>
    {
        public ShoppingCartValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty()
                .NotNull();

            RuleFor(x=>x.Items)
                .NotEmpty()
                .NotNull();
        }
    }
}

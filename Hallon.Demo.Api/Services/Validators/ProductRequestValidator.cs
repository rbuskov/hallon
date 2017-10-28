using FluentValidation;
using Hallon.Demo.Common;

namespace Hallon.Demo.Services.Validators
{
    public class ProductRequestValidator : AbstractValidator<ProductRequest>
    {
        public ProductRequestValidator()
        {
            RuleFor(product => product.Name).NotEmpty();
            RuleFor(product => product.Price).GreaterThanOrEqualTo(0);
        }
    }
}
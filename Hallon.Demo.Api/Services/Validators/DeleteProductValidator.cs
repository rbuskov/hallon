using System.Linq;
using FluentValidation;
using Hallon.Demo.Data;

namespace Hallon.Demo.Services.Validators
{
    public class DeleteProductValidator : AbstractValidator<int>
    {
        public DeleteProductValidator()
        {
            RuleFor(id => id)
                .Must(id => Repository.OrderLines.Any(line => line.Product.Id == id))
                .WithMessage("Products that have been ordered cannot be deleted.");
        }
    }
}
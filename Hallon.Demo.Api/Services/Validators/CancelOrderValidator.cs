using System.Linq;
using FluentValidation;
using Hallon.Demo.Common;
using Hallon.Demo.Data;

namespace Hallon.Demo.Services.Validators
{
    public class CancelOrderValidator : AbstractValidator<int>
    {
        public CancelOrderValidator()
        {
            RuleFor(id => id)
                .Must(id => Repository.Orders.Any(order => order.Id == id && (order.Status == OrderStatus.Draft || order.Status == OrderStatus.Confirmed)))
                .WithMessage("Only orders with status 'draft' or 'confirmed' can be cancelled.");
        }
    }
}
using System.Linq;
using FluentValidation;
using Hallon.Demo.Common;
using Hallon.Demo.Data;

namespace Hallon.Demo.Services.Validators
{
    public class ConfirmOrderValidator : AbstractValidator<int>
    {
        public ConfirmOrderValidator()
        {
            RuleFor(id => id)
                .Must(id => Repository.Orders.Any(order => order.Id == id && order.Status == OrderStatus.Draft))
                .WithMessage("Only orders with status 'draft' can be confirmed.");
        }
    }
}
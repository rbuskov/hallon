using System.Linq;
using FluentValidation;
using Hallon.Demo.Common;
using Hallon.Demo.Data;

namespace Hallon.Demo.Services.Validators
{
    public class ShipOrderValidator : AbstractValidator<int>
    {
        public ShipOrderValidator()
        {
            RuleFor(id => id)
                .Must(id => Repository.Orders.Any(order => order.Id == id && order.Status == OrderStatus.Confirmed))
                .WithMessage("Only orders with status 'confirmed' can be shipped.");
        }
    }
}
using System.Linq;
using FluentValidation;
using Hallon.Demo.Data;

namespace Hallon.Demo.Services.Validators
{
    public class DeleteCustomerValidator : AbstractValidator<int>
    {
        public DeleteCustomerValidator()
        {
            RuleFor(id => id)
                .Must(id => Repository.Customers.Any(customer => customer.Id == id && customer.Orders.Any()))
                .WithMessage("Customers that have orders cannot be deleted.");
        }
    }
}
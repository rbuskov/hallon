using System.Linq;
using FluentValidation;
using Hallon.Demo.Common;
using Hallon.Demo.Data;

namespace Hallon.Demo.Services.Validators
{
    public class OrderRequestValidator : AbstractValidator<OrderRequest>
    {
        public OrderRequestValidator()
        {
            RuleFor(request => request.CustomerId)
                .Must(id => Repository.Customers.Any(customer => customer.Id == id))
                .WithMessage("Customer does not exist.");

            RuleFor(request => request.Lines).SetCollectionValidator(new LineValidator());
        }

        public class LineValidator : AbstractValidator<OrderRequest.Line>
        {
            public LineValidator()
            {
                RuleFor(line => line.Quantity).GreaterThan(0);

                RuleFor(line => line.ProductId)
                    .Must(id => Repository.Products.Any(product => product.Id == id))
                    .WithMessage(id => $"Product with ID '{id}' does not exist.");
            }
        }
    }
}
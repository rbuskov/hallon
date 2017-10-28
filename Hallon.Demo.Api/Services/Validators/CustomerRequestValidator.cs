using FluentValidation;
using Hallon.Demo.Common;

namespace Hallon.Demo.Services.Validators
{
    public class CustomerRequestValidator : AbstractValidator<CustomerRequest>
    {
        public CustomerRequestValidator()
        {
            RuleFor(request => request.Name).NotEmpty();

            RuleFor(request => request.Address).NotNull();
            RuleFor(request => request.Address.Street).NotEmpty().When(r => r.Address != null);
            RuleFor(request => request.Address.PostalCode).NotEmpty().When(r => r.Address != null);
            RuleFor(request => request.Address.City).NotEmpty().When(r => r.Address != null);
            RuleFor(request => request.Address.Country).IsInEnum().When(r => r.Address != null);
        }
    }
}
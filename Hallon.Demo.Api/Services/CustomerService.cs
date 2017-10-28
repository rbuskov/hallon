using Hallon.Demo.Data;
using Hallon.Demo.Common;
using Hallon.Demo.Services.Validators;

namespace Hallon.Demo.Services
{
    public class CustomerService : DemoService<Customer>
    {
        public CustomerService() : base(Repository.Customers)
        { }

        public ServiceResult<Customer> Create(CustomerRequest request)
            => Create<CustomerRequest, CustomerRequestValidator>(request, () => new Customer
            {
                Id = Repository.NextCustomerId,
                Name = request.Name,
                Address = request.Address
            });

        public ServiceResult<Customer> Update(int id, CustomerRequest request)
            => Update<CustomerRequest, CustomerRequestValidator>(id, request, customer =>
            {
                customer.Name = request.Name;
                customer.Address = request.Address;
            });

        public ServiceResult<Customer> Delete(int id)
            => DeleteById<DeleteCustomerValidator>(id);
    }
}
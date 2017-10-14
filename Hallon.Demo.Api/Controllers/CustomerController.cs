using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Hallon.Demo.Resources;

namespace Hallon.Demo.Controllers
{
    [RoutePrefix("api")]
    public class CustomerController : ApiController
    {
        [HttpGet, Route("customer/{id}")]
        public Customer Get(int id)
        {
            return Repository.Customers.FirstOrDefault(customer => customer.Id == id);
        }

        [HttpGet, Route("customers")]
        public IEnumerable<Customer> Get()
        {
            return Repository.Customers;
        }
    }
}

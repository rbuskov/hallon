using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using Hallon.Demo.Data;
using Hallon.Demo.Resources;

namespace Hallon.Demo.Controllers
{
    [RoutePrefix("api")]
    public class CustomerController : ApiController
    {
        [HttpGet, Route("customers/{id}")]
        public CustomerResource Get(int id)
        {
            var customer = Repository.Customers.SingleOrDefault(c => c.Id == id) 
                ?? throw new HttpResponseException(HttpStatusCode.BadRequest);

            return customer.ToResource();
        }

        [HttpGet, Route("customers")]
        public IEnumerable<CustomerResource> Get()
        {
            return Repository.Customers.Select(c => c.ToResource());
        }
    }
}

using System.Web.Http;
using Hallon.Demo.Data;
using Hallon.Demo.Common;
using Hallon.Demo.Services;

namespace Hallon.Demo.Controllers
{
    [RoutePrefix("api")]
    public class CustomerController : DemoController<Customer, CustomerResource, CustomerSummaryResource>
    {
        private readonly CustomerService service;

        public CustomerController() 
            => this.service = new CustomerService();

        // Get

        [HttpGet, Route("customers")]
        public IHttpActionResult Get()
            => Handle(service.Get());

        [HttpGet, Route("customers/{id}")]
        public IHttpActionResult Get(int id)
            => Handle(service.Get(id));

        // Post, Put, Delete

        [HttpPost, Route("customers")]
        public IHttpActionResult Create(CustomerRequest request)
            => Handle(service.Create(request));

        [HttpPut, Route("customers/{id}")]
        public IHttpActionResult Update(CustomerRequest request)
            => Handle(service.Update(request));

        [HttpDelete, Route("customers/{id}")]
        public IHttpActionResult Delete(int id)
            => Handle(service.Delete(id));
    }
}

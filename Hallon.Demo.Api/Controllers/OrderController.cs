using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using Hallon.Demo.Resources;

namespace Hallon.Demo.Controllers
{
    [RoutePrefix("api")]
    public class OrderController : ApiController
    {
        [HttpGet, Route("orders/{id}")]
        public Order Get(int id)
        {
            var order = Repository.Orders.FirstOrDefault(o => o.Id == id) 
                ?? throw new HttpResponseException(HttpStatusCode.BadRequest);

            order.AddLink("self", $"/orders/{id}");
            order.AddLink("customer", $"/customers/{order.Customer.Id}");

            return order;
        }

        [HttpGet, Route("orders")]
        public IEnumerable<Order> Get()
        {
            return Repository.Orders;
        }

        [HttpGet, Route("customers/{id}/orders")]
        public IEnumerable<Order> GetByCustomer(int id)
        {
            return Repository.Orders.Where(order => order.Customer.Id == id);
        }
    }
}
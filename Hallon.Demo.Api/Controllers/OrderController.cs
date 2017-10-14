using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Hallon.Demo.Resources;

namespace Hallon.Demo.Controllers
{
    [RoutePrefix("api/orders")]
    public class OrderController : ApiController
    {
        private static readonly List<Order> Orders = new List<Order>();

        static OrderController()
        {
            Orders.Add(new Order() { Id = 1, Date = new DateTime(2017, 10, 1)});
            Orders.Add(new Order() { Id = 2, Date = new DateTime(2017, 10, 3)});
            Orders.Add(new Order() { Id = 3, Date = new DateTime(2017, 10, 5) });
            Orders.Add(new Order() { Id = 4, Date = new DateTime(2017, 10, 7) });

            foreach (var order in Orders)
                order.AddLink("self", $"/api/orders/{order.Id}");
        }

        [HttpGet, Route("{id}")]
        public Order Get(int id)
        {
            return Orders.FirstOrDefault(o => o.Id == id);
        }

        [HttpGet, Route("")]
        public IEnumerable<Order> Get()
        {
            return Orders;
        }
    }
}

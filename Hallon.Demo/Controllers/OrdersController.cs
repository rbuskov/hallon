using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Hallon.Demo.Models;

namespace Hallon.Demo.Controllers
{
    [RoutePrefix("api/orders")]
    public class OrdersController : ApiController
    {
        private static readonly List<Order> Orders = new List<Order>();

        static OrdersController()
        {
            Orders.Add(new Order() { Id = 1, Date = new DateTime(2017, 10, 1)});
            Orders.Add(new Order() { Id = 2, Date = new DateTime(2017, 10, 2)});
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

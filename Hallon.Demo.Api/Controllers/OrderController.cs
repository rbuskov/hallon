using System.Web.Http;
using Hallon.Demo.Data;
using Hallon.Demo.Common;
using Hallon.Demo.Services;

namespace Hallon.Demo.Controllers
{
    [RoutePrefix("api")]
    public class OrderController : DemoController<Order, OrderResource>
    {
        private readonly OrderService service;

        public OrderController() 
            => service = new OrderService();

        // Get

        [HttpGet, Route("orders")]
        public IHttpActionResult Get()
            => Handle(service.Get());
 
        [HttpGet, Route("orders/{id}")]
        public IHttpActionResult Get(int id) 
            => Handle(service.Get(id));

        [HttpGet, Route("customers/{id}/orders")]
        public IHttpActionResult GetByCustomer(int id)
            => Handle(service.GetByCustomer(id));

        // Post

        [HttpPost, Route("orders")]
        public IHttpActionResult Create([FromBody] OrderRequest request)
            => Handle(service.Create(request));

        [HttpPost, Route("orders/{id}/confirm")]
        public IHttpActionResult Confirm(int id)
            => Handle(service.Confirm(id));

        [HttpPost, Route("orders/{id}/ship")]
        public IHttpActionResult Ship(int id)
            => Handle(service.Ship(id));

        [HttpPost, Route("orders/{id}/cancel")]
        public IHttpActionResult Cancel(int id)
            => Handle(service.Cancel(id));
    }
}
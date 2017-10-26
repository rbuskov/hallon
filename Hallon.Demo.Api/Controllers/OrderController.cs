using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using AutoMapper;
using Hallon.Demo.Data;
using Hallon.Demo.Resources;

namespace Hallon.Demo.Controllers
{
    [RoutePrefix("api")]
    public class OrderController : ApiController
    {
        [HttpGet, Route("orders")]
        public IHttpActionResult Get()
        {
            return Ok(Repository.Orders.Select(Mapper.Map<OrderResource>));
        }

        [HttpGet, Route("orders/{id}")]
        public IHttpActionResult Get(int id)
        {
            var order = Repository.Orders.FirstOrDefault(o => o.Id == id) 
                ?? throw new HttpResponseException(HttpStatusCode.NotFound);

            return Ok(Mapper.Map<OrderResource>(order));
        }

        [HttpGet, Route("customers/{id}/orders")]
        public IHttpActionResult GetByCustomer(int id)
        {
            switch (Repository.FindCustomer(id))
            {
                case Success<Customer> _:
                    return Ok(Repository.Orders.Where(order => order.Customer.Id == id).Select(Mapper.Map<OrderResource>));
                case Failure failure:
                    return BadRequest(failure.Message);
            }
        }

        [HttpPost, Route("orders")]
        public IHttpActionResult Create([FromBody] dynamic request)
        {
            var customer = Repository.Customers.FirstOrDefault(c => c.Id == request.CustomerId);

            if (customer == null)
                return BadRequest($"Customer with ID '{request.CustomerId}' not found.");

            var order = Repository.InsertOrder(customer);

            return Ok(Mapper.Map<OrderResource>(order));
        }

        [HttpPost, Route("orders/{id}/confirm")]
        public IHttpActionResult Confirm(int id)
        {
            var order = Repository.Orders.FirstOrDefault(o => o.Id == id);

            if (order == null)
                return BadRequest($"Order with ID '{id}' not found.");

            if (order.Status != OrderStatus.Draft)
                return BadRequest($"Order '{id}' has status '{order.Status.ToString()}' and cannot be confirmed.");

            order.Status = OrderStatus.Confirmed;
            order.ConfirmedDate = DateTime.UtcNow;

            order = Repository.UpdateOrder(order);

            return Ok(Mapper.Map<OrderResource>(order));
        }

        [HttpPost, Route("orders/{id}/ship")]
        public IHttpActionResult Ship(int id)
        {
            var order = Repository.Orders.FirstOrDefault(o => o.Id == id);

            if (order == null)
                return BadRequest($"Order with ID '{id}' not found.");

            if (order.Status != OrderStatus.Confirmed)
                return BadRequest($"Order '{id}' has status '{order.Status.ToString()}' and cannot be shipped.");

            order.Status = OrderStatus.Shipped;
            order.ShippedDate = DateTime.UtcNow;

            order = Repository.UpdateOrder(order);

            return Ok(Mapper.Map<OrderResource>(order));
        }

        [HttpPost, Route("orders/{id}/cancel")]
        public IHttpActionResult Cancel(int id)
        {
            switch (Repository.FindOrder(id))
            {
                case Success result:
                    return Ok(Mapper.Map<OrderResource>(result.Value));
                case Failure failure:
                    return BadRequest(failure.Message);
            }
        }
    }
}
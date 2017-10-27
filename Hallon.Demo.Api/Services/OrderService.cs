using System;
using System.Collections.Generic;
using System.Linq;
using Hallon.Demo.Data;
using Hallon.Demo.Resources;

namespace Hallon.Demo.Services
{
    public class OrderService
    {
        public IEnumerable<Order> Get() 
            => Repository.Orders;

        public ServiceResult<Order> Get(int id)
        {
            var order = Repository.Orders.FirstOrDefault(o => o.Id == id);

            if (order == null)
                return new ServiceResult<Order>($"Order with ID '{id}' not found.");

            return new ServiceResult<Order>(order);
        }

        public ServiceResult<Order> Insert(CreateOrderRequest request)
        {
            var customer = Repository.Customers.FirstOrDefault(c => c.Id == request.CustomerId);

            if (customer == null)
                return ServiceResult<Order>.CustomerNotFound(request.CustomerId);

            var order = new Order
            {
                Id = Repository.Orders.Max(o => o.Id) + 1,
                Customer = customer,
                OrderDate = DateTime.UtcNow,
                Status = OrderStatus.Draft
            };

            foreach (var requestLine in request.Lines)
            {
                var product = Repository.Products.SingleOrDefault(p => p.Id == requestLine.ProductId);

                if (product == null)
                    return ServiceResult<Order>.ProductNotFound(requestLine.ProductId);

                var orderLine = new OrderLine
                {
                    Id = Repository.NextOrderLineId,
                    Order = order,
                    Quantity = requestLine.Quantity,
                    Product = product
                };

                order.Lines.Add(orderLine);                    
            }

            Repository.Orders.Add(order);

            return new ServiceResult<Order>(order);
        }

        public ServiceResult<Order> Confirm(int id)
        {
            throw new NotImplementedException();
            /*
            var order = Repository.Orders.FirstOrDefault(o => o.Id == id);

            if (order == null)
                return BadRequest($"Order with ID '{id}' not found.");

            if (order.Status != OrderStatus.Draft)
                return BadRequest($"Order '{id}' has status '{order.Status.ToString()}' and cannot be confirmed.");

            order.Status = OrderStatus.Confirmed;
            order.ConfirmedDate = DateTime.UtcNow;

            order = Repository.UpdateOrder(order);
            */
        }

        public ServiceResult<Order> Ship(int id)
        {
            throw new NotImplementedException();
        }

        public ServiceResult<Order> Cancel(int id)
        {
            throw new NotImplementedException();
        }

        public ServiceResult<Order> GetByCustomer(int id)
        {
            throw new NotImplementedException();
        }
    }
}
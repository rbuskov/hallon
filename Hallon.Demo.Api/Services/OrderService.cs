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
            switch (Repository.Orders.SingleOrDefault(o => o.Id == id))
            {
                case Order order:
                    return new ServiceResult<Order>(order);
                case null:
                    return ServiceResult<Order>.OrderNotFound(id);
            }
        }

        public ServiceResult<IEnumerable<Order>> GetByCustomer(int id)
        {
            var customer = Repository.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return ServiceResult<IEnumerable<Order>>.CustomerNotFound(id);

            return new ServiceResult<IEnumerable < Order >>(Repository.Orders.Where(o => o.Customer == customer));
        }

        public ServiceResult<Order> Create(OrderRequest request)
        {
            var customer = Repository.Customers.SingleOrDefault(c => c.Id == request.CustomerId);

            if (customer == null)
                return ServiceResult<Order>.CustomerNotFound(request.CustomerId);

            Order order = new Order
            {
                Id = Repository.NextOrderId,
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

            foreach (var line in order.Lines)
                Repository.OrderLines.Add(line);

            return new ServiceResult<Order>(order);
        }

        public ServiceResult<Order> Confirm(int id)
        {
            switch (Repository.Orders.SingleOrDefault(o => o.Id == id))
            {
                case null:
                    return ServiceResult<Order>.OrderNotFound(id);
                case Order order when order.Status != OrderStatus.Draft:
                    return new ServiceResult<Order>("Only orders with status 'draft' can be shipped.");
                case Order order:
                    order.Status = OrderStatus.Confirmed;
                    order.ConfirmedDate = DateTime.UtcNow;
                    return new ServiceResult<Order>(order);
            }
        }

        public ServiceResult<Order> Ship(int id)
        {
            switch (Repository.Orders.SingleOrDefault(o => o.Id == id))
            {
                case null:
                    return ServiceResult<Order>.OrderNotFound(id);
                case Order order when order.Status != OrderStatus.Confirmed:
                    return new ServiceResult<Order>("Only orders with status 'confirmed' can be shipped.");
                case Order order:
                    order.Status = OrderStatus.Shipped;
                    order.ShippedDate = DateTime.UtcNow;
                    return new ServiceResult<Order>(order);
            }
        }

        public ServiceResult<Order> Cancel(int id)
        {
            switch (Repository.Orders.SingleOrDefault(o => o.Id == id))
            {
                case null:
                    return ServiceResult<Order>.OrderNotFound(id);
                case Order order when order.Status != OrderStatus.Draft || order.Status == OrderStatus.Confirmed:
                    return new ServiceResult<Order>("Only orders with status 'draft' or 'confirmed' can be cancelled.");
                case Order order:
                    order.Status = OrderStatus.Cancelled;
                    return new ServiceResult<Order>(order);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data;
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

        public ServiceResult<Order> Insert(CreateOrderRequest request)
        {
            var customer = Repository.Customers.SingleOrDefault(c => c.Id == request.CustomerId);

            if (customer == null)
                return ServiceResult<Order>.CustomerNotFound(request.CustomerId);

            Order order = CreateOrder();

            foreach (var requestLine in request.Lines)
            {
                var product = Repository.Products.SingleOrDefault(p => p.Id == requestLine.ProductId);

                if (product == null)
                    return ServiceResult<Order>.ProductNotFound(requestLine.ProductId);

                CreateOrderLine(requestLine, product);
            }

            Repository.Orders.Add(order);

            foreach (var line in order.Lines)
                Repository.OrderLines.Add(line);

            return new ServiceResult<Order>(order);

            Order CreateOrder()
            {
                return new Order
                {
                    Id = Repository.NextOrderId,
                    Customer = customer,
                    OrderDate = DateTime.UtcNow,
                    Status = OrderStatus.Draft
                };
            }

            void CreateOrderLine(Line requestLine, Product product)
            {
                var orderLine = new OrderLine
                {
                    Id = Repository.NextOrderLineId,
                    Order = order,
                    Quantity = requestLine.Quantity,
                    Product = product
                };

                order.Lines.Add(orderLine);
            }
        }

        public ServiceResult<Order> Confirm(int id) 
            => ChangeStatus(id, OrderStatus.Draft, OrderStatus.Confirmed, order => order.ConfirmedDate = DateTime.UtcNow);

        public ServiceResult<Order> Ship(int id)
            => ChangeStatus(id, OrderStatus.Confirmed, OrderStatus.Shipped, order => order.ShippedDate = DateTime.UtcNow);

        public ServiceResult<Order> Cancel(int id)
        {
            throw new NotImplementedException();
        }

        public ServiceResult<Order> GetByCustomer(int id)
        {
            throw new NotImplementedException();
        }

        private ServiceResult<Order> ChangeStatus(int id, OrderStatus fromStatus, OrderStatus toStatus, Action<Order> action)
        {
            switch (Repository.Orders.SingleOrDefault(o => o.Id == id))
            {
                case null:
                    return ServiceResult<Order>.OrderNotFound(id);
                case Order order when order.Status != fromStatus:
                    return ServiceResult<Order>.InvalidOrderStatus(order, fromStatus, toStatus);
                case Order order:
                    return DoChangeStatus(order);
            }

            ServiceResult<Order> DoChangeStatus(Order order)
            {
                order.Status = toStatus;
                action.Invoke(order);

                return new ServiceResult<Order>(order);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using Hallon.Demo.Data;
using Hallon.Demo.Common;
using Hallon.Demo.Services.Validators;

namespace Hallon.Demo.Services
{
    public class OrderService : DemoService<Order>
    {
        public OrderService() : base(Repository.Orders)
        { }

        public ServiceResult<IEnumerable<Order>> GetByStatus(string statusName)
            => Enum.TryParse<OrderStatus>(statusName, true, out var status)
                ? GetMany(order => order.Status == status)
                : new ServiceResult<IEnumerable<Order>>($"'{statusName}' is not a valid order status.");

        public ServiceResult<IEnumerable<Order>> GetByCustomer(int id)
        {
            var customer = Repository.Customers.SingleOrDefault(c => c.Id == id);

            return customer == null
                ? ServiceResult<IEnumerable<Order>>.CustomerNotFound()
                : GetMany(o => o.Customer == customer);
        }

        public ServiceResult<Order> Create(OrderRequest request)
            => Create<OrderRequest, OrderRequestValidator>(request, () =>
            {
                var order = new Order
                {
                    Id = Repository.NextOrderId,
                    Customer = Repository.Customers.Single(c => c.Id == request.CustomerId),
                    OrderDate = DateTime.UtcNow,
                    Status = OrderStatus.Draft
                };

                foreach (var requestLine in request.Lines)
                {
                    var product = Repository.Products.Single(p => p.Id == requestLine.ProductId);

                    var orderLine = new OrderLine
                    {
                        Id = Repository.NextOrderLineId,
                        Order = order,
                        Quantity = requestLine.Quantity,
                        Product = product,
                        Description = product.Name,
                        UnitPrice = product.Price
                    };

                    order.Lines.Add(orderLine);
                    Repository.OrderLines.Add(orderLine);
                }

                return order;
            });

        public ServiceResult<Order> Confirm(int id)
            => Update<ConfirmOrderValidator>(id, order => order.Status = OrderStatus.Confirmed);

        public ServiceResult<Order> Ship(int id)
            => Update<ShipOrderValidator>(id, order => order.Status = OrderStatus.Shipped);
 
        public ServiceResult<Order> Cancel(int id)
            => Update<CancelOrderValidator>(id, order => order.Status = OrderStatus.Cancelled);
    }
}
using System;
using System.Collections.Generic;
using Hallon.Demo.Resources;

namespace Hallon.Demo.Data
{
    public class Order
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public Customer Customer { get; set; }

        public List<OrderLine> Lines { get; } = new List<OrderLine>();

        public OrderResource ToResource()
        {
            var resource = new OrderResource
            {
                Id = Id,
                Date = Date
            };

            resource.Links["customer"] = new Link($"customers/{Customer.Id}");

            return resource;
        }
    }
}
using System;
using System.Collections.Generic;

namespace Hallon.Demo.Resources
{
    public class OrderResource : Resource
    {
        public int Id { get; set; }

        public DateTime OrderDate { get; set; }

        public DateTime? ConfirmedDate { get; set; }

        public DateTime? ShippedDate { get; set; }

        public List<OrderLineResource> Lines { get; } = new List<OrderLineResource>();
    }
}
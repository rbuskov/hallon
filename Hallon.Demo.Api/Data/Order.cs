using System;
using System.Collections.Generic;
using Hallon.Demo.Common;

namespace Hallon.Demo.Data
{
    public class Order : DemoEntity
    {
        public OrderStatus Status { get; set; }

        public DateTime OrderDate { get; set; }

        public DateTime? ConfirmedDate { get; set; }

        public DateTime? ShippedDate { get; set; }

        public Customer Customer { get; set; }

        public List<OrderLine> Lines { get; } = new List<OrderLine>();
    }
}
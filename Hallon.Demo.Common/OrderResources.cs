using System;
using System.Collections.Generic;

namespace Hallon.Demo.Common
{
    public class OrderSummaryResource : Resource
    {
        public int Id { get; set; }

        public OrderStatus Status { get; set; }

        public DateTime OrderDate { get; set; }

        public string CustomerName { get; set; }
    }

    public class OrderResource : OrderSummaryResource
    {
        public DateTime? ConfirmedDate { get; set; }

        public DateTime? ShippedDate { get; set; }

        public decimal Total { get; set; }

        public List<Line> Lines { get; set; }

        public class Line : Resource
        {
            public int Quantity { get; set; }

            public string Description { get; set; }

            public decimal UnitPrice { get; set; }

            public decimal Total { get; set; }
        }
    }
}
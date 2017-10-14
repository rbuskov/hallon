using System;
using System.Collections.Generic;

namespace Hallon.Demo.Resources
{
    public class Order : Resource
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public Customer Customer { get; set; }

        public List<OrderLine> Lines { get; } = new List<OrderLine>();
    }
}
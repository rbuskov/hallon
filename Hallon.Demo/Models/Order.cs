using System;
using System.Collections.Generic;
using System.Linq;

namespace Hallon.Demo.Models
{
    public class Order
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public List<OrderLine> Lines { get; } = new List<OrderLine>();

        public decimal Total => Lines.Sum(line => line.Total);
    }
}
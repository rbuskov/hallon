using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hallon.Demo.Resources
{
    public class CreateOrderRequest
    {
        public int CustomerId { get; set; }

        public List<Line> Lines { get; } = new List<Line>();
    }

    public class Line
    {
        public int Quantity { get; set; }

        public int ProductId { get; set; }
    }
}

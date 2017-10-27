using System.Collections.Generic;

namespace Hallon.Demo.Resources
{
    public class OrderRequest
    {
        public int CustomerId { get; set; }

        public List<Line> Lines { get; } = new List<Line>();

        public class Line
        {
            public int Quantity { get; set; }

            public int ProductId { get; set; }
        }
    }
}

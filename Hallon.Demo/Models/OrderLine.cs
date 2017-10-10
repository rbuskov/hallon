using System.Collections;

namespace Hallon.Demo.Models
{
    public class OrderLine
    {
        public int Id { get; set; }

        public Product Product { get; set; }
        
        public int Quantity { get; set; }

        public decimal Total => Quantity * Product?.Price ?? 0;
    }
}
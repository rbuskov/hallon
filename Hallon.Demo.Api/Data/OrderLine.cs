namespace Hallon.Demo.Data
{
    public class OrderLine
    {
        public int Id { get; set; }

        public Order Order { get; set; }

        public int Quantity { get; set; }

        public Product Product { get; set; }

        public decimal Total => Quantity * Product?.Price ?? 0;
    }
}
namespace Hallon.Demo.Data
{
    public class OrderLine : DemoEntity
    {
        public Order Order { get; set; }

        public int Quantity { get; set; }

        public Product Product { get; set; }

        public string Description { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal Total => Quantity * Product?.Price ?? 0;
    }
}
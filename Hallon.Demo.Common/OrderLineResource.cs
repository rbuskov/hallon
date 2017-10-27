namespace Hallon.Demo.Common
{
    public class OrderLineResource : Resource
    {
        public int Id { get; set; }

        public OrderResource OrderResource { get; set; }

        public int Quantity { get; set; }

        public ProductResource Product { get; set; }

        public decimal Total => Quantity * Product?.Price ?? 0;
    }
}
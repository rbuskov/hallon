namespace Hallon.Demo.Common
{
    public class ProductSummaryResource : Resource
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }

    public class ProductResource : ProductSummaryResource
    {
        public decimal Price { get; set; }
    }
}
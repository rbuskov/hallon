namespace Hallon.Demo.Common
{
    public class ProductSummaryResource : Resource
    {
    }

    public class ProductResources : ProductSummaryResource
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }
    }
}
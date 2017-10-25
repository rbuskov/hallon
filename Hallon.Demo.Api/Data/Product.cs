using Hallon.Demo.Resources;

namespace Hallon.Demo.Data
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public ProductResource ToResource()
        {
            throw new System.NotImplementedException();
        }
    }
}
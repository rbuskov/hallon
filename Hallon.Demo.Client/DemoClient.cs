using System.Linq;
using System.Net;
using Hallon.Client;
using Hallon.Demo.Resources;

namespace Hallon.Demo.Client
{
    public class DemoClient
    {
        public DemoClient()
        {
            var client = new HalClient("http://example.org/api");

            // check status code (good idea when link is hardcoded)
            if (client.Get<OrderResource>("/orders/1", out var order) == HttpStatusCode.OK)
            {
                // trust that the resource is there (will throw an exception if link is not found)
                var customer = client.Get<CustomerResource>(order, "customer");

                foreach (var line in order.Lines)
                {
                    // OrderResource line may or may not have an order link
                    if (line.Links.TryGet("product", out var productLink))
                    {
                        var product = client.Get<ProductResource>(productLink);
                    }
                }
            }
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using Hallon.Configuration;
using Hallon.Demo.Models;

namespace Hallon.Demo
{
    public class HalConfig : HalConfiguration
    {
        public HalConfig()
        {
            /*
            AddResource<Order>()
                // actions
                .WithLink("confirm", order => $"orders/{order.Id}/confirm")
                .WithLink("invoice", order => $"orders/{order.Id}/invoice")
                .WithLink("cancel", order => $"orders/{order.Id}/cancel")
                // related resources
                .WithLink("invoice", order => $"invoices/{order.Invoice.Id}", order => order.Invoice != null)
                .AddEmbeddedCollection<OrderLine>("lines", order => order.Lines.Where(line => line.Product != null))
                    .WithLink("product", line => $"products/{line.Product.Id}")
                    .WithProperty("quantity", line => line.Quantity)
                    .WithProperty("description", line => line.Product.Name)
                    .WithProperty("unitPrice", line => line.Product.Price)
                    .WtihProperty("total", line => line.Total);
              .AddEmbeddedObject<...>(...)

            AddCollectionResource<Order>()
                .WithProperty("count", orders => orders.Count())
                .WithProperty("grandTotal", orders => orders.Sum(order => order.Total));
              .AddEmbeddedItems()
                 .WithLink(...)            
                 .WithProperty(...)            
            */
        }
    }
}
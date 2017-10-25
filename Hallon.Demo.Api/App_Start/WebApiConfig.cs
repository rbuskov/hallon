using System.Web.Http;
using AutoMapper;
using Hallon.Demo.Data;
using Hallon.Demo.Resources;

namespace Hallon.Demo
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();
            config.Formatters.Add(new HalMediaTypeFormatter());

            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Order, OrderResource>()
                    // map lines...
                    .AfterMap((order, resource) =>
                    {
                        var self = $"orders/{order.Id}";

                        resource.Links.AddSelf(self);
                        resource.Links.Add("customer", $"customers/{order.Customer.Id}");

                        switch (order.Status)
                        {
                            case OrderStatus.Draft:
                                resource.Links.Add("confirm", $"{self}/confirm");
                                resource.Links.Add("cancel", $"{self}/cancel");
                                break;

                            case OrderStatus.Confirmed:
                                resource.Links.Add("ship", $"{self}/ship");
                                resource.Links.Add("cancel", $"{self}/cancel");
                                break;
                        }
                    });
            });
        }
    }
}

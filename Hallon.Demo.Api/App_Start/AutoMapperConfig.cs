using AutoMapper;
using Hallon.Demo.Common;
using Hallon.Demo.Data;

namespace Hallon.Demo
{
    public class AutoMapperConfig
    {
        public static void Register()
        {
            Mapper.Initialize(config =>
            {
                config.CreateMap<Product, ProductResource>();
                config.CreateMap<Product, ProductSummaryResource>();

                config.CreateMap<Customer, CustomerResource>();
                config.CreateMap<Customer, CustomerSummaryResource>();

                config.CreateMap<Order, OrderResource>()
                    .ForMember(resource => resource.CustomerName,
                        options => options.MapFrom(entity => entity.Customer.Name))
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
                config.CreateMap<Order, OrderSummaryResource>();

                config.CreateMap<OrderLine, OrderResource.Line>();
            });
        }
    }
}
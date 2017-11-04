using System;
using System.Linq;
using System.Runtime.Remoting;
using AutoMapper;
using Hallon.Demo.Common;
using Hallon.Demo.Data;
using Xunit;

namespace Hallon.Demo.UnitTests
{
    public class AutoMapper
    {
        static AutoMapper()
        {
            AutoMapperConfig.Register();
        }

        [Fact]
        public void ShouldMapProducts()
        {
            var entity = new Product {Id = 1, Name = "test"};
            var resource = Mapper.Map<ProductResource>(entity);

            Assert.Equal(entity.Id, resource.Id);
            Assert.Equal(entity.Name, resource.Name);
            Assert.Equal(entity.Price, resource.Price);
        }

        [Fact]
        public void ShouldMapProductSummaries()
        {
            Mapper.Map<Product, ProductSummaryResource>(new Product());
        }

        [Fact]
        public void ShouldMapCustomers()
        {
            var entity = new Customer
            {
                Id = 1,
                Name = "test",
                Address = new Address
                {
                    Street = "street",
                    PostalCode = "1111",
                    City = "city",
                    State = "state",
                    Country = Country.Afghanistan
                }
            };

            var resource = Mapper.Map<CustomerResource>(entity);

            Assert.Equal(entity.Id, resource.Id);
            Assert.Equal(entity.Name, resource.Name);
            Assert.Equal(entity.Address.Street, resource.Address.Street);
            Assert.Equal(entity.Address.PostalCode, resource.Address.PostalCode);
            Assert.Equal(entity.Address.City, resource.Address.City);
            Assert.Equal(entity.Address.State, resource.Address.State);
            Assert.Equal(entity.Address.Country, resource.Address.Country);
        }

        [Fact]
        public void ShouldMapCustomerSummaries()
        {
            Mapper.Map<Customer, CustomerSummaryResource>(new Customer());
        }

        [Fact]
        public void ShouldMapOrders()
        {
            var entity = new Order
            {
                Id = 1,
                Customer = new Customer { Id = 2, Name = "test" },
                OrderDate = new DateTime(1970, 10, 11),
                Status = OrderStatus.Cancelled
            };

            entity.Lines.Add(new OrderLine
            {
                Id = 3, 
                Order = entity,
                Quantity = 3,
                Description = "test",
                UnitPrice = 1.23M,
                Product = new Product { Id = 4}
            });

            var resource = Mapper.Map<OrderResource>(entity);

            Assert.Equal(entity.Id, resource.Id);
            Assert.Equal(entity.Customer.Name, resource.CustomerName);
            Assert.Equal(entity.OrderDate, resource.OrderDate);
            Assert.Equal(entity.Status, resource.Status);
            Assert.Single(resource.Lines);

            Assert.Equal(entity.Lines[0].Quantity, resource.Lines[0].Quantity);
            Assert.Equal(entity.Lines[0].Description, resource.Lines[0].Description);
            Assert.Equal(entity.Lines[0].UnitPrice, resource.Lines[0].UnitPrice);
            Assert.Equal(entity.Lines[0].Total, resource.Lines[0].Total);
        }

        [Fact]
        public void ShouldMapOrderSummaries()
        {
            Mapper.Map<OrderSummaryResource>(new Order());
        }
    }
}

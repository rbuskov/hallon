﻿using System;
using System.Collections.Generic;
using Hallon.Demo.Common;

namespace Hallon.Demo.Data
{
    public static class Repository
    {
        private static int nextOrderId;
        private static int nextOrderLineId;
        private static int nextCustomerId;
        private static int nextProductId;

        public static int NextOrderId => ++nextOrderId;

        public static int NextOrderLineId => ++nextOrderLineId;

        public static int NextCustomerId => ++nextCustomerId;

        public static int NextProductId => ++nextProductId;

        public static List<Order> Orders { get; } = new List<Order>();

        public static List<OrderLine> OrderLines { get; } = new List<OrderLine>();

        public static List<Customer> Customers { get; } = new List<Customer>();

        public static List<Product> Products { get; } = new List<Product>();

        static Repository()
        {
            Customers.Add(new Customer
            {
                Id = 1,
                Name = "Acme, Inc",
                Address = new Address { Street = "H.C. Andersens Boulevard 1", PostalCode = "1000", City = "Copenhagen", Country = Country.Denmark }
            });

            Customers.Add(new Customer
            {
                Id = 2,
                Name = "Secondhand Submaries Ltd.",
                Address = new Address { Street = "Peter Madsens Gatan", PostalCode = "2000", City = "Malmo", Country = Country.Sweden }
            });

            Products.Add(new Product { Id = 1, Name = "Blue Widget", Price = 34.95M});
            Products.Add(new Product { Id = 2, Name = "Red Widget", Price = 29.95M });
            Products.Add(new Product { Id = 3, Name = "Yellow Widget", Price = 33.95M });

            Orders.Add(new Order { Id = 1, Customer = Customers[0], OrderDate = new DateTime(2017, 1, 10), Status = OrderStatus.Draft});
            Orders.Add(new Order { Id = 2, Customer = Customers[0], OrderDate = new DateTime(2017, 2, 10), Status = OrderStatus.Confirmed });
            Orders.Add(new Order { Id = 3, Customer = Customers[1], OrderDate = new DateTime(2017, 3, 10), Status = OrderStatus.Confirmed });

            Customers[0].Orders.Add(Orders[0]);
            Customers[0].Orders.Add(Orders[1]);
            Customers[1].Orders.Add(Orders[2]);

            OrderLines.Add(new OrderLine { Id = 1, Order = Orders[0], Product = Products[0], Quantity = 1 });
            OrderLines.Add(new OrderLine { Id = 2, Order = Orders[0], Product = Products[1], Quantity = 2 });
            OrderLines.Add(new OrderLine { Id = 3, Order = Orders[1], Product = Products[2], Quantity = 3 });
            OrderLines.Add(new OrderLine { Id = 4, Order = Orders[1], Product = Products[0], Quantity = 4 });
            OrderLines.Add(new OrderLine { Id = 5, Order = Orders[2], Product = Products[1], Quantity = 5 });
            OrderLines.Add(new OrderLine { Id = 6, Order = Orders[2], Product = Products[2], Quantity = 6 });
            OrderLines.Add(new OrderLine { Id = 7, Order = Orders[2], Product = Products[0], Quantity = 7 });

            Orders[0].Lines.Add(OrderLines[0]);
            Orders[0].Lines.Add(OrderLines[1]);
            Orders[1].Lines.Add(OrderLines[2]);
            Orders[1].Lines.Add(OrderLines[3]);
            Orders[2].Lines.Add(OrderLines[4]);
            Orders[2].Lines.Add(OrderLines[5]);
            Orders[2].Lines.Add(OrderLines[6]);

            nextOrderId = Orders.Count;
            nextOrderLineId = OrderLines.Count;
            nextCustomerId = Customers.Count;
            nextProductId = Products.Count;
        }
    }
}

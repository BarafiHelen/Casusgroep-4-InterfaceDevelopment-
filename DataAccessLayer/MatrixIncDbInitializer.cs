using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public static class MatrixIncDbInitializer
    {
        public static void Initialize(MatrixIncDbContext context)
        {
            // Check for a data if it created
            context.Database.EnsureCreated();

            // Look for any customers.
            if (context.Customers.Any() || context.Products.Any() || context.Orders.Any())
            {
                return;   // DB has been seeded
            }

            var categories = new Category[]
           {
                new Category { Name = "Ship Equipment" },
                new Category { Name = "Furniture" },
                new Category { Name = "Weapons" }
           };
            context.Categories.AddRange(categories);
            context.SaveChanges();
           
            var customers = new Customer[]
            {
                new Customer { Name = "Neo", Address = "123 Elm St" , IsActive=true},
                new Customer { Name = "Morpheus", Address = "456 Oak St", IsActive = true },
                new Customer { Name = "Trinity", Address = "789 Pine St", IsActive = true }
            };
            context.Customers.AddRange(customers);
            context.SaveChanges();

            var orders = new Order[]
            {
                new Order { Customer = customers[0], OrderDate = DateTime.Parse("2021-01-01")},
                new Order { Customer = customers[0], OrderDate = DateTime.Parse("2021-02-01")},
                new Order { Customer = customers[1], OrderDate = DateTime.Parse("2021-02-01")},
                new Order { Customer = customers[2], OrderDate = DateTime.Parse("2021-03-01")}
            };  
            context.Orders.AddRange(orders);
            context.SaveChanges();

            var products = new Product[]
            {
                new Product { Name = "Nebuchadnezzar", Description = "Het schip waarop Neo voor het eerst de echte wereld leert kennen", Price = 10000.00m, Stock = 5, ImageUrl = "",IsActive = true, Category = categories[0]},
                new Product { Name = "Jack-in Chair", Description = "Stoel met een rugsteun en metalen armen waarin mensen zitten om ingeplugd te worden in de Matrix via een kabel in de nekpoort", Price = 500.50m, Stock = 5, ImageUrl = "", IsActive = true, Category = categories[1]},
                new Product { Name = "EMP (Electro-Magnetic Pulse) Device", Description = "Wapentuig op de schepen van Zion", Price = 129.99m, Stock = 5, ImageUrl = "", IsActive = true, Category = categories[2]}
            };
            context.Products.AddRange(products);
            context.SaveChanges();

            var orderItems = new OrderItem[]
            {
                new OrderItem { Order = orders[0], Product = products[0], Quantity = 1 },
                new OrderItem { Order = orders[0], Product = products[1], Quantity = 2 },
                new OrderItem { Order = orders[1], Product = products[2], Quantity = 1 }
            };
            context.OrderItems.AddRange(orderItems);
            context.SaveChanges();

        }
    }
}

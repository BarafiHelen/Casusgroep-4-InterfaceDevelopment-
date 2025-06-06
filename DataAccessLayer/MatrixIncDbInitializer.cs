using DataAccessLayer.Models;

namespace DataAccessLayer
{
    public static class MatrixIncDbInitializer
    {
        public static void SeedData(MatrixIncDbContext context)
        {
            // Stop als er al gegevens zijn
            if (context.Customers.Any() || context.Products.Any() || context.Orders.Any())
                return;

            // Categorieën
            var categories = new Category[]
            {
                new Category { Name = "Ship Equipment" },
                new Category { Name = "Furniture" },
                new Category { Name = "Weapons" }
            };
            context.Categories.AddRange(categories);
            context.SaveChanges();

            // Klanten
            var customers = new Customer[]
            {
                new Customer { Name = "Neo", Address = "123 Elm St", IsActive = true },
                new Customer { Name = "Morpheus", Address = "456 Oak St", IsActive = true },
                new Customer { Name = "Trinity", Address = "789 Pine St", IsActive = true }
            };
            context.Customers.AddRange(customers);
            context.SaveChanges();

            // Orders
            var orders = new Order[]
            {
                new Order { Customer = customers[0], OrderDate = new DateTime(2021, 1, 1) },
                new Order { Customer = customers[0], OrderDate = new DateTime(2021, 2, 1) },
                new Order { Customer = customers[1], OrderDate = new DateTime(2021, 2, 1) },
                new Order { Customer = customers[2], OrderDate = new DateTime(2021, 3, 1) }
            };
            context.Orders.AddRange(orders);
            context.SaveChanges();

            // Producten
            var products = new Product[]
            {
                new Product
                {
                    Name = "Nebuchadnezzar",
                    Description = "Het schip waarop Neo voor het eerst de echte wereld leert kennen",
                    Price = 10000.00m,
                    Stock = 5,
                    ImageUrl = "",
                    IsActive = true,
                    Category = categories[0]
                },
                new Product
                {
                    Name = "Jack-in Chair",
                    Description = "Stoel met metalen armen om in te pluggen",
                    Price = 500.50m,
                    Stock = 5,
                    ImageUrl = "",
                    IsActive = true,
                    Category = categories[1]
                },
                new Product
                {
                    Name = "EMP Device",
                    Description = "Wapentuig op de schepen van Zion",
                    Price = 129.99m,
                    Stock = 5,
                    ImageUrl = "",
                    IsActive = true,
                    Category = categories[2]
                }
            };
            context.Products.AddRange(products);
            context.SaveChanges();

            // OrderItems
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

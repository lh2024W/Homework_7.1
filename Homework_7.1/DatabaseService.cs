using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_7._1
{
    public class DatabaseService
    {

        DbContextOptions<ApplicationContext> options;

        public void EnsurePopulated()
        {

            var builder = new ConfigurationBuilder();
            // установка пути к текущему каталогу
            builder.SetBasePath(Directory.GetCurrentDirectory());
            // получаем конфигурацию из файла appsettings.json
            builder.AddJsonFile("appsettings.json");
            // создаем конфигурацию
            var config = builder.Build();
            // получаем строку подключения
            string connectionString = config.GetConnectionString("DefaultConnection");


            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            options = optionsBuilder.UseSqlServer(connectionString).Options;

            using (ApplicationContext db = new ApplicationContext(options))
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                List<Client> clients = new List<Client>
                {
                    new Client { Name = "John Doe", Email = "john@mail.com", Address = " Odessa"},
                    new Client { Name = " Alice Smith", Email = "alice@gmail.com", Address = "Kyiv"}
                };
                db.Clients.AddRange(clients);

                List<Product> products = new List<Product>
                {
                    new Product { Name = "Яблоки", Price = 16.99m},
                    new Product { Name = "Ананас", Price = 46.50m},
                    new Product { Name = " Вишня", Price = 35.45m}
                };
                db.Products.AddRange(products);

                db.Orders.AddRange
                    (
                        new Order
                        {
                            Client = clients[0],
                            OrderDetails = new OrderDetail[]
                            {
                                new OrderDetail
                                {
                                    Product = products[0],
                                    Quantity = 10
                                },
                                new OrderDetail
                                {
                                    Product = products[2],
                                    Quantity = 5
                                }
                            }
                        },
                        new Order
                        {
                            Client = clients[0],
                            OrderDetails = new OrderDetail[]
                            {
                                new OrderDetail
                                {
                                    Product = products[1],
                                    Quantity = 2
                                }
                            }
                        },
                        new Order
                        {
                            Client = clients[0],
                            OrderDetails = new OrderDetail[]
                            {
                                new OrderDetail
                                {
                                    Product = products[0],
                                    Quantity = 9
                                }

                            }
                        },
                        new Order
                        {
                            Client = clients[1],
                            OrderDetails = new OrderDetail[]
                            {
                                new OrderDetail
                                {
                                    Product= products[2],
                                    Quantity = 10
                                }
                            }
                        },
                        new Order
                        {
                            Client = clients[1],
                            OrderDetails = new OrderDetail[]
                            {
                                new OrderDetail
                                {
                                    Product= products[1],
                                    Quantity = 5
                                }
                            }
                        }
                    );
                db.SaveChanges();
            }

        }

        public void GetClientWithInfo()
        {
            using (ApplicationContext db = new ApplicationContext(options))
            {
                var query = db.Clients.Select(client => new
                {
                    Name = client.Name,
                    Email = client.Email,
                    Address = client.Address,
                    TotalOrders = client.Orders.Count(),
                    TotalSpent = client.Orders.SelectMany(order => order.OrderDetails).Sum(detail => detail.Quantity * detail.Product.Price),
                    MaxExpensiveProduct = client.Orders.SelectMany(order => order.OrderDetails).Select(detail => detail.Product.Price).Max()
                }).ToList();

                foreach (var item in query)
                {
                    Console.WriteLine($"Name: {item.Name}");
                    Console.WriteLine($"Email: {item.Email}");
                    Console.WriteLine($"Address: {item.Address}");
                    Console.WriteLine($"Total Orders: {item.TotalOrders}");
                    Console.WriteLine($"Total Spent: {item.TotalSpent}");
                    Console.WriteLine($"Most Expensive Product: {item.MaxExpensiveProduct}");
                    Console.WriteLine();
                    
                }
            }
        }
    }
}

      
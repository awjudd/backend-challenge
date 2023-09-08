using Challenge.Core.DataAccess;
using Challenge.Core.DataAccess.Contracts.Persistence;
using Challenge.UI.Console;
using Challenge.UI.Console.Output;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", false, true)
    .AddJsonFile("appsettings.development.json", true, true)
    .Build();

var services = new ServiceCollection()
    .AddApplicationServices(config)
    .BuildServiceProvider();

var resetDatabase = config.GetValue<bool>("ResetDatabaseOnStartup");

if (resetDatabase)
{
    await services.ResetDatabaseAsync();
}

var customerRepository = services.GetRequiredService<ICustomerRepository>();
var productRepository = services.GetRequiredService<IProductRepository>();
var orderRepository = services.GetRequiredService<IOrderRepository>();

while (true)
{
    Console.Clear();

    Console.WriteLine("""
                        Backend Challenge
                        1. Log a Order
                        2. View Purchases
                        3. Exit
                      """);

    var option = Console.ReadLine();

    if (int.TryParse(option, out var optionInt))
    {
        switch (optionInt)
        {
            case 1:
                Console.Clear();
                Console.WriteLine("================================");
                Console.WriteLine("Log a Order");
                Console.WriteLine("================================");
                Console.WriteLine("Enter a Customer Name");
                var customerName = Console.ReadLine();

                while (string.IsNullOrWhiteSpace(customerName))
                {
                    Console.WriteLine("Enter a Customer Name");
                    customerName = Console.ReadLine();
                }

                Console.WriteLine("Enter a Product Name");
                var productName = Console.ReadLine();

                while (string.IsNullOrWhiteSpace(productName))
                {
                    Console.WriteLine("Enter a Product Name");
                    productName = Console.ReadLine();
                }

                var customer = await customerRepository.FindOrCreateCustomer(customerName);
                var product = await productRepository.FindOrCreateProduct(productName);

                await orderRepository.AddOrderForCustomer(customer, product);

                var orders = await orderRepository.FindOrdersForCustomer(customer);

                Console.WriteLine("Order Received!");
                Console.WriteLine($"""
                                   Customer Name: {customer.Name}
                                   Current Order:
                                   {orders.Print()}
                                   """);

                Console.ReadLine();
                break;
            case 2:
                Console.Clear();

                Console.WriteLine("================================");
                Console.WriteLine("View Purchases");
                Console.WriteLine("================================");

                var ordersByCustomer = await orderRepository.GetOrdersByCustomer();
                var orderNumber = 1;

                foreach (var order in ordersByCustomer)
                {
                    Console.WriteLine($"""
                                       Order #{orderNumber++}
                                       Customer Name:
                                       {order.Item1.Name}

                                       Products:
                                       {order.Item2.Print()}
                                       """);
                }

                Console.ReadLine();

                break;
            case 3:
                Console.WriteLine("Exiting...");
                return;
            default:
                Console.WriteLine("Invalid option");
                break;
        }
    }
}
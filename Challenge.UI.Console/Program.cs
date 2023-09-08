using Challenge.Core.DataAccess;
using Challenge.UI.Console;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile("appsettings.development.json", optional: true, reloadOnChange: true)
    .Build();

var services = new ServiceCollection()
    .AddApplicationServices(config);

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

                while (string.IsNullOrWhiteSpace(customerName))
                {
                    Console.WriteLine("Enter a Product Name");
                    productName = Console.ReadLine();
                }
                
                
                break;
            case 2:
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
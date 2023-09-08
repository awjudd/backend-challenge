using System.Text;
using Challenge.Core.Domain.Models;

namespace Challenge.UI.Console.Output;

public static class OrderExtensions
{
    public static string Print(this IEnumerable<Order> orders)
    {
        var builder = new StringBuilder();
        var item = 1;

        foreach (var order in orders)
        {
            builder.AppendLine($"{item++}: {order}");
        }

        return builder.ToString();
    }
}
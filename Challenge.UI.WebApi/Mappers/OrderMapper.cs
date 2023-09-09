using Challenge.Core.Domain.Models;
using Challenge.UI.WebApi.DataTransfer;

namespace Challenge.UI.WebApi.Mappers;

public static class OrderMapper
{
    public static OrderResponseDto ToDto(this Order order)
    {
        return new OrderResponseDto
        {
            Id = order.Id,
            Customer = order.Customer.Name,
            Product = order.Product.Name,
            Quantity = order.Quantity
        };
    }

    public static IEnumerable<OrderResponseDto> ToDto(this IEnumerable<Order> orders)
    {
        return orders.Select(ToDto);
    }
}
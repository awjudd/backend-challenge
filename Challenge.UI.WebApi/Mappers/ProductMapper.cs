using Challenge.Core.Domain.Models;
using Challenge.UI.WebApi.DataTransfer;

namespace Challenge.UI.WebApi.Mappers;

public static class ProductMapper
{
    public static ProductResponseDto ToDto(this Product customer)
    {
        return new ProductResponseDto
        {
            Id = customer.Id,
            Name = customer.Name
        };
    }

    public static IEnumerable<ProductResponseDto> ToDto(this IEnumerable<Product> products)
    {
        return products.Select(ToDto);
    }
}
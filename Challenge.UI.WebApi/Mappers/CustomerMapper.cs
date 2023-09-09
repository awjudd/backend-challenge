using Challenge.Core.Domain.Models;
using Challenge.UI.WebApi.DataTransfer;

namespace Challenge.UI.WebApi.Mappers;

public static class CustomerMapper
{
    public static CustomerResponseDto ToDto(this Customer customer)
    {
        return new CustomerResponseDto
        {
            Id = customer.Id,
            Name = customer.Name
        };
    }

    public static IEnumerable<CustomerResponseDto> ToDto(this IEnumerable<Customer> customers)
    {
        return customers.Select(ToDto);
    }
}
namespace Challenge.Core.Domain.Models;

public class Customer
{
    public Guid Id { get; init; }

    public required string Name { get; init; }
}
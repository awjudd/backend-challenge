namespace Challenge.Core.Domain.Models;

public class Product
{
    public Guid Id { get; init; } = default!;
    
    public required string Name { get; init; }
}
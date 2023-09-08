namespace Challenge.Core.Domain.Models;

public class Product
{
    public Guid Id { get; init; }

    public required string Name { get; init; }

    public override string ToString()
    {
        return Name;
    }
}
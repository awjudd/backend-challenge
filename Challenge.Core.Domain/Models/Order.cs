namespace Challenge.Core.Domain.Models;

public class Order
{
    public Guid Id { get; init; }

    public required Product Product { get; init; } = null!;

    public required Customer Customer { get; init; } = null!;

    public required int Quantity { get; set; } = 1;

    public override string ToString()
    {
        return $"{nameof(Product)}: {Product}, {nameof(Quantity)}: {Quantity}";
    }
}
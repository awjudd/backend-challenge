namespace Challenge.Core.Domain.Models;

public class Order
{
    public required Guid Id { get; init; }  = Guid.NewGuid();
    
    public required Product Product { get; init; }  = null!;
    
    public required Customer Customer { get; init; }  = null!;
    
    public required int Quantity { get; init; }  = 0;
}
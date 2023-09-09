namespace Challenge.UI.WebApi.DataTransfer;

public class OrderResponseDto
{
    public required Guid Id { get; init; }

    public required string Customer { get; init; }

    public required string Product { get; init; }

    public required int Quantity { get; init; }
}
namespace Challenge.UI.WebApi.DataTransfer;

public class OrderRequestDto
{
    public required string Customer { get; init; }

    public required string Product { get; init; }
}
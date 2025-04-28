namespace SuperMarket.API.DTOs;

/// <summary>
/// Response DTO for all cart items
/// </summary>
public class CartItemsResponse
{
    public List<CartItemDto> Items { get; set; } = [];
}

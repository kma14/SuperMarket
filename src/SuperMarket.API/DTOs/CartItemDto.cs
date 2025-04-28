using System.ComponentModel.DataAnnotations;

namespace SuperMarket.API.DTOs;

/// <summary>
/// DTO representing a single cart item
/// </summary>
public class CartItemDto
{
    public required string Sku { get; set; }
    public int Quantity { get; set; }
    public decimal LineTotal { get; set; }
}

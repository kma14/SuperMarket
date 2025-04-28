using Mapster;
using Microsoft.AspNetCore.Mvc;
using SuperMarket.API.DTOs;
using SuperMarket.Application.Interfaces;
using SuperMarket.Domain.Exceptions;
using SuperMarket.Domain.Interfaces;

namespace SuperMarket.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CartController(ICartService cartServices, IPricingStrategy pricingStrategy) : Controller()
{
    // GET: api/cart/items
    [HttpGet("items")]
    public IActionResult GetCartItems()
    {
        try
        {
            var items = cartServices.GetCartItems();
            var dtoItems = items.Adapt<List<CartItemDto>>();
            return Ok(new CartItemsResponse() { Items = dtoItems });
        }
        catch (Exception ex)
        {
            return Problem(
               title: "Failed to retrieve cart items",
               detail: ex.Message,
               statusCode: StatusCodes.Status500InternalServerError
           );
        }
    }

    [HttpGet("items/{sku}")]
    public IActionResult GetItemBySku(string sku)
    {
        var item = cartServices.GetCartItem(sku);
        if (item == null)
            return Problem(
                    title: "Cart item not found",
                    detail: $"No cart item found for SKU '{sku}'.",
                    statusCode: StatusCodes.Status404NotFound
                );

        return Ok(item.Adapt<CartItemDto>());
    }

    // GET: api/cart/total
    [HttpGet("total")]
    public IActionResult GetTotalPrice()
    {
        try
        {
            var totalPrice = cartServices.CalculateTotalPrice();
            return Ok(new TotalPriceResponse() { TotalPrice = totalPrice });
        }
        catch (Exception ex)
        {
            return Problem(
                title: "Failed to calculate cart total",
                detail: ex.Message,
                statusCode: StatusCodes.Status500InternalServerError
            );
        }
    }


    // POST: api/cart/add
    [HttpPost("add")]
    public IActionResult AddItemToCart(AddCartItemRequest request)
    {
        try
        {
            cartServices.AddCartItem(request.Sku);
            var locationUrl = Url.Action(nameof(GetItemBySku), "Cart", new { sku = request.Sku }, Request.Scheme);
            return Created(locationUrl, new { message = "Item added to cart" });
        }
        catch (InvalidSkuException ex)
        {
            return Problem(
                title: "Invalid SKU",
                detail: ex.Message,
                statusCode: StatusCodes.Status400BadRequest,
                type: "https://supermarket.com/errors/invalid-sku"
            );
        }
        catch (Exception ex)
        {
            return Problem(
                title: "Failed to add item to cart",
                detail: ex.Message,
                statusCode: StatusCodes.Status500InternalServerError
            );
        }
    }
}

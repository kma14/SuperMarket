using SuperMarket.Application.Services;
using SuperMarket.Domain.Entities;
using SuperMarket.Domain.Exceptions;
using SuperMarket.Domain.Interfaces;
using SuperMarket.Domain.ValueObjects;

namespace SuperMarket.UnitTests;

public class CartServiceTests
{
    private readonly Catalog _catalog = new(new Dictionary<string, List<Pack>>
    {
        { "A", new List<Pack> { new Pack(3, 130), new Pack(1, 50) } },
        { "B", new List<Pack> { new Pack(2, 45), new Pack(1, 30) } }
    });

    private readonly IPricingStrategy _pricingStrategy;

    public CartServiceTests()
    {
        _pricingStrategy = new SmarterFakePricingStrategy();
    }

    private CartService CreateCartService() => new(_pricingStrategy, _catalog);

    [Fact]
    public void AddCartItem_ShouldAddNewItem_WhenSkuExists()
    {
        // Arrange
        var cart = CreateCartService();

        // Act
        cart.AddCartItem("A");

        // Assert
        var items = cart.GetCartItems();
        Assert.Single(items);
        Assert.Equal("A", items[0].Sku);
        Assert.Equal(1, items[0].Quantity);
    }

    [Fact]
    public void AddCartItem_ShouldIncrementQuantity_WhenSameSkuAddedAgain()
    {
        var cart = CreateCartService();

        cart.AddCartItem("A");
        cart.AddCartItem("A");

        var items = cart.GetCartItems();
        Assert.Single(items);
        Assert.Equal(2, items[0].Quantity);
    }

    [Fact]
    public void AddCartItem_ShouldThrowInvalidSkuException_WhenSkuDoesNotExist()
    {
        var cart = CreateCartService();

        Assert.Throws<InvalidSkuException>(() => cart.AddCartItem("D"));
    }

    [Fact]
    public void CalculateTotalPrice_ShouldApplyPackDiscountsCorrectly()
    {
        var cart = CreateCartService();
 
        cart.AddCartItem("A");
        cart.AddCartItem("B");
        cart.AddCartItem("A"); 
        cart.AddCartItem("B"); 
        cart.AddCartItem("B");
        cart.AddCartItem("A");

        var total = cart.CalculateTotalPrice();

        Assert.Equal(205, total); // 130 + 45 +30 = 205
    }

    [Fact]
    public void GetCartItems_ShouldReturnItemsWithCorrectLineTotals()
    {
        var cart = CreateCartService();

        cart.AddCartItem("A");
        cart.AddCartItem("B");

        var items = cart.GetCartItems();

        Assert.Equal(2, items.Count);
        Assert.Contains(items, i => i.Sku == "A" && i.LineTotal == 50);
        Assert.Contains(items, i => i.Sku == "B" && i.LineTotal == 30);
    }

    [Fact]
    public void GetCartItem_ShouldReturnSpecificItemWithLineTotal()
    {
        var cart = CreateCartService();

        cart.AddCartItem("A");
        cart.AddCartItem("B");

        var item = cart.GetCartItem("B");

        Assert.NotNull(item);
        Assert.Equal("B", item?.Sku);
        Assert.Equal(1, item?.Quantity);
        Assert.Equal(30, item?.LineTotal);
    }

    [Fact]
    public void GetCartItem_ShouldReturnNull_WhenSkuNotInCart()
    {
        var cart = CreateCartService();

        cart.AddCartItem("A");

        var item = cart.GetCartItem("C");

        Assert.Null(item);
    }

    private class SmarterFakePricingStrategy : IPricingStrategy
    {
        public decimal CalculatePrice(string sku, int quantity)
        {
            return sku switch
            {
                "A" => (quantity / 3) * 130 + (quantity % 3) * 50,
                "B" => (quantity / 2) * 45 + (quantity % 2) * 30,
                _ => quantity * 20
            };
        }
    }
}

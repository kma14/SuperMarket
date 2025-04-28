using SuperMarket.Application.Interfaces;
using SuperMarket.Application.Models;
using SuperMarket.Domain.Entities;
using SuperMarket.Domain.Exceptions;
using SuperMarket.Domain.Interfaces;
using SuperMarket.Domain.PricingStrategies;
using SuperMarket.Domain.ValueObjects;
using System.Linq;

namespace SuperMarket.Application.Services;


/// <summary>
/// Application service that orchestrates cart operations, coordinates between domain models
/// </summary>
public class CartService : ICartService
{
    private readonly IPricingStrategy _pricingStrategy;
    private readonly Catalog _catalog;
    private readonly List<CartItem> _items = [];

    public CartService(IPricingStrategy pricingStrategy, Catalog catalog)
    {
        _pricingStrategy = pricingStrategy ?? throw new ArgumentNullException(nameof(pricingStrategy));
        _catalog = catalog;
    }

    public List<CartItemWithLineTotal> GetCartItems() => _items.Select(c => new CartItemWithLineTotal
    {
        Sku = c.Sku,
        Quantity = c.Quantity,
        LineTotal = _pricingStrategy.CalculatePrice(c.Sku, c.Quantity)
    }).ToList();

    public void AddCartItem(string sku)
    {
        if (!_catalog.ContainsSku(sku))
        {
            throw new InvalidSkuException(sku);
        }

        if (_items.FirstOrDefault(i => i.Sku == sku) is { } item)
        {
            item.Increment();
        }
        else
        {
            _items.Add(new CartItem(sku));
        }
    }

    public decimal CalculateTotalPrice() => _items.Sum(item => _pricingStrategy.CalculatePrice(item.Sku, item.Quantity));

    public CartItemWithLineTotal? GetCartItem(string sku)
    {
        var item = _items.FirstOrDefault(c => c.Sku == sku);
        if (item == null)
        { 
            return null; 
        }

        return new CartItemWithLineTotal
        {
            Sku = item.Sku,
            Quantity = item.Quantity,
            LineTotal = _pricingStrategy.CalculatePrice(item.Sku, item.Quantity)
        };
    }
}
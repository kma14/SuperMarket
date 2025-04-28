using SuperMarket.Domain.Interfaces;
using SuperMarket.Domain.ValueObjects;

namespace SuperMarket.Domain.PricingStrategies;

/// <summary>
/// Pricing strategy that calculates the total price of specific cartItems
/// </summary>
public class PackPricingStrategy : IPricingStrategy
{
    private readonly Catalog _catelog;

    public PackPricingStrategy(Catalog pricing)
    {
        _catelog = pricing;
    }

    /// <summary>
    /// Calculates the total price for a given SKU and quantity,
    /// </summary>
    public decimal CalculatePrice(string sku, int quantity)
    {
        decimal total = 0;
        int remaining = quantity;

        foreach (var pack in _catelog.GetPacksForSku(sku))
        {
            int numOfPacks = remaining / pack.Size;
            total += numOfPacks * pack.Price;
            remaining %= pack.Size;

            if (remaining == 0)
                break;
        }

        return total;
    }
}

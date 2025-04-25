using SuperMarket.Domain.Interfaces;
using SuperMarket.Domain.ValueObjects;

namespace SuperMarket.Domain.PricingStrategies
{
    public class PackPricingStrategy : IPricingStrategy
    {
        private readonly Dictionary<string, List<Pack>> _pricing;

        public PackPricingStrategy(Dictionary<string, List<Pack>> pricing)
        {
            // Ensure each SKU’s packs are sorted from largest to smallest
            _pricing = pricing.ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value.OrderByDescending(p => p.Size).ToList()
            );
        }

        public decimal CalculatePrice(string sku, int quantity)
        {
            if (!_pricing.TryGetValue(sku, out var packs))
                throw new KeyNotFoundException($"No pricing rules defined for SKU '{sku}'");

            decimal total = 0;
            int remaining = quantity;

            foreach (var pack in packs)
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
}

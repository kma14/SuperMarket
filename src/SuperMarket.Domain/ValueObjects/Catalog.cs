namespace SuperMarket.Domain.ValueObjects;

/// <summary>
/// Represents a catalog of available products and their associated pack options.
/// </summary>
public class Catalog
{
    private readonly Dictionary<string, List<Pack>> _productOptions;

    public Catalog(IReadOnlyDictionary<string, List<Pack>> products)
    {
        // Ensure each SKU's packs are sorted from largest to smallest
        _productOptions = products.ToDictionary(
               kvp => kvp.Key,
               kvp => kvp.Value.OrderByDescending(p => p.Size).ToList()
           ) ?? throw new ArgumentNullException(nameof(products));
    }

    /// <summary>
    /// Check if a sku is on the price list
    /// </summary>
    public bool ContainsSku(string sku) => _productOptions.ContainsKey(sku);

    /// <summary>
    /// Retrieves the available pack options for the specified SKU.
    /// </summary>
    public List<Pack> GetPacksForSku(string sku)
    {
        if (!_productOptions.ContainsKey(sku))
            throw new KeyNotFoundException($"SKU '{sku}' not found in catalog.");

        return _productOptions[sku];
    }
}

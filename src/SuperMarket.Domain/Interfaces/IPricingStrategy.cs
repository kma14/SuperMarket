namespace SuperMarket.Domain.Interfaces;

public interface IPricingStrategy
{
    decimal CalculatePrice(string sku, int quantity);
}

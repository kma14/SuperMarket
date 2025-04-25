using SuperMarket.Domain.ValueObjects;
using SuperMarket.Domain;

namespace SuperMarket.UnitTests
{
    public class PackPricingStrategyTests
    {
        [Fact]
        public void CalculatePrice_ShouldApplyBundlePricing()
        {
            // Arrange
            var rules = new Dictionary<string, List<Pack>>
            {
                { "A", new List<Pack> { new(3, 130), new(1, 50) } }
            };

            var strategy = new PackPricingStrategy(rules);

            // Act
            var price = strategy.CalculatePrice("A", 4);

            // Assert
            Assert.Equal(180, price); // 130 (3 pack) + 50 (1 pack)
        }
    }
}
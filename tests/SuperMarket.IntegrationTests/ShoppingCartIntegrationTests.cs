using SuperMarket.Domain;
using SuperMarket.Domain.Entities;
using SuperMarket.Domain.PricingStrategies;
using SuperMarket.Domain.ValueObjects;

namespace SuperMarket.IntegrationTests
{
    public class ShoppingCartIntegrationTests
    {
        [Fact]
        public void Cart_WithPackPricingStrategy_ShouldCalculateCorrectTotal()
        {
            // Arrange
            var rules = new Dictionary<string, List<Pack>>
            {
                { "A", new List<Pack> { new(1, 50), new(3, 130) } },
                { "B", new List<Pack> { new(2, 45), new(1, 30) } }
            };

            var strategy = new PackPricingStrategy(rules);
            var cart = new ShoppingCart(strategy);

            cart.AddItem(new CartItem("A"));
            cart.AddItem(new CartItem("B"));
            cart.AddItem(new CartItem("A"));
            cart.AddItem(new CartItem("A")); 

            // Act
            var total = cart.CalculateTotalPrice();

            // Assert
            Assert.Equal(160, total); // 130 + 30
        }
    }
}
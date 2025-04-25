using SuperMarket.Domain.Entities;
using SuperMarket.Domain.Interfaces;

namespace SuperMarket.UnitTests
{
    public class ShoppingCartTests
    {
        [Fact]
        public void CalculateTotal_ShouldSumPerSku()
        {
            var strategy = new FakePricingStrategy();
            var cart = new ShoppingCart(strategy);

            cart.AddItem(new CartItem("A"));
            cart.AddItem(new CartItem("A"));
            cart.AddItem(new CartItem("B"));

            var total = cart.CalculateTotalPrice();

            Assert.Equal(150, total); // (2 * 50 for A) + (1 * 50 for B)
        }

        private class FakePricingStrategy : IPricingStrategy
        {
            public decimal CalculatePrice(string sku, int quantity)
            {
                return quantity * 50;
            }
        }
    }
}

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SuperMarket.Application.Interfaces;
using SuperMarket.Application.Services;
using SuperMarket.Domain.Interfaces;
using SuperMarket.Domain.PricingStrategies;
using SuperMarket.Domain.ValueObjects;
using SuperMarket.Infrastructure;

namespace SuperMarket.IntegrationTests
{
    public class ShoppingCartIntegrationTests
    {
        private readonly IServiceProvider _serviceProvider;

        public ShoppingCartIntegrationTests()
        {
            // Arrange DI container
            var builder = new ServiceCollection();
            builder.AddInfrastructureServices(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build());

            var dataSource = builder.BuildServiceProvider().GetRequiredService<IDataSource>();
            var pricingRules = dataSource.Load<Dictionary<string, List<Pack>>>("PricingRules:PackRules");
            var catalog = new Catalog(pricingRules);
            builder.AddSingleton(catalog);
            builder.AddSingleton<IPricingStrategy>(new PackPricingStrategy(catalog));
            builder.AddSingleton<ICartService, CartService>();

            _serviceProvider = builder.BuildServiceProvider();
        }

        [Fact]
        public void Cart_WithPackPricingStrategy_ShouldCalculateCorrectTotal()
        {
            // Arrange: Resolve the CartService and use the injected strategy
            var cartService = _serviceProvider.GetRequiredService<ICartService>();

            cartService.AddCartItem("A");
            cartService.AddCartItem("A");
            cartService.AddCartItem("B");
            cartService.AddCartItem("A");
            cartService.AddCartItem("C");
            cartService.AddCartItem("B");

            // Act
            var total = cartService.CalculateTotalPrice();

            // Assert
            Assert.Equal(195, total); // 130 + 30
        }
    }
}
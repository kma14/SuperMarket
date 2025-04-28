using Microsoft.Extensions.DependencyInjection;
using SuperMarket.Application.Interfaces;
using SuperMarket.Application.Providers;
using SuperMarket.Application.Services;
using SuperMarket.Domain.Interfaces;
using SuperMarket.Domain.PricingStrategies;
using SuperMarket.Domain.ValueObjects;

namespace SuperMarket.Application;

public static class ApplicationDI
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddSingleton<PricingRulesProvider>();
        services.AddSingleton<Catalog>(provider =>
        {
            var pricingRules = provider.GetRequiredService<PricingRulesProvider>().PackRules;
            return new Catalog(pricingRules);
        });

        services.AddSingleton<IPricingStrategy>(provider =>
        {
            var catalog = provider.GetRequiredService<Catalog>();
            return new PackPricingStrategy(catalog);
        });

        services.AddSingleton<ICartService, CartService>();
        return services;
    }
}
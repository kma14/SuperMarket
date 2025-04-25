using Microsoft.Extensions.DependencyInjection;
using SuperMarket.Application.Interfaces;
using SuperMarket.Application.Services;
using SuperMarket.Domain.Interfaces;
using SuperMarket.Domain.PricingStrategies;

namespace SuperMarket.Application
{
    public class ApplicationDI
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<IPricingStrategy>(provider =>
            {
                var ruleSource = provider.GetRequiredService<IPricingRuleSource>();
                var rules = ruleSource.GetPackRules();
                return new PackPricingStrategy(rules);
            });

            return services;
        }
    }
}

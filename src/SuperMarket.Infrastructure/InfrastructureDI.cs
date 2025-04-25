using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SuperMarket.Infrastructure.DataProviders;
using SuperMarket.Infrastructure.Interfaces;

namespace SuperMarket.Infrastructure
{
    public static class InfrastructureDI
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IConfiguration>(configuration);
            services.AddSingleton<IDataSource, JsonConfigReader>();
            return services;
        }
    }
}

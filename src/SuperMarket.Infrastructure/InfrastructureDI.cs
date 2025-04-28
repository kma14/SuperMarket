using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SuperMarket.Application.Interfaces;
using SuperMarket.Infrastructure.DataProviders;
using SuperMarket.Infrastructure.Services;

namespace SuperMarket.Infrastructure
{
    public static class InfrastructureDI
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IConfiguration>(configuration);
            services.AddSingleton<IDataSource, JsonConfigReader>();
            services.AddSingleton<ITokenService, TokenService>();

            return services;
        }
    }
}

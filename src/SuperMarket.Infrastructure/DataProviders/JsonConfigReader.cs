using Microsoft.Extensions.Configuration;
using SuperMarket.Infrastructure.Interfaces;

namespace SuperMarket.Infrastructure.DataProviders
{
    public class JsonConfigReader : IDataSource
    {
        private readonly IConfiguration _configuration;

        public JsonConfigReader(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public T Load<T>(string key)
        {
            return _configuration.GetSection(key).Get<T>();
        }
    }
}

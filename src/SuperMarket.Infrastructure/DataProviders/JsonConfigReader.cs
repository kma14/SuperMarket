using Microsoft.Extensions.Configuration;
using SuperMarket.Application.Interfaces;

namespace SuperMarket.Infrastructure.DataProviders
{
    /// <summary>
    /// Reads configuration sections from application's configuration(appsettings.json)
    /// 
    /// Looks thin but abstract away the underlying configuration system from application layer
    /// </summary>
    public class JsonConfigReader : IDataSource
    {
        private readonly IConfiguration _configuration;

        public JsonConfigReader(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Loads a specific configuration section and deserializes it into the specified type.
        /// 
        /// Flexiable with generic type
        /// </summary>
        public T Load<T>(string key)
        {
            return _configuration.GetSection(key).Get<T>();
        }
    }
}

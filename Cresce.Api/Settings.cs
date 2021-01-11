using Microsoft.Extensions.Configuration;

namespace Cresce.Api
{
    public class Settings
    {
        private readonly IConfiguration _configuration;
        public const string Secret = "fedaf7d8863b48e197b9287d492b708e";

        public Settings(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string ConnectionString => _configuration.GetConnectionString("default");

        public string Version => _configuration["Version"];
    }
}


using Microsoft.Extensions.Configuration;

namespace Cresce.Core
{
    public class Settings
    {
        private readonly IConfiguration? _configuration;

        public Settings(IConfiguration? configuration) => _configuration = configuration;

        public string ConnectionString => _configuration.GetConnectionString("default");

        public string Version => _configuration != null ? _configuration["Version"] : "";

        public string AppKey => _configuration != null ? _configuration["AppKey"] : "fedaf7d8863b48e197b9287d492b708e";
    }
}

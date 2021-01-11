using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Cresce.Api.Controllers
{
    [ApiController]
    [Route("")]
    public class HealthController : ControllerBase
    {
        private Settings _settings;

        public HealthController(Settings settings)
        {
            _settings = settings;
        }

        [HttpGet]
        public HealthDto Get() =>
            new()
            {
                ServerTime = DateTime.UtcNow,
                Health = HealthStatus.Healthy,
                ApiVersion = _settings.Version,
                ConnectionString = _settings.ConnectionString,
                EnvConnectionString = Environment.GetEnvironmentVariable("SQLCONNSTR_CONNECTION_STRING")
            };
    }
}

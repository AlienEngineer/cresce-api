using System;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Cresce.Api.Controllers
{
    public record HealthDto
    {
        public DateTime ServerTime { get; init; } = DateTime.Now;
        public HealthStatus Health { get; init; } = HealthStatus.Healthy;
        public string ApiVersion { get; init; } = string.Empty;
        public string ConnectionString { get; set; } = string.Empty;
        public string? EnvConnectionString { get; set; }
    }
}

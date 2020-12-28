using System;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Cresce.Api.Controllers
{
    public record HealthDto
    {
        public DateTime ServerTime { get; init; }
        public HealthStatus Health { get; init; }
        public string ApiVersion { get; init; }
    }
}

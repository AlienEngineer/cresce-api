using System;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Cresce.Api.Controllers
{
    public class HealthDto
    {
        public DateTime ServerTime { get; set; }
        public HealthStatus Health { get; set; }
    }
}
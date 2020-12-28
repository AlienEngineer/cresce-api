using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Cresce.Api.Controllers
{
    [ApiController]
    [Route("")]
    public class HealthController : ControllerBase
    {
        [HttpGet]
        public HealthDto Get() =>
            new()
            {
                ServerTime = DateTime.UtcNow,
                Health = HealthStatus.Healthy,
                ApiVersion = new Version(0, 0, 1).ToString()
            };
    }
}

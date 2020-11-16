﻿using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Cresce.Api.Controllers
{
    [ApiController]
    [Route("")]
    public class HealthController : ControllerBase
    {
        [HttpGet]
        public HealthDto Get()
        {
            return new HealthDto
            {
                ServerTime = DateTime.UtcNow,
                Health = HealthStatus.Healthy
            };
        }
    }

    public class HealthDto
    {
        public DateTime ServerTime { get; set; }
        public HealthStatus Health { get; set; }
    }
}
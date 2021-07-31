using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lesson001.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostRecipient : ControllerBase
    {
        private readonly ILogger<PostRecipient> _logger;

        public PostRecipient(ILogger<PostRecipient> logger)
        {
            _logger = logger;
        }

        [HttpGet ("GET /posts/{id}")] 
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}

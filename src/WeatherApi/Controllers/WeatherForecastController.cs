using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace WeatherApi
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static object _locker = new();
        private static readonly string[] weatherSummaries = new[]
            {
                "Muzlatuvchi", "Sovuq", "Salqinroq", "Salqin", "Iliq-miliq", "Iliq", "Issiqroq", "Issiq", "Qaynoq", "Kuydiruvchi"            
            };

        [HttpGet]
        public IActionResult Get()
        {
            Random random = new();
            int randomValue = 0;
            lock (_locker)
            {
                randomValue = random.Next(0, 9);
            }

            if (randomValue % 3 == 0)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }

            return Ok(weatherSummaries[randomValue]);
        }
    }
}
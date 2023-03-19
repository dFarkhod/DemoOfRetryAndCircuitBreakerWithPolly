using Microsoft.AspNetCore.Mvc;

namespace WeatherMiddleware
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var client = _httpClientFactory.CreateClient(Constants.HTTP_CLIENT_NAME);
            return Ok(await client.GetStringAsync(Constants.WEATHER_API_URL_PATH));
        }
    }
}
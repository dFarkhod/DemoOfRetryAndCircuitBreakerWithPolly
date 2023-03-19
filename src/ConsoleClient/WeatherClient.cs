namespace ConsoleClientWithRetry
{
    public class WeatherClient
    {
        private readonly HttpClient httpClient;
        public WeatherClient()
        {
            httpClient = new HttpClient();
        }

        public string GetWeather()
        {
            return Constants.Retry3TimesWith3SecondsIntervalPolicy.Execute(() =>
            {
                var response = httpClient.GetAsync(Constants.WEATHER_API_URL).GetAwaiter().GetResult();
                response.EnsureSuccessStatusCode();
                return response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            });
        }
    }
}

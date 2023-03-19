using Polly;
using Polly.CircuitBreaker;
using Polly.Retry;

namespace ConsoleClientWithRetry
{
    public class Constants
    {
        public const int MAX_RETRIES_COUNT = 3;
        public const int RETRY_INTERVAL_SEC = 3;
        public const string WEATHER_API_URL = "http://localhost:5197/weatherforecast";

        public static RetryPolicy Retry3TimesWith3SecondsIntervalPolicy = 
                                            Policy.Handle<Exception>()
                                            .WaitAndRetry(MAX_RETRIES_COUNT, 
                                            count => TimeSpan.FromSeconds(RETRY_INTERVAL_SEC),
                                            onRetry: (exception, sleepDuration, attemptNumber, context) =>
                                            {
                                                Console.WriteLine($"Xato ro'y berdi. {sleepDuration} soniyada qaytadan urinib ko'ramiz. Qayta urinish: {attemptNumber} chi / {MAX_RETRIES_COUNT} dan");
                                            });

        public static RetryPolicy RetryForeverWith3SecondsIntervalPolicy = Policy.Handle<Exception>()
                                                       .WaitAndRetryForever(retryAttempt => TimeSpan.FromSeconds(RETRY_INTERVAL_SEC));


        public static CircuitBreakerPolicy CircuitBreakerPolicy = Policy.Handle<Exception>()
            .CircuitBreaker(3, TimeSpan.FromSeconds(30),
                onBreak: (exception, timespan) =>
                {
                    Console.WriteLine($"Circuit breaker opened due to {exception.Message}. Will retry in {timespan.TotalSeconds} seconds.");
                },
                onReset: () =>
                {
                    Console.WriteLine("Circuit breaker reset.");
                },
                onHalfOpen: () =>
                {
                    Console.WriteLine("Circuit breaker half-opened.");
                });

    }
}

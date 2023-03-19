using Polly;
using WeatherMiddleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient(Constants.HTTP_CLIENT_NAME, client =>
    {
        client.BaseAddress = new Uri(Constants.WEATHER_API_BASE_URL);
    })
    .AddTransientHttpErrorPolicy(builder => builder.WaitAndRetryAsync(new[]
    {
        TimeSpan.FromSeconds(1),
        TimeSpan.FromSeconds(3),
        TimeSpan.FromSeconds(7)
    }, 
    onRetry: (exception, sleepDuration, attemptNumber, context) =>
    {
        Console.WriteLine($"Xato ro'y berdi. {sleepDuration} soniyada qaytadan urinib ko'ramiz. Qayta urinish: {attemptNumber}");
    }))
    .AddTransientHttpErrorPolicy(builder => builder.CircuitBreakerAsync(
        handledEventsAllowedBeforeBreaking: 2,
        durationOfBreak: TimeSpan.FromSeconds(10)
    ));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

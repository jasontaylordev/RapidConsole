using AwesomeConsole.WeatherForecasts;
using Microsoft.Extensions.Logging;

namespace AwesomeConsole;

public class WeatherForecastService
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild",
        "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger _logger;

    public WeatherForecastService(ILogger<WeatherForecastService> logger)
    {
        _logger = logger;
    }

    public IEnumerable<WeatherForecast> GetForecasts(int count)
    {
        _logger.LogDebug("Getting {count} forecasts.", count);
        
        var rng = new Random();

        var forecasts = Enumerable.Range(1, count).Select(index =>
            new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            });

        return forecasts;
    }
}
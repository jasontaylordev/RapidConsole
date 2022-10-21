using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using Spectre.Console;
using Spectre.Console.Cli;

namespace RapidConsole.WeatherForecasts;

public class WeatherForecastCommand : Command<WeatherForecastSettings>
{
    private readonly IConfiguration _configuration;

    private readonly WeatherForecastService _weatherForecastService;

    public WeatherForecastCommand(IConfiguration configuration, WeatherForecastService weatherForecastService)
    {
        _configuration = configuration;
        _weatherForecastService = weatherForecastService;
    }

    public override int Execute([NotNull] CommandContext context, [NotNull] WeatherForecastSettings settings)
    {
        var unit = settings.Unit ?? 
                   _configuration.GetValue<TemperatureUnit>("Unit");

        var forecasts = _weatherForecastService.GetForecasts(settings.Count);

        var table = new Table();

        table.AddColumn("Date");
        table.AddColumn("Temp");
        table.AddColumn("Summary");

        foreach (var forecast in forecasts)
        {
            var temperature = unit == TemperatureUnit.Fahrenheit
                ? forecast.TemperatureF
                : forecast.TemperatureC;

            table.AddRow(
                forecast.Date.ToShortDateString(),
                temperature.ToString(),
                forecast.Summary);
        }

        table.Expand();

        AnsiConsole.Write(table);

        return 0;
    }
}
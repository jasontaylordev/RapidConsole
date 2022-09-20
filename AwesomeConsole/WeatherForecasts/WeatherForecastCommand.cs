using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using Spectre.Console;
using Spectre.Console.Cli;

namespace AwesomeConsole.WeatherForecasts;

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
        var unit = _configuration.GetValue<TemperatureUnit>("Unit");

        var forecasts = _weatherForecastService.GetForecasts(settings.Count);

        var table = new Table();

        table.AddColumn("Date");
        table.AddColumn("Temp");
        table.AddColumn("Summary");

        foreach (var forecast in forecasts)
        {
            var temperature = unit == TemperatureUnit.Celsius
                ? forecast.TemperatureC
                : forecast.TemperatureF;

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
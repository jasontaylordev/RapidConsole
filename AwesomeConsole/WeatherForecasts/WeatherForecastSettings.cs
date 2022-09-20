using System.ComponentModel;
using Spectre.Console.Cli;

namespace AwesomeConsole.WeatherForecasts;

public class WeatherForecastSettings : CommandSettings
{
    [Description("The number of weather forecasts to display.")]
    [CommandArgument(0, "[count]")]
    public int Count { get; set; }
}
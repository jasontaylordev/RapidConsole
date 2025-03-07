using Microsoft.Extensions.Logging;
using Moq;
using RapidConsole;

public class WeatherForecastServiceTests
{
    [Fact]
    public void GetForecasts_ReturnsCorrectNumberOfForecasts()
    {
        // Arrange
        var mockLogger = new Mock<ILogger<WeatherForecastService>>();
        var service = new WeatherForecastService(mockLogger.Object);

        // Act
        var forecasts = service.GetForecasts(5);

        // Assert
        Assert.Equal(5, forecasts.Count());
    }
}
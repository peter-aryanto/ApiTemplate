using Template1.Controllers;
using Template1.Logics;
using Moq;
using Template1.Models;

namespace Template1UnitTests;

public class WeatherForecastControllerTests
{
    private readonly WeatherForecastController sut;
    private readonly Mock<IWeatherForecastLogic> mockLogic;

    public WeatherForecastControllerTests()
    {
        mockLogic = new Mock<IWeatherForecastLogic>();
        sut = new WeatherForecastController(mockLogic.Object);
    }

    [Fact]
    public void Get_ShouldReturnCorrectOutput()
    {
        var dummyExpected = new WeatherForecast[] {};
        mockLogic.Setup(x => x.Get(3))
            .Returns(dummyExpected);
        var output = sut.Get();
        Assert.Same(dummyExpected, output);
    }
}
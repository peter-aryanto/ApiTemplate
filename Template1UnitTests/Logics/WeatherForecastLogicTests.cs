using System.Collections.Generic;
using Template1.Logics;
using Template1.Models;

namespace Template1UnitTests;

public class WeatherForecastLogicTests
{
    private IWeatherForecastLogic sut;

    public WeatherForecastLogicTests()
    {
        sut = new WeatherForecastLogic();
    }

    [Fact]
    public void GetWithValidDayCountParam_ShouldReturnCorrectDayCount()
    {
        int dayCount;
        IEnumerable<WeatherForecast> output;

        dayCount = 1;
        output = sut.Get(dayCount);
        Assert.Equal(dayCount, output.Count());

        dayCount = 10;
        output = sut.Get(dayCount);
        Assert.Equal(dayCount, output.Count());
    }

    [Fact]
    public void GetWithInvalidDayCountParam_ShouldReturnDefaultDayCount()
    {
        int dayCount;
        IEnumerable<WeatherForecast> output;

        dayCount = 0;
        output = sut.Get(dayCount);
        Assert.Equal(WeatherForecastLogic.DefaultDayCount, output.Count());
    }

    [Fact]
    public void GetWithoutDayCountParam_ShouldReturnDefaultDayCount()
    {
        IEnumerable<WeatherForecast> output;

        output = sut.Get();
        Assert.Equal(WeatherForecastLogic.DefaultDayCount, output.Count());
    }
}
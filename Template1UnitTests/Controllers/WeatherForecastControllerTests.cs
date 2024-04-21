using Template1.Controllers;
using Template1.Logics;
using Moq;
using Template1.Models;
using RestSharp;
using Template1.Entities;

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

    [Fact]
    public async Task GetGoogle_ShouldReturnCorrectOutput()
    {
        var dummyExpected = "DUMMY";
        mockLogic.Setup(x => x.GetGoogleAsync(It.IsAny<IRestClient>()))
            .Returns(Task.FromResult(dummyExpected));
        var output = await sut.GetGoogle();
        Assert.Same(dummyExpected, output);
    }

    //qwe2
    [Fact]
    public async Task CreateInDb_ShouldReturnCreatedRecord()
    {
        var dummyExpected = new KeyValue();
        mockLogic.Setup(x => x.CreateKeyValueAsync(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(dummyExpected);
#pragma warning disable CS8604 // Possible null reference argument.
        var output = await sut.CreateInDbAsync(dummyExpected.Value1, dummyExpected.Value2);
        Assert.Same(dummyExpected, output);
    }

    [Fact]
    public async Task ReadFromDb_ShouldReturnRecordList()
    {
        var dummyExpected = new List<KeyValue>();
        mockLogic.Setup(x => x.GetKeyValuesAsync())
            .ReturnsAsync(dummyExpected);
        var output = await sut.ReadFromDbAsync();
        Assert.Same(dummyExpected, output);
    }
}
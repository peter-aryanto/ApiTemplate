using System.Collections.Generic;
using Moq;
using RestSharp;
using Template1.Logics;
using Template1.Models;
using Template1.Entities.Queries;
using Template1.Entities;

namespace Template1UnitTests;

public class WeatherForecastLogicTests
{
    private IWeatherForecastLogic sut;
    private Mock<IKeyValueQueries> mockKeyValueQueries;

    public WeatherForecastLogicTests()
    {
        mockKeyValueQueries = new Mock<IKeyValueQueries>();
        mockKeyValueQueries.Setup(x => x.GetAsync())
            .ReturnsAsync(new List<KeyValue>());
        sut = new WeatherForecastLogic(mockKeyValueQueries.Object);
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

    [Fact]
    public async Task GetGoogle_ShouldReturnGoogleSearch()
    {
        const string ExpectedGS = "Google Search";
        var expectedRes = new RestResponse()
        {
            StatusCode = System.Net.HttpStatusCode.OK,
            Content = $"...{ExpectedGS}..."
        };
        var mockRestClient = new Mock<IRestClient>();
        mockRestClient.Setup(x => x.ExecuteAsync(It.IsAny<RestRequest>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(expectedRes));
        var res = await sut.GetGoogleAsync(mockRestClient.Object);
        Assert.Equal($"{expectedRes.StatusCode} - {ExpectedGS}", res);
    }

    //qwe4
    [Fact]
    public async Task CreateKeyalues_ShouldReturnCreatedKeyValue()
    {
        var expected = new KeyValue { KeyValueId = 1, Value1 = "Val 1", Value2 = "Val 2" };
        mockKeyValueQueries.Setup(x => x.CreateAsync(It.Is<string>(y => y.Contains(DateTime.UtcNow.Year.ToString())), expected.Value1, expected.Value2))
            .ReturnsAsync(expected);
        var output = await sut.CreateKeyValueAsync(expected.Value1, expected.Value2);
        Assert.Same(expected, output);

        expected = new KeyValue();
        output = await sut.CreateKeyValueAsync(null, null);
        Assert.Equal(-1, output.KeyValueId);
    }

    [Fact]
    public async Task GetKeyalues_ShouldReturnKeyValueList()
    {
        var expected = new List<KeyValue>();
        mockKeyValueQueries.Setup(x => x.GetAsync())
            .ReturnsAsync(expected);
        var output = await sut.GetKeyValuesAsync();
        // Assert.True(output is List<KeyValue>);
        Assert.Same(expected, output);
    }
}
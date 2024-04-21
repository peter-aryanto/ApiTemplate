using System.Collections.Generic;
using Moq;
using EntityFrameworkCoreMock;
using Moq.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Template1.Entities.Queries;
using Template1.Entities;
using Template1;

namespace Template1UnitTests;

public class KeyValueQueriesTests
{
    private IKeyValueQueries sut;
    private DbContextMock<Context1> mockContext;
    private KeyValueQueries sut2;
    private Mock<Context1> mockContext2;
    private List<KeyValue> testKeyValues;
    private List<AdditionalInfo> testAdditionalInfos;

    public KeyValueQueriesTests()
    {
        mockContext = new DbContextMock<Context1>();
        testKeyValues =
        [
            new() { KeyValueId = 1, Key = "One", Value1 = "1a", Value2 = "1b" },
            new() { KeyValueId = 2, Key = "Two", Value1 = "2a", Value2 = "2b" },
            new() { KeyValueId = 3, Key = "Three", Value1 = "3a", Value2 = "3b" },
        ];
        testAdditionalInfos =
        [
            new() { AdditionalInfoId = 1, KeyValueId = testKeyValues[0].KeyValueId, KeyValue = testKeyValues[0] },
        ];
        testKeyValues[0].AdditionalInfos.Add(testAdditionalInfos[0]);
        mockContext.CreateDbSetMock(x => x.KeyValues, testKeyValues);
        mockContext.CreateDbSetMock(x => x.AdditionalInfos, testAdditionalInfos);
        sut = new KeyValueQueries(mockContext.Object);

        mockContext2 = new Mock<Context1>();
        mockContext2.Setup(x => x.KeyValues).ReturnsDbSet(testKeyValues);
        sut2 = new KeyValueQueries(mockContext2.Object);

        // var mockKeyValueDbSet = CreateMockDbSet(testKeyValues.AsQueryable());
        // mockKeyValueDbSet.Setup(x => x.Include(nameof(KeyValue.AdditionalInfos))).Returns(mockKeyValueDbSet.Object);
        // mockContext2 = new Mock<Context1>();
        // mockContext2.Setup(x => x.KeyValues).Returns(mockKeyValueDbSet.Object);
        // sut = new KeyValueQueries(mockContext2.Object);
    }

    // private Mock<DbSet<T>> CreateMockDbSet<T>(IQueryable<T> entities) where T : class
    // {
    //     var mockSet = new Mock<DbSet<T>>();
    //     mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(entities.Provider);
    //     mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(entities.Expression);
    //     mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(entities.ElementType);
    //     mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(entities.GetEnumerator());
    //     return mockSet;
    // }

    [Fact]
    public async Task Get_ShouldReturnKeyValueList()
    {
      var output = await sut.GetAsync();
      Assert.Equal(testKeyValues.Count, output.Count);
      Assert.Equal(testKeyValues[0].AdditionalInfos.First(), output[0].AdditionalInfos.First());

      var output2 = await sut2.GetAsync();
      Assert.Equal(testKeyValues.Count, output2.Count);
      Assert.Equal(testKeyValues[0].AdditionalInfos.First(), output2[0].AdditionalInfos.First());
    }
}
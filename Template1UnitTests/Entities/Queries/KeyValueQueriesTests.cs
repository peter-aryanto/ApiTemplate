using System.Collections.Generic;
using Moq;
using EntityFrameworkCoreMock;
using Moq.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Template1.Entities.Queries;
using Template1.Entities;
using Template1;
using System.Linq.Expressions;

namespace Template1UnitTests;

public class KeyValueQueriesTests
{
    private IKeyValueQueries sut;
    private DbContextMock<Context1> mockContext;
    private KeyValueQueries sut2;
    private Mock<Context1> mockContext2;
    private KeyValueQueries sut3;
    private Mock<Context1> mockContext3;
    private Mock<DbSet<KeyValue>> mockKeyValueDbSet;
    private Expression<Func<KeyValue, bool>> IsAddingCreatedKeyValue = (KeyValue x) => false;
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
        mockContext.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).Verifiable();
        sut = new KeyValueQueries(mockContext.Object);

        mockContext2 = new Mock<Context1>();
        mockContext2.Setup(x => x.KeyValues).ReturnsDbSet(testKeyValues);
        mockContext2.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).Verifiable();
        sut2 = new KeyValueQueries(mockContext2.Object);

        // mockKeyValueDbSet = CreateMockDbSet(testKeyValues.AsQueryable());
        mockKeyValueDbSet = new Mock<DbSet<KeyValue>>();
        mockKeyValueDbSet.Setup(x => x.Add(It.Is<KeyValue>(IsAddingCreatedKeyValue))).Verifiable();
        // mockKeyValueDbSet.Setup(x => x.Include(nameof(KeyValue.AdditionalInfos))).Returns(mockKeyValueDbSet.Object);
        mockContext3 = new Mock<Context1>();
        // mockContext3.Setup(x => x.KeyValues).ReturnsDbSet(mockKeyValueDbSet.Object);
        mockContext3.Setup(x => x.KeyValues).Returns(mockKeyValueDbSet.Object);
        mockContext3.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).Verifiable();
        sut3 = new KeyValueQueries(mockContext3.Object);
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

    //qwe7
    [Fact]
    public async Task Create_ShouldReturnCreatedKeyValue()
    {
        var key = "Key";
        var val1 = "Val 1";
        var val2 = "Val 2";
        var output = await sut.CreateAsync(key, val1, val2);
        mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        Assert.Equal(key, output.Key);
        Assert.Equal(val1, output.Value1);
        Assert.Equal(val2, output.Value2);

        output = await sut2.CreateAsync(key, val1, val2);
        mockContext2.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        Assert.Equal(key, output.Key);
        Assert.Equal(val1, output.Value1);
        Assert.Equal(val2, output.Value2);

        output = await sut3.CreateAsync(key, val1, val2);
        IsAddingCreatedKeyValue = (KeyValue x) => x.Key == key && x.Value1 == val1 && x.Value2 == val2;
        mockKeyValueDbSet.Verify(x => x.Add(It.Is<KeyValue>(IsAddingCreatedKeyValue)), Times.Once);
        mockContext2.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        Assert.Equal(key, output.Key);
        Assert.Equal(val1, output.Value1);
        Assert.Equal(val2, output.Value2);

        var LocalIsAddingCreatedKeyValue = (KeyValue x) => x.Key == key && x.Value1 == val1 && x.Value2 == val2;
        // var localMockKeyValueDbSet = CreateMockDbSet(testKeyValues.AsQueryable());
        var localMockKeyValueDbSet = new Mock<DbSet<KeyValue>>();
        localMockKeyValueDbSet.Setup(x => x.Add(It.Is<KeyValue>(y => LocalIsAddingCreatedKeyValue(y)))).Verifiable();
        // localMockKeyValueDbSet.Setup(x => x.Include(nameof(KeyValue.AdditionalInfos))).Returns(localMockKeyValueDbSet.Object);
        var localMockContext = new Mock<Context1>();
        // localMockContext.Setup(x => x.KeyValues).ReturnsDbSet(mockKeyValueDbSet.Object);
        localMockContext.Setup(x => x.KeyValues).Returns(localMockKeyValueDbSet.Object);
        localMockContext.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).Verifiable();
        var localSut = new KeyValueQueries(localMockContext.Object);
        output = await localSut.CreateAsync(key, val1, val2);
        localMockKeyValueDbSet.Verify(x => x.Add(It.Is<KeyValue>(y => LocalIsAddingCreatedKeyValue(y))), Times.Once);
        localMockContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        Assert.Equal(key, output.Key);
        Assert.Equal(val1, output.Value1);
        Assert.Equal(val2, output.Value2);
    }

    [Fact]
    public async Task Get_ShouldReturnKeyValueList()
    {
        var output = await sut.GetAsync();
        Assert.Equal(testKeyValues.Count, output.Count);
        Assert.Equal(testKeyValues[0].AdditionalInfos.First(), output[0].AdditionalInfos.First());

        output = await sut2.GetAsync();
        Assert.Equal(testKeyValues.Count, output.Count);
        Assert.Equal(testKeyValues[0].AdditionalInfos.First(), output[0].AdditionalInfos.First());
    }
}
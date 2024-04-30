using Template1Console1;

namespace Template1UnitTests.Console1;

public class Process01Tests
{
    private IProcess01 sut;

    public Process01Tests()
    {
        sut = new Process01();
    }

    [Fact]
    public void TestSomething()
    {
        Assert.True(sut.DoProcess());
    }
}
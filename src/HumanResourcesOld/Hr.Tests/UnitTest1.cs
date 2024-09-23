using Hr.Api;

namespace Hr.Tests;

public class CalculatorTests
{
    [Fact]// This is a "fact" your test only proves one thing.
    public void CanAddTwoAndTwoToGetFour()
    {
        // Given (Arrange)
        var myCalculator = new Calculator();
        // When (Act)
        int result = myCalculator.Add(2, 2); // System Under Test (SUT)
        // Then (Assert)
        Assert.Equal(4, result);
    }

    [Theory]
    [InlineData(2, 2, 4)]
    [InlineData(2, 3, 5)]
    [InlineData(1, 3, 4)]

    public void CanAddAnyTwoIntegers(int a, int b, int expected)
    {
        var myCalculator = new Calculator();

        var result = myCalculator.Add(a, b);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void CanAddTwoDecimals()
    {
        var myCalculator = new Calculator();

        var result = myCalculator.Add(1.50M, 1.50M);

        Assert.Equal(3M, result);
    }

    //[Fact]
    //public void CanAddTwoStrings()
    //{
    //    var myCalculator = new Calculator();

    //    var fullName = myCalculator.Add("Han", "Solo");

    //    Assert.Equal("Solo, Han", fullName);
    //}

}
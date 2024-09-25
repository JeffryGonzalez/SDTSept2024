using Hr.Api;

namespace Hr.Tests;

public class CalculatorTests
{

    private Calculator calculator;

    public CalculatorTests()
    {
        calculator = new Calculator();
    }
    [Fact]// This is a "fact" your test only proves one thing.
    [Trait("Category", "Unit")]
    public void CanAddTwoAndTwoToGetFour()
    {
        // Given (Arrange)

        // When (Act)
        int result = calculator.Add(2, 2); // System Under Test (SUT)
        // Then (Assert)
        Assert.Equal(4, result);
    }

    [Theory]
    [Trait("Category", "Unit")]
    [InlineData(2, 2, 4)]
    [InlineData(2, 3, 5)]
    [InlineData(1, 3, 4)]

    public void CanAddAnyTwoIntegers(int a, int b, int expected)
    {
        var result = calculator.Add(a, b);
        Assert.Equal(expected, result);
    }

    [Fact]
    [Trait("Category", "Unit")]
    public void CanAddTwoDecimals()
    {


        var result = calculator.Add(1.50M, 1.50M);

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
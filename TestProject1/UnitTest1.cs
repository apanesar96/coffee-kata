using FluentAssertions;

namespace TestProject1;

public class Tests
{
    private CoffeeMachine _coffeeMachine;

    [SetUp]
    public void Setup()
    {
        _coffeeMachine = new CoffeeMachine();
    }

    [TestCase("Chocolate", "H::")]
    [TestCase("Tea", "T::")]
    [TestCase("Coffee", "C::")]
    public void GivenDrinksCommandForHotChocolate_OutputOrder(string drink, string expectedOutput) =>
        _coffeeMachine.Order(drink,0).Should().Be(expectedOutput);
    
    [TestCase("Chocolate",1, "H:1:0")]
    [TestCase("Chocolate",2, "H:2:0")]
    [TestCase("Tea",1, "T:1:0")]
    [TestCase("Tea",2, "T:2:0")]
    [TestCase("Coffee",1, "C:1:0")]
    [TestCase("Coffee",2, "C:2:0")]
    
    public void GivenAHotChocolateDrinkWithDifferentAmountsOfSugar_OutputOrder(string drink,int numberOfSugars, string expectedOutput)
    {
        var output = _coffeeMachine.Order(drink, numberOfSugars);
        output.Should().Be(expectedOutput);
    }
}

public class CoffeeMachine
{
    public string Order(string drink, int numberOfSugars)
    {
        var order = numberOfSugars > 0 ? $":{numberOfSugars}:0" : "::";
        return drink switch
        {
            "Tea" => $"T{order}",
            "Coffee" => $"C{order}",
            _ => $"H{order}"
        };
    }
}
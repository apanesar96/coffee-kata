using System.Security.Authentication.ExtendedProtection;
using CoffeeMachineApp;
using CoffeeMachineApp.Modules.Drink;
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
    [TestCase("Orange", "O::")]
    public void GivenDrinksCommand_OutputOrder(string drink, string expectedOutput) =>
        _coffeeMachine.Order(new DrinksOrder(drink, 100, false, 0)).Should().Be(expectedOutput);

    [TestCase("Chocolate", 1, "H:1:0")]
    [TestCase("Chocolate", 2, "H:2:0")]
    [TestCase("Tea", 1, "T:1:0")]
    [TestCase("Tea", 2, "T:2:0")]
    [TestCase("Coffee", 1, "C:1:0")]
    [TestCase("Coffee", 2, "C:2:0")]
    public void GivenAHotChocolateDrinkWithDifferentAmountsOfSugar_OutputOrder(string drink, int numberOfSugars,
        string expectedOutput)
    {
        var output = _coffeeMachine.Order(new DrinksOrder(Name: drink, 100, false, numberOfSugars));
        output.Should().Be(expectedOutput);
    }

    [Test]
    public void Given40cents_MakeTheOrderForDrinkTea()
    {
        var output = _coffeeMachine.Order(new DrinksOrder("Tea", 40, false, 0));
        output.Should().Be("T::");
    }

    [Test]
    public void Given50cents_MakeTheOrderForDrinkTea()
    {
        var output = _coffeeMachine.Order(new DrinksOrder("Tea", 50, false, 0));
        output.Should().Be("T::");
    }

    [Test]
    public void Given50Cents_MakeTheOrderForHotChocolate()
    {
        var output = _coffeeMachine.Order(new DrinksOrder("Chocolate", 50, false, 0));
        output.Should().Be("H::");
    }

    [Test]
    public void Given60Cents_MakeTheOrderForCoffee()
    {
        var output = _coffeeMachine.Order(new DrinksOrder("Coffee", 60, false, 0));
        output.Should().Be("C::");
    }

    [TestCase("Chocolate", "Hh::")]
    [TestCase("Tea", "Th::")]
    [TestCase("Coffee", "Ch::")]
    public void GivenAHotDrinkWithTemperatureExtraHot_OutputCorrectFormat(string drink, string expectedOutput) =>
        _coffeeMachine.Order(new DrinksOrder(drink, 100, true, 0)).Should().Be(expectedOutput);

    [Test]
    public void GivenAnOrangeJuiceHasBeenSelectedWithTheTemperatureExtraHot_OutputDoesNotIncludeExtraHotTemperature()
    {
        _coffeeMachine.Order(new DrinksOrder("Orange", 100, true, 0)).Should().Be("O::");
    }

    [Test]
    public void GivenAnOrangeJuiceHasBeenSelectedWithASugar_OutputDoesNotExtraSugar()
    {
        _coffeeMachine.Order(new DrinksOrder("Orange", 100, false, 1)).Should().Be("O::");
    }

    [TestCase("Tea", 30, 10)]
    [TestCase("Tea", 20, 20)]
    [TestCase("Chocolate", 40, 10)]
    [TestCase("Chocolate", 30, 20)]
    [TestCase("Coffee", 50, 10)]
    [TestCase("Coffee", 40, 20)]
    [TestCase("Orange", 50, 10)]
    [TestCase("Orange", 40, 20)]
    [TestCase("Orange", 30, 30)]
    public void GivenAnIncorrectAmountForTheDesiredDrink_CorrectMessageIsSent(string drink, int amountGivenInCents, int expectedAmountMissing)
    {
        var output = _coffeeMachine.Order(new DrinksOrder(drink, amountGivenInCents, false, 0));
        output.Should().Be($"M: Incorrect amount given. Please provide {expectedAmountMissing} cents more");
    }

    [Test]
    public void GivenATeaAtAPriceOf40Cents_WhenTheAmountGivenIsLessThanThePrice_ThenTheCorrectMessageIsSent()
    {
        var output = _coffeeMachine.Order(new DrinksOrder("Tea", 30, false, 0));
        output.Should().Be("M: Incorrect amount given. Please provide 10 cents more");
    }

    [Test]
    public void GivenAHotDrink_OutputCorrectFormat()
    {
        var output = _coffeeMachine.Order(new DrinksOrder("Tea", 40, false, 0));
        output.Should().Be("T::");
    }
}
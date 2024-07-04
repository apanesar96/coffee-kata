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

    [TestCase(DrinkType.Chocolate, "H::")]
    [TestCase(DrinkType.Tea, "T::")]
    [TestCase(DrinkType.Coffee, "C::")]
    [TestCase(DrinkType.Orange, "O::")]
    public void GivenDrinksCommand_OutputOrder(DrinkType drink, string expectedOutput) =>
        _coffeeMachine.Order(new DrinksOrderRequest(drink, 100, false, 0)).Should().Be(expectedOutput);

    [TestCase(DrinkType.Chocolate, 1, "H:1:0")]
    [TestCase(DrinkType.Chocolate, 2, "H:2:0")]
    [TestCase(DrinkType.Tea, 1, "T:1:0")]
    [TestCase(DrinkType.Tea, 2, "T:2:0")]
    [TestCase(DrinkType.Coffee, 1, "C:1:0")]
    [TestCase(DrinkType.Coffee, 2, "C:2:0")]
    public void GivenAHotChocolateDrinkWithDifferentAmountsOfSugar_OutputOrder(DrinkType drink, int numberOfSugars,
        string expectedOutput)
    {
        var output = _coffeeMachine.Order(new DrinksOrderRequest(Name: drink, MoneyGiven: 100, IsExtraHot: false, NumberOfSugars: numberOfSugars));
        output.Should().Be(expectedOutput);
    }

    [Test]
    public void Given40cents_MakeTheOrderForDrinkTea()
    {
        var output = _coffeeMachine.Order(new DrinksOrderRequest(DrinkType.Tea, 40, false, 0));
        output.Should().Be("T::");
    }

    [Test]
    public void Given50cents_MakeTheOrderForDrinkTea()
    {
        var output = _coffeeMachine.Order(new DrinksOrderRequest(DrinkType.Tea, 50, false, 0));
        output.Should().Be("T::");
    }

    [Test]
    public void Given50Cents_MakeTheOrderForHotChocolate()
    {
        var output = _coffeeMachine.Order(new DrinksOrderRequest(DrinkType.Chocolate, 50, false, 0));
        output.Should().Be("H::");
    }

    [Test]
    public void Given60Cents_MakeTheOrderForCoffee()
    {
        var output = _coffeeMachine.Order(new DrinksOrderRequest(DrinkType.Coffee, 60, false, 0));
        output.Should().Be("C::");
    }

    [TestCase(DrinkType.Chocolate, "Hh::")]
    [TestCase(DrinkType.Tea, "Th::")]
    [TestCase(DrinkType.Coffee, "Ch::")]
    public void GivenAHotDrinkWithTemperatureExtraHot_OutputCorrectFormat(DrinkType drink, string expectedOutput) =>
        _coffeeMachine.Order(new DrinksOrderRequest(drink, 100, true, 0)).Should().Be(expectedOutput);

    [Test]
    public void GivenAnOrangeJuiceHasBeenSelectedWithTheTemperatureExtraHot_OutputDoesNotIncludeExtraHotTemperature()
    {
        _coffeeMachine.Order(new DrinksOrderRequest(DrinkType.Orange, 100, true, 0)).Should().Be("O::");
    }

    [Test]
    public void GivenAnOrangeJuiceHasBeenSelectedWithASugar_OutputDoesNotExtraSugar()
    {
        _coffeeMachine.Order(new DrinksOrderRequest(DrinkType.Orange, 100, false, 1)).Should().Be("O::");
    }

    [TestCase(DrinkType.Tea, 30, 10)]
    [TestCase(DrinkType.Tea, 20, 20)]
    [TestCase(DrinkType.Chocolate, 40, 10)]
    [TestCase(DrinkType.Chocolate, 30, 20)]
    [TestCase(DrinkType.Coffee, 50, 10)]
    [TestCase(DrinkType.Coffee, 40, 20)]
    [TestCase(DrinkType.Orange, 50, 10)]
    [TestCase(DrinkType.Orange, 40, 20)]
    [TestCase(DrinkType.Orange, 30, 30)]
    public void GivenAnIncorrectAmountForTheDesiredDrink_CorrectMessageIsSent(DrinkType drink, int amountGivenInCents, int expectedAmountMissing)
    {
        var output = _coffeeMachine.Order(new DrinksOrderRequest(drink, amountGivenInCents, false, 0));
        output.Should().Be($"M: Incorrect amount given. Please provide {expectedAmountMissing} cents more");
    }

    [Test]
    public void GivenATeaAtAPriceOf40Cents_WhenTheAmountGivenIsLessThanThePrice_ThenTheCorrectMessageIsSent()
    {
        var output = _coffeeMachine.Order(new DrinksOrderRequest(DrinkType.Tea, 30, false, 0));
        output.Should().Be("M: Incorrect amount given. Please provide 10 cents more");
    }

    [Test]
    public void GivenAHotDrink_OutputCorrectFormat()
    {
        var output = _coffeeMachine.Order(new DrinksOrderRequest(DrinkType.Tea, 40, false, 0));
        output.Should().Be("T::");
    }
}
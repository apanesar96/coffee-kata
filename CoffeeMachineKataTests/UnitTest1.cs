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
        _coffeeMachine.Order(new Drink(drink, 100, false, 0), 100).Should().Be(expectedOutput);

    [TestCase("Chocolate", 1, "H:1:0")]
    [TestCase("Chocolate", 2, "H:2:0")]
    [TestCase("Tea", 1, "T:1:0")]
    [TestCase("Tea", 2, "T:2:0")]
    [TestCase("Coffee", 1, "C:1:0")]
    [TestCase("Coffee", 2, "C:2:0")]
    public void GivenAHotChocolateDrinkWithDifferentAmountsOfSugar_OutputOrder(string drink, int numberOfSugars,
        string expectedOutput)
    {
        var output = _coffeeMachine.Order(new Drink(Name: drink, 100, false, numberOfSugars), 100);
        output.Should().Be(expectedOutput);
    }

    [Test]
    public void Given40cents_MakeTheOrderForDrinkTea()
    {
        var output = _coffeeMachine.Order(new Drink("Tea", 40,false, 0), 40);
        output.Should().Be("T::");
    }

    [Test]
    public void Given50cents_MakeTheOrderForDrinkTea()
    {
        var output = _coffeeMachine.Order(new Drink("Tea", 50,false, 0), 50);
        output.Should().Be("T::");
    }
    
    [Test]
    public void Given50Cents_MakeTheOrderForHotChocolate()
    {
        var output = _coffeeMachine.Order(new Drink("Chocolate", 50,false, 0), 50);
        output.Should().Be("H::");
    }
    
    [Test]
    public void Given60Cents_MakeTheOrderForCoffee()
    {
        var output = _coffeeMachine.Order(new Drink("Coffee", 60,false, 0), 60);
        output.Should().Be("C::");
    }
    
    [TestCase("Chocolate", "Hh::")]
    [TestCase("Tea", "Th::")]
    [TestCase("Coffee", "Ch::")]
    public void GivenAHotDrinkWithTemperatureExtraHot_OutputCorrectFormat(string drink, string expectedOutput) => 
        _coffeeMachine.Order(new Drink(drink, 0, true, 0), 100).Should().Be(expectedOutput);

    [Test]
    public void GivenAnOrangeJuiceHasBeenSelectedWithTheTemperatureExtraHot_OutputDoesNotIncludeExtraHotTemperature()
    {
        _coffeeMachine.Order(new Drink("Orange", 50, true, 0), 100).Should().Be("O::");
    }
    
    [Test]
    public void GivenAnOrangeJuiceHasBeenSelectedWithASugar_OutputDoesNotExtraSugar()
    {
        _coffeeMachine.Order(new Drink("Orange", 5, false, 1), 100).Should().Be("O::");
    }

    [TestCase("Tea", 40,30, 10)]
    [TestCase("Tea", 40,20, 20)]
    [TestCase("Chocolate", 50,40, 10)]
    [TestCase("Chocolate", 50,30, 20)]
    [TestCase("Coffee", 60,50, 10)]
    [TestCase("Coffee", 60,40, 20)]
    [TestCase("Orange", 60,50, 10)]
    [TestCase("Orange", 60,40, 20)]
    [TestCase("Orange", 60,30, 30)]
    public void GivenAnIncorrectAmountForTheDesiredDrink_CorrectMessageIsSent(string drink, int drinkPrice,int amountGivenInCents, int expectedAmountMissing)
    {
        var output = _coffeeMachine.Order(new Drink(drink,  drinkPrice, false, 0), amountGivenInCents);
        output.Should().Be($"M: Incorrect amount given. Please provide {expectedAmountMissing} cents more");
    }
    
    [Test]
    public void GivenATeaAtAPriceOf40Cents_WhenTheAmountGivenIsLessThanThePrice_ThenTheCorrectMessageIsSent()
    {
        var output = _coffeeMachine.Order(new Drink("Tea", 40, false, 0), 30);
        output.Should().Be("M: Incorrect amount given. Please provide 10 cents more");
    }
    
    [Test]
    public void GivenAHotDrink_OutputCorrectFormat()
    {
        var output = _coffeeMachine.Order(new Drink("Tea", 40, false, 0), 40);
        output.Should().Be("T::");
    }
}
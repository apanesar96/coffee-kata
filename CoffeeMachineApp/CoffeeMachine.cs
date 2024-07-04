using CoffeeMachineApp.Modules.Drink;

namespace CoffeeMachineApp;

public class CoffeeMachine
{
    private const string Tea = "Tea";
    private const string Chocolate = "Chocolate";
    private const string Coffee = "Coffee";
    private const string Orange = "Orange";

    public string Order(DrinksOrderRequest selectedDrinksOrderRequest)
    {
        var drink = GetAvailableDrink(selectedDrinksOrderRequest.Name);
        if (selectedDrinksOrderRequest.MoneyGiven < drink.Price)
        {
            return $"M: Incorrect amount given. Please provide {drink.Price - selectedDrinksOrderRequest.MoneyGiven} cents more";
        }

        var order = selectedDrinksOrderRequest.NumberOfSugars > 0 ? $":{selectedDrinksOrderRequest.NumberOfSugars}:0" : "::";

        return selectedDrinksOrderRequest.Name switch
        {
            DrinkType.Tea => GenerateOrder("T", order, selectedDrinksOrderRequest.IsExtraHot),
            DrinkType.Coffee => GenerateOrder("C", order, selectedDrinksOrderRequest.IsExtraHot),
            DrinkType.Chocolate => GenerateOrder("H", order, selectedDrinksOrderRequest.IsExtraHot),
            DrinkType.Orange => $"O::",
            _ => throw new ArgumentOutOfRangeException(nameof(selectedDrinksOrderRequest), selectedDrinksOrderRequest, null)
        };
    }

    Drink GetAvailableDrink(DrinkType name)
    {
        return name switch
        {
            DrinkType.Tea => new Drink(Tea, 40),
            DrinkType.Coffee => new Drink(Coffee, 60),
            DrinkType.Chocolate => new Drink(Chocolate, 50),
            DrinkType.Orange => new Drink(Orange, 60),
            _ => throw new ArgumentOutOfRangeException(nameof(name), name, null)
        };
    }

    static string GenerateOrder(string drink, string order, bool isExtraHot) =>
        isExtraHot ? $"{drink}h{order}" : $"{drink}{order}";
}
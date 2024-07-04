using CoffeeMachineApp.Modules.Drink;

namespace CoffeeMachineApp;

public class CoffeeMachine
{
    private const string Tea = "Tea";
    private const string Chocolate = "Chocolate";
    private const string Coffee = "Coffee";
    private const string Orange = "Orange";

    private readonly Dictionary<string, int> _priceLookup = new()
    {
        { Tea, 40 },
        { Coffee, 60 },
        { Chocolate, 50 },
        { Orange, 60 }
    };

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
            Tea => GenerateOrder("T", order, selectedDrinksOrderRequest.IsExtraHot),
            Coffee => GenerateOrder("C", order, selectedDrinksOrderRequest.IsExtraHot),
            Chocolate => GenerateOrder("H", order, selectedDrinksOrderRequest.IsExtraHot),
            Orange => $"O::",
            _ => throw new ArgumentOutOfRangeException(nameof(selectedDrinksOrderRequest), selectedDrinksOrderRequest, null)
        };
    }

    Drink GetAvailableDrink(string name)
    {
        return name switch
        {
            Tea => new Drink(Tea, 40),
            Coffee => new Drink(Coffee, 60),
            Chocolate => new Drink(Chocolate, 50),
            Orange => new Drink(Orange, 60),
        };
    }

    static string GenerateOrder(string drink, string order, bool isExtraHot) =>
        isExtraHot ? $"{drink}h{order}" : $"{drink}{order}";
}
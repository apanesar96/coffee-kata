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
    public string Order(DrinksOrder selectedDrinksOrder)
    {
        if (selectedDrinksOrder.MoneyGiven < _priceLookup[selectedDrinksOrder.Name])
        {
            return $"M: Incorrect amount given. Please provide {_priceLookup[selectedDrinksOrder.Name] -selectedDrinksOrder.MoneyGiven } cents more";
        }

        var order = selectedDrinksOrder.NumberOfSugars > 0 ? $":{selectedDrinksOrder.NumberOfSugars}:0" : "::";

        return selectedDrinksOrder.Name switch
        {
            Tea => GenerateOrder("T", order, selectedDrinksOrder.IsExtraHot),
            Coffee => GenerateOrder("C", order, selectedDrinksOrder.IsExtraHot),
            Chocolate => GenerateOrder("H", order, selectedDrinksOrder.IsExtraHot),
            Orange => $"O::",
            _ => throw new ArgumentOutOfRangeException(nameof(selectedDrinksOrder), selectedDrinksOrder, null)
        };
    }

    static string GenerateOrder(string drink, string order, bool isExtraHot) => 
        isExtraHot ? $"{drink}h{order}" : $"{drink}{order}";
}


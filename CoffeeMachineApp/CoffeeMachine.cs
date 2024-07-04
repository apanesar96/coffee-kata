using CoffeeMachineApp.Modules.Drink;

namespace CoffeeMachineApp;

public class CoffeeMachine
{
    private const string Tea = "Tea";
    private const string Chocolate = "Chocolate";
    private const string Coffee = "Coffee";
    private const string Orange = "Orange";

    public string Order(Drink selectedDrink, int moneyGiven)
    {
        if (moneyGiven < selectedDrink.Price)
        {
            return $"M: Incorrect amount given. Please provide {selectedDrink.Price - moneyGiven} cents more";
        }

        var order = selectedDrink.NumberOfSugars > 0 ? $":{selectedDrink.NumberOfSugars}:0" : "::";

        return selectedDrink.Name switch
        {
            Tea => GenerateOrder("T", order, selectedDrink.IsExtraHot),
            Coffee => GenerateOrder("C", order, selectedDrink.IsExtraHot),
            Chocolate => GenerateOrder("H", order, selectedDrink.IsExtraHot),
            Orange => $"O::",
            _ => throw new ArgumentOutOfRangeException(nameof(selectedDrink), selectedDrink, null)
        };
    }

    static string GenerateOrder(string drink, string order, bool isExtraHot) => 
        isExtraHot ? $"{drink}h{order}" : $"{drink}{order}";
}
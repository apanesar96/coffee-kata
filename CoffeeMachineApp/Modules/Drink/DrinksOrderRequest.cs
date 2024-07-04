namespace CoffeeMachineApp.Modules.Drink;

public record DrinksOrderRequest(string Name, int MoneyGiven, bool IsExtraHot, int NumberOfSugars);

public class Drink
{
    public Drink(string name, int price)
    {
        Name = name;
        Price = price;
    }

    public string Name { get; }
    public int Price { get; }
    
}

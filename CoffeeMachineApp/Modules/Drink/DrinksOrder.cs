namespace CoffeeMachineApp.Modules.Drink;

public record DrinksOrder(string Name, int MoneyGiven, bool IsExtraHot, int NumberOfSugars);

// public abstract record HotDrink(int Price, bool IsExtraHot, int NumberOfSugars) : Drink(Price);
//
// public abstract record ColdDrink(int Price) : Drink(Price);
//
// public record Tea(int Price, bool IsExtraHot) : HotDrink(Price, IsExtraHot);
// public record Coffee(int Price, bool IsExtraHot) : HotDrink(Price, IsExtraHot);
// public record Chocolate(int Price, bool IsExtraHot) : HotDrink(Price, IsExtraHot);
//
// public record Orange(int Price) : ColdDrink(Price);
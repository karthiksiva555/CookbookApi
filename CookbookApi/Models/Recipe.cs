namespace CookbookApi.Models;

public record Recipe(int Id, string Name, IList<Ingredient> Ingredients);
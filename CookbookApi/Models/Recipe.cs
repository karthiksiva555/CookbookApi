namespace CookbookApi.Models;

public record Recipe(int Id, string Name, IEnumerable<Ingredient> Ingredients);
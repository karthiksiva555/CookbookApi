namespace CookbookApi.Models;

public record Recipe
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public IEnumerable<Ingredient> Ingredients { get; init;  } = new List<Ingredient>();
}
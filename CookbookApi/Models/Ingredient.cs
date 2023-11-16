namespace CookbookApi.Models;

public record Ingredient
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public string? Unit { get; set; }

    public Recipe? Recipe { get; set; }

    public int RecipeId { get; set; }
}
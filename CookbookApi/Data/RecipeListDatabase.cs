using CookbookApi.Models;
using CookbookApi.Utility;

namespace CookbookApi.Data;

public class RecipeListDatabase
{
    private readonly IList<Recipe> _recipes = new List<Recipe>
    {
        new() {Id = 1, Name = "Tomato Dhal", Ingredients = new List<Ingredient>
        {
            new() { Id = 1, Name = "Tomato", Unit = "Kilo Gram" },
            new() { Id = 2, Name = "Lentils", Unit = "Cup" }
        }},
        new() {Id = 2, Name = "Chicken Kurma", Ingredients = new List<Ingredient>
        {
            new() { Id = 3, Name = "Chicken", Unit = "Kilo Gram" },
            new() { Id = 4, Name = "Yogurt", Unit = "Cup" }
        }}
    };

    public IList<Recipe> GetRecipes() => _recipes;

    public Recipe GetRecipeById(int id)
    {
        return GetRecipe(id);
    }

    public void AddRecipe(Recipe recipe) => _recipes.Add(recipe);

    public void UpdateRecipe(int id, Recipe updatedRecipe)
    {
        var existingRecipe = GetRecipe(id);
        var index = _recipes.IndexOf(existingRecipe);
        _recipes[index] = updatedRecipe;
    }

    public void DeleteRecipe(int id)
    {
        var existingRecipe = GetRecipe(id);
        _recipes.Remove(existingRecipe);
    }

    private Recipe GetRecipe(int id)
    {
        var recipe = _recipes.FirstOrDefault(recipe => recipe.Id == id);
        if (recipe is null)
            throw new HttpException(StatusCodes.Status404NotFound, $"Item with Id {id} not found");
        
        return recipe;
    }
        

}
using CookbookApi.Models;
using CookbookApi.Utility;

namespace CookbookApi.Data;

public static class RecipeListDatabase
{
    private static IList<Recipe> _recipes = new List<Recipe>
    {
        new(1, "Tomato Dhal", new List<Ingredient>
        {
            new(1, "Tomato", "Kilo Gram"),
            new (2, "Lentils", "Cup")
        }),
        new(2, "Chicken Kurma", new List<Ingredient>
        {
            new(1, "Chicken", "Kilo Gram"),
            new (2, "Yogurt", "Cup")
        })
    };

    public static IList<Recipe> GetRecipes() => _recipes;

    public static Recipe GetRecipeById(int id)
    {
        return GetRecipe(id);
    }
        

    public static void AddRecipe(Recipe recipe) => _recipes.Add(recipe);

    public static void UpdateRecipe(int id, Recipe updatedRecipe)
    {
        var existingRecipe = GetRecipe(id);
        var index = _recipes.IndexOf(existingRecipe);
        _recipes[index] = updatedRecipe;
    }

    public static void DeleteRecipe(int id)
    {
        var existingRecipe = GetRecipe(id);
        _recipes.Remove(existingRecipe);
    }

    private static Recipe GetRecipe(int id)
    {
        var recipe = _recipes.FirstOrDefault(recipe => recipe.Id == id);
        if (recipe is null)
            throw new HttpException(StatusCodes.Status404NotFound, $"Item with Id {id} not found");
        
        return recipe;
    }
        

}
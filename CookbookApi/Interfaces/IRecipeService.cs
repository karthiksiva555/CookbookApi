using CookbookApi.Models;

namespace CookbookApi.Interfaces;

public interface IRecipeService
{
    IList<Recipe> GetRecipes();
    Recipe GetRecipeById(int id);
    void AddRecipe(Recipe recipe);
    void UpdateRecipe(int id, Recipe updatedRecipe);
    void DeleteRecipe(int id);
}
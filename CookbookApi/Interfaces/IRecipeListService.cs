using CookbookApi.Dtos;

namespace CookbookApi.Interfaces;

public interface IRecipeListService
{
    IList<RecipeDto> GetRecipes();
    RecipeDto GetRecipeById(int id);
    void AddRecipe(RecipeDto recipe);
    void UpdateRecipe(int id, RecipeDto updatedRecipe);
    void DeleteRecipe(int id);
}
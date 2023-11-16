using CookbookApi.Dtos;

namespace CookbookApi.Interfaces;

public interface IRecipeService
{
    Task<IList<RecipeDto>> GetRecipesAsync();

    Task<RecipeDto> GetRecipeByIdAsync(int id);

    Task AddRecipeAsync(RecipeDto recipeDto);

    Task UpdateRecipeAsync(int id, RecipeDto recipeDto);

    Task DeleteRecipeAsync(int id);
}
using CookbookApi.Interfaces;
using CookbookApi.Models;

namespace CookbookApi.Services;

public class RecipeService : IRecipeService
{
    private readonly IRepository<Recipe> _recipeRepository;

    public RecipeService(IRepository<Recipe> recipeRepository)
    {
        _recipeRepository = recipeRepository;
    }

    public IList<Recipe> GetRecipes()
    {
        return _recipeRepository.GetAll();
    }

    public Recipe GetRecipeById(int id)
    {
        return _recipeRepository.GetById(id);
    }

    public void AddRecipe(Recipe recipe)
    {
        _recipeRepository.Add(recipe);
    }

    public void UpdateRecipe(int id, Recipe updatedRecipe)
    {
        _recipeRepository.Update(id, updatedRecipe);
    }

    public void DeleteRecipe(int id)
    {
        _recipeRepository.Delete(id);
    }
}
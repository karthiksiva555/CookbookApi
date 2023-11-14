using CookbookApi.Data;
using CookbookApi.Interfaces;
using CookbookApi.Models;

namespace CookbookApi.Repositories;

public class RecipeRepository : IRepository<Recipe>
{
    public Recipe GetById(int id)
    {
        return RecipeListDatabase.GetRecipeById(id);
    }

    public IList<Recipe> GetAll()
    {
        return RecipeListDatabase.GetRecipes();
    }

    public void Add(Recipe recipe)
    {
        RecipeListDatabase.AddRecipe(recipe);
    }

    public void Update(int id, Recipe recipe)
    {
        RecipeListDatabase.UpdateRecipe(id, recipe);
    }

    public void Delete(int id)
    {
        RecipeListDatabase.DeleteRecipe(id);
    }
}
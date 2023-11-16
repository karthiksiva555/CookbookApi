using CookbookApi.Data;
using CookbookApi.Interfaces;
using CookbookApi.Models;

namespace CookbookApi.Repositories;

public class RecipeListRepositoryList : IRepositoryList<Recipe>
{
    private readonly RecipeListDatabase _listDatabase;

    public RecipeListRepositoryList(RecipeListDatabase listDatabase)
    {
        _listDatabase = listDatabase;
    }
    
    public Recipe GetById(int id)
    {
        return _listDatabase.GetRecipeById(id);
    }

    public IList<Recipe> GetAll()
    {
        return _listDatabase.GetRecipes();
    }

    public void Add(Recipe recipe)
    {
        _listDatabase.AddRecipe(recipe);
    }

    public void Update(int id, Recipe recipe)
    {
        _listDatabase.UpdateRecipe(id, recipe);
    }

    public void Delete(int id)
    {
        _listDatabase.DeleteRecipe(id);
    }
}
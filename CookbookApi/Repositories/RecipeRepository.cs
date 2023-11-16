using CookbookApi.Data;
using CookbookApi.Interfaces;
using CookbookApi.Models;
using CookbookApi.Utility;
using Microsoft.EntityFrameworkCore;

namespace CookbookApi.Repositories;

public class RecipeRepository : IRepository<Recipe>
{
    private readonly CookbookDbContext _dbContext;
    private const string NoRecipesErrorMessage = "No Recipes found in the database.";
    
    public RecipeRepository(CookbookDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<IList<Recipe>> GetAllAsync()
    {
        if (!_dbContext.Recipes.Any())
        {
            throw new HttpException(StatusCodes.Status404NotFound, NoRecipesErrorMessage);
        }
        
        return await _dbContext.Recipes.Include(r => r.Ingredients).ToListAsync();
    }

    public async Task<Recipe> GetByIdAsync(int id)
    {
        if (!_dbContext.Recipes.Any())
        {
            throw new HttpException(StatusCodes.Status404NotFound, NoRecipesErrorMessage);
        }
        
        var recipe = await _dbContext.Recipes.Include(r => r.Ingredients).AsNoTracking().FirstOrDefaultAsync(r => r.Id == id);
        
        if (recipe is null)
            throw new HttpException(StatusCodes.Status404NotFound, $"Item with Id {id} not found.");
        
        return recipe;
    }

    public async Task AddAsync(Recipe recipe)
    {
        await _dbContext.AddAsync(recipe);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(int id, Recipe updatedRecipe)
    {
        var existingRecipe = await GetByIdAsync(id);

        if (existingRecipe.Id != updatedRecipe.Id)
        {
            throw new HttpException(StatusCodes.Status400BadRequest, $"Id does not match with the recipe supplied.");
        }

        _dbContext.Entry(existingRecipe).CurrentValues.SetValues(updatedRecipe);
        
        foreach (var ingredient in existingRecipe.Ingredients.ToList())
        {
            var updatedIngredient = updatedRecipe.Ingredients.FirstOrDefault(i => i.Id == ingredient.Id);
            if(updatedIngredient!=null)
                _dbContext.Entry(ingredient).CurrentValues.SetValues(updatedIngredient);
            else
                _dbContext.Ingredients.Remove(ingredient);
        }

        foreach (var ingredient in updatedRecipe.Ingredients.ToList().Where(i => i.Id == 0))
        {
            _dbContext.Ingredients.Add(ingredient);
        }
        
        _dbContext.Entry(updatedRecipe).State = EntityState.Modified;

        try
        {
            await _dbContext.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!RecipeExists(id))
            {
                throw new HttpException(StatusCodes.Status404NotFound, $"Item with Id {id} not found.");
            }

            throw;
        }
    }

    public async Task DeleteAsync(int id)
    {
        var recipe = await GetByIdAsync(id);

        _dbContext.Remove(recipe);
        await _dbContext.SaveChangesAsync();
    }

    private bool RecipeExists(int id) => _dbContext.Recipes.Any(e => e.Id == id);
   
}
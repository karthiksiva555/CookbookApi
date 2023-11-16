using CookbookApi.Data;
using CookbookApi.Interfaces;
using CookbookApi.Models;
using CookbookApi.Utility;
using Microsoft.EntityFrameworkCore;

namespace CookbookApi.Repositories;

public class RecipeRepository : IRepository<Recipe>
{
    private readonly CookbookDbContext _dbContext;

    public RecipeRepository(CookbookDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<IList<Recipe>> GetAllAsync()
    {
        return await _dbContext.Recipes.ToListAsync();
    }

    public async Task<Recipe> GetByIdAsync(int id)
    {
        if (!_dbContext.Recipes.Any())
        {
            throw new HttpException(StatusCodes.Status404NotFound, $"Recipe list is empty.");
        }
        
        var recipe = await _dbContext.Recipes.FindAsync(id);
        
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
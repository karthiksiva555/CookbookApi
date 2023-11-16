using AutoMapper;
using CookbookApi.Dtos;
using CookbookApi.Interfaces;
using CookbookApi.Models;

namespace CookbookApi.Services;

public class RecipeService : IRecipeService
{
    private readonly IRepository<Recipe> _recipeRepository;
    private readonly IMapper _mapper;

    public RecipeService(IRepository<Recipe> recipeRepository, IMapper mapper)
    {
        _recipeRepository = recipeRepository;
        _mapper = mapper;
    }
    
    public async Task<IList<RecipeDto>> GetRecipesAsync()
    {
        var recipes = await _recipeRepository.GetAllAsync();
        var recipesDto = _mapper.Map<IList<RecipeDto>>(recipes);

        return recipesDto;
    }

    public async Task<RecipeDto> GetRecipeByIdAsync(int id)
    {
        var recipe = await _recipeRepository.GetByIdAsync(id);
        var recipeDto = _mapper.Map<RecipeDto>(recipe);

        return recipeDto;
    }

    public async Task AddRecipeAsync(RecipeDto recipeDto)
    {
        var recipe = _mapper.Map<Recipe>(recipeDto);
        await _recipeRepository.AddAsync(recipe);
    }

    public async Task UpdateRecipeAsync(int id, RecipeDto recipeDto)
    {
        var recipe = _mapper.Map<Recipe>(recipeDto);
        await _recipeRepository.UpdateAsync(id, recipe);
    }

    public async Task DeleteRecipeAsync(int id)
    {
        await _recipeRepository.DeleteAsync(id);
    }
}
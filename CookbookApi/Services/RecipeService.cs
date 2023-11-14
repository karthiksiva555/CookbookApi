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

    public IList<RecipeDto> GetRecipes()
    {
        var recipes = _recipeRepository.GetAll(); 
        return _mapper.Map<IList<RecipeDto>>(recipes);;
    }

    public RecipeDto GetRecipeById(int id)
    {
        var recipe = _recipeRepository.GetById(id); 
        return _mapper.Map<RecipeDto>(recipe);
    }

    public void AddRecipe(RecipeDto recipeDto)
    {
        var recipe = _mapper.Map<Recipe>(recipeDto);
        _recipeRepository.Add(recipe);
    }

    public void UpdateRecipe(int id, RecipeDto updatedRecipeDto)
    {
        var updatedRecipe = _mapper.Map<Recipe>(updatedRecipeDto);
        _recipeRepository.Update(id, updatedRecipe);
    }

    public void DeleteRecipe(int id)
    {
        _recipeRepository.Delete(id);
    }
}
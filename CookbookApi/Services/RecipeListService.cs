using AutoMapper;
using CookbookApi.Dtos;
using CookbookApi.Interfaces;
using CookbookApi.Models;

namespace CookbookApi.Services;

public class RecipeListService : IRecipeListService
{
    private readonly IRepositoryList<Recipe> _recipeListRepository;
    private readonly IMapper _mapper;

    public RecipeListService(IRepositoryList<Recipe> recipeListRepository, IMapper mapper)
    {
        _recipeListRepository = recipeListRepository;
        _mapper = mapper;
    }

    public IList<RecipeDto> GetRecipes()
    {
        var recipes = _recipeListRepository.GetAll(); 
        return _mapper.Map<IList<RecipeDto>>(recipes);;
    }

    public RecipeDto GetRecipeById(int id)
    {
        var recipe = _recipeListRepository.GetById(id); 
        return _mapper.Map<RecipeDto>(recipe);
    }

    public void AddRecipe(RecipeDto recipeDto)
    {
        var recipe = _mapper.Map<Recipe>(recipeDto);
        _recipeListRepository.Add(recipe);
    }

    public void UpdateRecipe(int id, RecipeDto updatedRecipeDto)
    {
        var updatedRecipe = _mapper.Map<Recipe>(updatedRecipeDto);
        _recipeListRepository.Update(id, updatedRecipe);
    }

    public void DeleteRecipe(int id)
    {
        _recipeListRepository.Delete(id);
    }
}
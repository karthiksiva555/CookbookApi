using System.Net.Mime;
using AutoMapper;
using CookbookApi.Dtos;
using CookbookApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace CookbookApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Produces(MediaTypeNames.Application.Json)] // Produces responses only in JSON format
[Consumes(MediaTypeNames.Application.Json)] // Accepts requests only in JSON format
public class RecipesController : ControllerBase
{
    private readonly IMapper _mapper;

    private readonly IList<Recipe> _recipes = new[]
    {
        new Recipe(1, "Tomato Dhal", new List<Ingredient>
        {
            new(1, "Tomato", "Kilo Gram"),
            new (2, "Lentils", "Cup")
        }),
        new Recipe(2, "Chicken Kurma", new List<Ingredient>
        {
            new(1, "Chicken", "Kilo Gram"),
            new (2, "Yogurt", "Cup")
        })
    };

    public RecipesController(IMapper mapper)
    {
        _mapper = mapper;
    }
        
    /// <summary>
    /// Retrieves all the recipes available in the Cookbook
    /// </summary>
    /// <returns>List of recipes</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<RecipeDto>),StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<IEnumerable<RecipeDto>> GetAllRecipes()
    {
        if (_recipes.Count == 0)
            return NotFound();

        var recipesDto = _mapper.Map<IEnumerable<RecipeDto>>(_recipes);
        return Ok(recipesDto);
    }

    /// <summary>
    /// Retrieves a specific recipe by taking recipe id as input
    /// </summary>
    /// <param name="id">Id of the recipe to search</param>
    /// <returns>Matched recipe</returns>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(RecipeDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<RecipeDto> GetRecipeById([FromRoute] int id)
    {
        var recipe = _recipes.FirstOrDefault(recipe => recipe.Id == id);
        if (recipe is null)
            return NotFound();

        var recipeDto = _mapper.Map<RecipeDto>(recipe);
        return Ok(recipeDto);
    }
}
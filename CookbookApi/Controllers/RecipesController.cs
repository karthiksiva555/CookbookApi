using System.Net.Mime;
using AutoMapper;
using CookbookApi.Data;
using CookbookApi.Dtos;
using CookbookApi.Models;
using CookbookApi.Utility;
using Microsoft.AspNetCore.Mvc;

namespace CookbookApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Produces(MediaTypeNames.Application.Json)] // Produces responses only in JSON format
[Consumes(MediaTypeNames.Application.Json)] // Accepts requests only in JSON format
public class RecipesController : ControllerBase
{
    private readonly IMapper _mapper;

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
        var recipes = RecipeListDatabase.GetRecipes();
        var recipesDto = _mapper.Map<IEnumerable<RecipeDto>>(recipes);
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
        try
        {
            var recipe = RecipeListDatabase.GetRecipeById(id);
            var recipeDto = _mapper.Map<RecipeDto>(recipe);
            return Ok(recipeDto);
        }
        catch (HttpException exception)
        {
            return StatusCode(exception.StatusCode, exception.Message);
        }
    }

    /// <summary>
    /// Creates a new recipe by taking RecipeDto as input from request body
    /// </summary>
    /// <param name="recipeDto">Details of the recipe to be created</param>
    /// <returns>New recipe created</returns>
    [HttpPost]
    [ProducesResponseType(typeof(RecipeDto),StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult CreateRecipe([FromBody] RecipeDto recipeDto)
    {
        var recipe = _mapper.Map<Recipe>(recipeDto);
        RecipeListDatabase.AddRecipe(recipe);

        return CreatedAtAction(nameof(GetRecipeById), new { id = recipe.Id }, recipeDto);
    }

    /// <summary>
    /// Updates a recipe that matches id with a new recipe object passed as parameter. 
    /// </summary>
    /// <param name="id">Id of the recipe to be updated</param>
    /// <param name="recipeDto">New recipe object that will replace existing recipe</param>
    /// <returns>NoContent (204) after successful update</returns>
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult UpdateRecipe([FromRoute] int id, [FromBody] RecipeDto recipeDto)
    {
        var updatedRecipe = _mapper.Map<Recipe>(recipeDto);
        try
        {
            RecipeListDatabase.UpdateRecipe(id, updatedRecipe);
        }
        catch (HttpException exception)
        {
            return StatusCode(exception.StatusCode, exception.Message);
        }
        
        return NoContent();
    }
}
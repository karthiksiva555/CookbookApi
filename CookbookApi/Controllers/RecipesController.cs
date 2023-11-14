using System.Net.Mime;
using CookbookApi.Dtos;
using CookbookApi.Interfaces;
using CookbookApi.Utility;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace CookbookApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Produces(MediaTypeNames.Application.Json)] // Produces responses only in JSON format
[Consumes(MediaTypeNames.Application.Json)] // Accepts requests only in JSON format
public class RecipesController : ControllerBase
{
    private readonly IRecipeService _recipeService;

    public RecipesController(IRecipeService recipeService)
    {
        _recipeService = recipeService;
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
        var recipes = _recipeService.GetRecipes();
        return Ok(recipes);
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
            var recipe = _recipeService.GetRecipeById(id);
            return Ok(recipe);
        }
        catch (HttpException exception)
        {
            return StatusCode(exception.StatusCode, exception.Message);
        }
    }

    /// <summary>
    /// Creates a new recipe by taking RecipeDto as input from request body
    /// </summary>
    /// <param name="recipe">Details of the recipe to be created</param>
    /// <returns>New recipe created</returns>
    [HttpPost]
    [ProducesResponseType(typeof(RecipeDto),StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult CreateRecipe([FromBody] RecipeDto recipe)
    {
        _recipeService.AddRecipe(recipe);

        return CreatedAtAction(nameof(GetRecipeById), new { id = recipe.Id }, recipe);
    }

    /// <summary>
    /// Updates a recipe that matches id with a new recipe object passed as parameter. 
    /// </summary>
    /// <param name="id">Id of the recipe to be updated</param>
    /// <param name="updatedRecipe">New recipe object that will replace existing recipe</param>
    /// <returns>NoContent (204) after successful update</returns>
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult UpdateRecipe([FromRoute] int id, [FromBody] RecipeDto updatedRecipe)
    {
        try
        {
            _recipeService.UpdateRecipe(id, updatedRecipe);
        }
        catch (HttpException exception)
        {
            return StatusCode(exception.StatusCode, exception.Message);
        }
        
        return NoContent();
    }

    /// <summary>
    /// Patches the recipe with matching id parameter. Instruction on properties to patch are given in recipeUpdates property
    /// The patching process follows JSON Patch specification 
    /// </summary>
    /// <param name="id">Id of the recipe to patch</param>
    /// <param name="recipeUpdates">Properties of recipe to be patched</param>
    /// <returns>NoContent 204 on success</returns>
    [HttpPatch("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult PatchRecipe(int id, [FromBody]JsonPatchDocument<RecipeDto> recipeUpdates)
    {
        try
        {
            var recipe = _recipeService.GetRecipeById(id);
            recipeUpdates.ApplyTo(recipe);

            _recipeService.UpdateRecipe(id, recipe);
            
            return NoContent();
        }
        catch (HttpException exception)
        {
            return StatusCode(exception.StatusCode, exception.Message);
        }
    }

    /// <summary>
    /// Deletes recipe with matching id from the database 
    /// </summary>
    /// <param name="id">Id of the recipe to be deleted</param>
    /// <returns>NoContent(204) on success</returns>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult DeleteRecipe(int id)
    {
        try
        {
            _recipeService.DeleteRecipe(id);
        }
        catch (HttpException exception)
        {
            return StatusCode(exception.StatusCode, exception.Message);
        }
        
        return NoContent();
    }
}
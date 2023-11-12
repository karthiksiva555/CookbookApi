using CookbookApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace CookbookApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipesController : ControllerBase
    {
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
        
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Recipe>),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<Recipe>> GetAllRecipes()
        {
            if (_recipes.Count == 0)
            {
                return NotFound();
            }
            
            return Ok(_recipes);
        }
    }
}

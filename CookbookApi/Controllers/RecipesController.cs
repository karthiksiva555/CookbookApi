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
        public IEnumerable<Recipe> GetAllRecipes()
        {
            return _recipes;
        }
    }
}

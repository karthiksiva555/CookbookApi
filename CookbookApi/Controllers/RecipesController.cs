using Microsoft.AspNetCore.Mvc;

namespace CookbookApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipesController : ControllerBase
    {
        private readonly string[] _recipes = new[] { "Tomato Dhal", "Chicken Kurma", "Shrimp Teriyaki" };
        
        [HttpGet]
        public string[] GetAllRecipes()
        {
            return _recipes;
        }
    }
}

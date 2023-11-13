using System.ComponentModel.DataAnnotations;

namespace CookbookApi.Dtos;

public record RecipeDto
{
    [Required]
    public int Id { get; set; }

    [Required]
    [MaxLength(30, ErrorMessage = "Recipe name cannot exceed thirty characters.")]
    public required string Name { get; set; }

    [MinLength(1, ErrorMessage = "Recipe must have at least one ingredient.")]
    public required IEnumerable<IngredientDto> Ingredients { get; set; }
}
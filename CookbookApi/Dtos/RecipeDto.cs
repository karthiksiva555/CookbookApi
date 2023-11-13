using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CookbookApi.Dtos;

public record RecipeDto
{
    [Required]
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [Required]
    [MaxLength(30, ErrorMessage = "Recipe name cannot exceed thirty characters.")]
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [MinLength(1, ErrorMessage = "Recipe must have at least one ingredient.")]
    [JsonPropertyName("ingredients")]
    public required IEnumerable<IngredientDto> Ingredients { get; set; }
}
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CookbookApi.Dtos;

public record IngredientDto
{
    [Required]
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [Required(ErrorMessage = "Ingredient name is a required field.")]
    [MaxLength(20, ErrorMessage = "Ingredient name cannot exceed twenty characters.")]
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("unit")]
    public string? Unit { get; set; }
}
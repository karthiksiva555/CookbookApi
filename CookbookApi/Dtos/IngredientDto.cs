using System.ComponentModel.DataAnnotations;

namespace CookbookApi.Dtos;

public record IngredientDto
{
    [Required]
    public int Id { get; set; }

    [Required(ErrorMessage = "Ingredient name is a required field.")]
    [MaxLength(20, ErrorMessage = "Ingredient name cannot exceed twenty characters.")]
    public required string Name { get; set; }

    public string? Unit { get; set; }
}
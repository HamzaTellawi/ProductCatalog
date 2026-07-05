using System.ComponentModel.DataAnnotations;

namespace ProductCatalog.ViewModels.Categories;

public class CreateCategoryViewModel
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(250)]
    public string? Description { get; set; }
}
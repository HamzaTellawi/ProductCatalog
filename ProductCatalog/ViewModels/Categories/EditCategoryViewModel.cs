using System.ComponentModel.DataAnnotations;

namespace ProductCatalog.ViewModels.Categories;

public class EditCategoryViewModel
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(250)]
    public string? Description { get; set; }
}
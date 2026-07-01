using System.ComponentModel.DataAnnotations;

namespace ProductCatalog.ViewModels.Products;

public class CreateProductViewModel
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [StringLength(500)]
    public string? Description { get; set; }

    [Range(0.01, 100000)]
    public decimal Price { get; set; }

    [Range(0, 10000)]
    public int Stock { get; set; }
}
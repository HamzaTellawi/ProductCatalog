using ProductCatalog.Models;
using ProductCatalog.ViewModels.Categories;

namespace ProductCatalog.Mappings;

public static class CategoryMappings
{
    public static CategoryViewModel ToViewModel(this Category category)
    {
        return new CategoryViewModel
        {
            Id = category.Id,
            Name = category.Name,
            ProductsCount = category.Products.Count,
            Description = category.Description,
        };
    }

    public static CategoryDetailsViewModel ToDetailsViewModel(this Category category)
    {
        return new CategoryDetailsViewModel
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description,
            ProductsCount = category.Products.Count
        };
    }

    public static EditCategoryViewModel ToEditViewModel(this Category category)
    {
        return new EditCategoryViewModel
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description
        };
    }

    public static Category ToEntity(this CreateCategoryViewModel model)
    {
        return new Category
        {
            Name = model.Name,
            Description = model.Description
        };
    }

    public static void UpdateEntity(this EditCategoryViewModel model, Category category)
    {
        category.Name = model.Name;
        category.Description = model.Description;
    }
}
using ProductCatalog.Models;
using ProductCatalog.ViewModels.Products;

namespace ProductCatalog.Mappings;

public static class ProductMappings
{
    public static ProductViewModel ToViewModel(this Product product)
    {
        return new ProductViewModel
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
            Stock = product.Stock,
            CategoryName = product.Category?.Name
        };
    }

    public static EditProductViewModel ToEditViewModel(this Product product)
    {
        return new EditProductViewModel
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            Stock = product.Stock,
            CategoryId = product.CategoryId
        };
    }

    public static Product ToEntity(this CreateProductViewModel model)
    {
        return new Product
        {
            Name = model.Name,
            Description = model.Description,
            Price = model.Price,
            Stock = model.Stock,
            CategoryId = model.CategoryId
        };
    }

    public static void UpdateEntity(this EditProductViewModel model, Product product)
    {
        product.Name = model.Name;
        product.Description = model.Description;
        product.Price = model.Price;
        product.Stock = model.Stock;
        product.CategoryId = model.CategoryId;
    }

    public static ProductDetailsViewModel ToDetailsViewModel(this Product product)
    {
        return new ProductDetailsViewModel
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            Stock = product.Stock,
            CategoryName = product.Category?.Name
        };
    }
}
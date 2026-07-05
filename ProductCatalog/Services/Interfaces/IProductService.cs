using ProductCatalog.Models;
using ProductCatalog.ViewModels.Products;

namespace ProductCatalog.Services.Interfaces;

public interface IProductService
{
    Task<List<ProductViewModel>> GetAllAsync();

    Task AddAsync(CreateProductViewModel model);
    Task<EditProductViewModel?> GetForEditAsync(int id);

    Task UpdateAsync(EditProductViewModel model);

    Task<ProductDetailsViewModel?> GetDetailsAsync(int id);
    Task<CreateProductViewModel> GetCreateModelAsync();
    Task DeleteAsync(int id);
}
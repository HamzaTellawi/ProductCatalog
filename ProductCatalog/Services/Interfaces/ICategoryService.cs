using ProductCatalog.ViewModels.Categories;

namespace ProductCatalog.Services.Interfaces;

public interface ICategoryService
{
    Task<List<CategoryViewModel>> GetAllAsync();

    Task AddAsync(CreateCategoryViewModel model);

    Task<EditCategoryViewModel?> GetForEditAsync(int id);

    Task UpdateAsync(EditCategoryViewModel model);

    Task<CategoryDetailsViewModel?> GetDetailsAsync(int id);

    Task<bool> DeleteAsync(int id);
}
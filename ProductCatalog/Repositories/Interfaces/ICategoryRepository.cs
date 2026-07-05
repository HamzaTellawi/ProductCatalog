using ProductCatalog.Models;

namespace ProductCatalog.Repositories.Interfaces;

public interface ICategoryRepository
{
    Task<List<Category>> GetAllAsync();

    Task<Category?> GetByIdAsync(int id);

    Task<Category?> GetTrackedByIdAsync(int id);

    Task AddAsync(Category category);

    Task DeleteAsync(int id);
    Task<bool> HasProductsAsync(int categoryId);
    Task SaveChangesAsync();
}
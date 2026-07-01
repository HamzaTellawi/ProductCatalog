using ProductCatalog.Models;

namespace ProductCatalog.Repositories.Interfaces;

public interface IProductRepository
{
    Task<List<Product>> GetAllAsync();

    Task<Product?> GetByIdAsync(int id);
    Task<Product?> GetTrackedByIdAsync(int id);
    Task AddAsync(Product product);

    Task UpdateAsync(Product product);

    Task DeleteAsync(int id);

    Task SaveChangesAsync();
}
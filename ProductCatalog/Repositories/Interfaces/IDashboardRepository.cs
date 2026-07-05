using ProductCatalog.Models;

namespace ProductCatalog.Repositories.Interfaces
{
    public interface IDashboardRepository
    {
        Task<int> GetProductsCountAsync();

        Task<int> GetCategoriesCountAsync();

        Task<int> GetTotalStockAsync();

        Task<List<Category>> GetCategoriesWithProductsAsync();

        Task<List<Product>> GetTopProductsAsync(int count = 5);
    }
}

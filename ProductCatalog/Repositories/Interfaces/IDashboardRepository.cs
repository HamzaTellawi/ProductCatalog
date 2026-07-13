using ProductCatalog.Models.Dtos;

namespace ProductCatalog.Repositories.Interfaces
{
    public interface IDashboardRepository
    {
        Task<DashboardDataDto> GetDashboardDataAsync(int topProductsCount = 5);
    }
}
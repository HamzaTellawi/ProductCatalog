using ProductCatalog.Repositories.Interfaces;
using ProductCatalog.Services.Interfaces;
using ProductCatalog.ViewModels.Dashboard;

namespace ProductCatalog.Services.Implementations
{
    public class DashboardService : IDashboardService
    {
        private readonly IDashboardRepository _dashboardRepository;

        public DashboardService(IDashboardRepository dashboardRepository)
        {
            _dashboardRepository = dashboardRepository;
        }

        public async Task<DashboardViewModel> GetDashboardAsync()
        {
            var totalProducts = await _dashboardRepository.GetProductsCountAsync();
            var totalCategories = await _dashboardRepository.GetCategoriesCountAsync();
            var totalStock = await _dashboardRepository.GetTotalStockAsync();
            var categories = await _dashboardRepository.GetCategoriesWithProductsAsync();
            var topProducts = await _dashboardRepository.GetTopProductsAsync();

            return new DashboardViewModel
            {
                TotalProducts = totalProducts,
                TotalCategories = totalCategories,
                TotalStock = totalStock,
                AverageStock = totalProducts == 0
                    ? 0
                    : Math.Round((double)totalStock / totalProducts, 1),

                ProductsPerCategory = categories
                    .Select(c => new CategoryChartViewModel
                    {
                        CategoryName = c.Name,
                        ProductCount = c.Products.Count
                    })
                    .ToList(),

                TopProducts = topProducts
                    .Select(p => new TopProductViewModel
                    {
                        ProductName = p.Name,
                        CategoryName = p.Category?.Name ?? "-",
                        Stock = p.Stock
                    })
                    .ToList()
            };
        }
    }
}

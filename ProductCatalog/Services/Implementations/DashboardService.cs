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
            var data = await _dashboardRepository.GetDashboardDataAsync();

            return new DashboardViewModel
            {
                TotalProducts = data.Summary.TotalProducts,
                TotalCategories = data.Summary.TotalCategories,
                TotalStock = data.Summary.TotalStock,
                AverageStock = data.Summary.TotalProducts == 0
                    ? 0
                    : Math.Round((double)data.Summary.TotalStock / data.Summary.TotalProducts, 1),

                ProductsPerCategory = data.ProductsPerCategory
                    .Select(c => new CategoryChartViewModel
                    {
                        CategoryName = c.CategoryName,
                        ProductCount = c.ProductCount
                    })
                    .ToList(),

                TopProducts = data.TopProducts
                    .Select(p => new TopProductViewModel
                    {
                        ProductName = p.ProductName,
                        CategoryName = p.CategoryName,
                        Stock = p.Stock
                    })
                    .ToList()
            };
        }
    }
}
using ProductCatalog.ViewModels.Dashboard;
namespace ProductCatalog.Services.Interfaces
{
    public interface IDashboardService
    {
        Task<DashboardViewModel> GetDashboardAsync();
    }
}

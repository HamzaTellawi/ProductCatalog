namespace ProductCatalog.ViewModels.Dashboard;

public class DashboardViewModel
{
    public int TotalProducts { get; set; }

    public int TotalCategories { get; set; }

    public int TotalStock { get; set; }

    public double AverageStock { get; set; }

    public List<CategoryChartViewModel> ProductsPerCategory { get; set; } = [];

    public List<TopProductViewModel> TopProducts { get; set; } = [];
}
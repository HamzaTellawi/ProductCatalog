namespace ProductCatalog.Models.Dtos
{
    public class DashboardDataDto
    {
        public DashboardSummaryDto Summary { get; set; } = new();

        public List<CategoryProductCountDto> ProductsPerCategory { get; set; } = [];

        public List<TopProductDto> TopProducts { get; set; } = [];
    }
}
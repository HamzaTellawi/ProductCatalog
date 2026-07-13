namespace ProductCatalog.Models.Dtos
{
    public class CategoryProductCountDto
    {
        public string CategoryName { get; set; } = string.Empty;

        public int ProductCount { get; set; }
    }
}
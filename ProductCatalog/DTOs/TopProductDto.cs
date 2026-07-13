namespace ProductCatalog.Models.Dtos
{
    public class TopProductDto
    {
        public string ProductName { get; set; } = string.Empty;

        public string CategoryName { get; set; } = string.Empty;

        public int Stock { get; set; }
    }
}
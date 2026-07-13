using Microsoft.EntityFrameworkCore;
using ProductCatalog.Data;
using ProductCatalog.Models.Dtos;
using ProductCatalog.Repositories.Interfaces;
using System.Data;

namespace ProductCatalog.Repositories.Implementations;

public class DashboardRepository : IDashboardRepository
{
    private readonly ApplicationDbContext _context;

    public DashboardRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<DashboardDataDto> GetDashboardDataAsync(int topProductsCount = 5)
    {
        var result = new DashboardDataDto();

        var connection = _context.Database.GetDbConnection();
        var shouldCloseConnection = connection.State != ConnectionState.Open;

        if (shouldCloseConnection)
        {
            await connection.OpenAsync();
        }

        try
        {
            using var command = connection.CreateCommand();
            command.CommandText = "dbo.sp_GetDashboardData";
            command.CommandType = CommandType.StoredProcedure;

            var topCountParam = command.CreateParameter();
            topCountParam.ParameterName = "@TopProductsCount";
            topCountParam.Value = topProductsCount;
            command.Parameters.Add(topCountParam);

            using var reader = await command.ExecuteReaderAsync();

            // summary stats (single row)
            if (await reader.ReadAsync())
            {
                result.Summary = new DashboardSummaryDto
                {
                    TotalProducts = reader.GetInt32(reader.GetOrdinal("TotalProducts")),
                    TotalCategories = reader.GetInt32(reader.GetOrdinal("TotalCategories")),
                    TotalStock = reader.GetInt32(reader.GetOrdinal("TotalStock"))
                };
            }

            // product count per category
            if (await reader.NextResultAsync())
            {
                while (await reader.ReadAsync())
                {
                    result.ProductsPerCategory.Add(new CategoryProductCountDto
                    {
                        CategoryName = reader.GetString(reader.GetOrdinal("CategoryName")),
                        ProductCount = reader.GetInt32(reader.GetOrdinal("ProductCount"))
                    });
                }
            }

            // top products by stock
            if (await reader.NextResultAsync())
            {
                while (await reader.ReadAsync())
                {
                    var categoryOrdinal = reader.GetOrdinal("CategoryName");

                    result.TopProducts.Add(new TopProductDto
                    {
                        ProductName = reader.GetString(reader.GetOrdinal("ProductName")),
                        CategoryName = reader.IsDBNull(categoryOrdinal) ? "-" : reader.GetString(categoryOrdinal),
                        Stock = reader.GetInt32(reader.GetOrdinal("Stock"))
                    });
                }
            }
        }
        finally
        {
            if (shouldCloseConnection)
            {
                await connection.CloseAsync();
            }
        }

        return result;
    }
}
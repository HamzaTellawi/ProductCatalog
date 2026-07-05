using Microsoft.EntityFrameworkCore;
using ProductCatalog.Data;
using ProductCatalog.Models;
using ProductCatalog.Repositories.Interfaces;

namespace ProductCatalog.Repositories.Implementations;

public class DashboardRepository : IDashboardRepository
{
    private readonly ApplicationDbContext _context;

    public DashboardRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<int> GetProductsCountAsync()
    {
        return await _context.Products.CountAsync();
    }
    public async Task<int> GetCategoriesCountAsync()
    {
        return await _context.Categories.CountAsync();
    }
    public async Task<int> GetTotalStockAsync()
    {
        return await _context.Products.SumAsync(x => x.Stock);
    }
    public async Task<List<Category>> GetCategoriesWithProductsAsync()
    {
        return await _context.Categories
            .Include(x => x.Products)
            .AsNoTracking()
            .ToListAsync();
    }
    public async Task<List<Product>> GetTopProductsAsync(int count = 5)
    {
        return await _context.Products
            .Include(x => x.Category)
            .AsNoTracking()
            .OrderByDescending(x => x.Stock)
            .Take(count)
            .ToListAsync();
    }
}
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Data;
using ProductCatalog.Models;
using ProductCatalog.Repositories.Interfaces;

namespace ProductCatalog.Repositories.Implementations;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _context;

    public ProductRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Product>> GetAllAsync()
    {
        return await _context.Products.AsNoTracking().ToListAsync();
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        return await _context.Products
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);
    }
    public async Task<Product?> GetTrackedByIdAsync(int id)
    {
        return await _context.Products
            .FirstOrDefaultAsync(x => x.Id == id);
    }
    public async Task AddAsync(Product product)
    {
        await _context.Products.AddAsync(product);
    }

    //public Task UpdateAsync(Product product)
    //{
    //    _context.Products.Update(product);
    //    return Task.CompletedTask;
    //}

    public async Task DeleteAsync(int id)
    {
        var product = await _context.Products.FindAsync(id);

        if (product is not null)
        {
            _context.Products.Remove(product);
        }
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
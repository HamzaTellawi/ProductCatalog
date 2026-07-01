using ProductCatalog.Models;
using ProductCatalog.Repositories.Interfaces;
using ProductCatalog.Services.Interfaces;
using ProductCatalog.ViewModels.Products;
using ProductCatalog.Mappings;
using ProductCatalog.Caching.Interfaces;
using ProductCatalog.Constants;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace ProductCatalog.Services.Implementations;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;
    private readonly ICacheService _cacheService;
    private readonly ILogger<ProductService> _logger;

    public ProductService(IProductRepository repository, ICacheService cacheService, ILogger<ProductService> logger)
    {
        _repository = repository;
        _cacheService = cacheService;
        _logger = logger;
    }

    public async Task<List<ProductViewModel>> GetAllAsync()
    {
        var stopwatch = Stopwatch.StartNew();

        var cachedProducts =
            await _cacheService.GetAsync<List<ProductViewModel>>(CacheKeys.Products);

        if (cachedProducts is not null)
        {
            stopwatch.Stop();

            _logger.LogInformation(
                "Products loaded from Redis in {ElapsedMilliseconds} ms.",
                stopwatch.ElapsedMilliseconds);

            return cachedProducts;
        }

        var products = await _repository.GetAllAsync();

        var result = products
            .Select(x => x.ToViewModel())
            .ToList();

        await _cacheService.SetAsync(
            CacheKeys.Products,
            result,
            TimeSpan.FromMinutes(5));

        stopwatch.Stop();

        _logger.LogInformation(
            "Products loaded from SQL Server in {ElapsedMilliseconds} ms.",
            stopwatch.ElapsedMilliseconds);

        return result;
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task AddAsync(CreateProductViewModel model)
    {
        var product = model.ToEntity();

        await _repository.AddAsync(product);
        await _repository.SaveChangesAsync();
        await _cacheService.RemoveAsync(CacheKeys.Products);
    }

    public async Task<EditProductViewModel?> GetForEditAsync(int id)
    {
        var product = await _repository.GetTrackedByIdAsync(id);

        return product?.ToEditViewModel();
    }

    
    public async Task UpdateAsync(EditProductViewModel model)
    {
        var product = await _repository.GetTrackedByIdAsync(model.Id);

        if (product is null)
            return;

        model.UpdateEntity(product);

        await _repository.SaveChangesAsync();
        await _cacheService.RemoveAsync(CacheKeys.Products);
    }

    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
        await _repository.SaveChangesAsync();
        await _cacheService.RemoveAsync(CacheKeys.Products);
    }
    public async Task<ProductDetailsViewModel?> GetDetailsAsync(int id)
    {
        var product = await _repository.GetByIdAsync(id);

        return product?.ToDetailsViewModel();
    }
}
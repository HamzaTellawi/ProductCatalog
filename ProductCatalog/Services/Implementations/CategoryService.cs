using Microsoft.Extensions.Logging;
using ProductCatalog.Caching.Interfaces;
using ProductCatalog.Constants;
using ProductCatalog.Mappings;
using ProductCatalog.Repositories.Interfaces;
using ProductCatalog.Services.Interfaces;
using ProductCatalog.ViewModels.Categories;
using System.Diagnostics;

namespace ProductCatalog.Services.Implementations;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _repository;
    private readonly ICacheService _cacheService;
    private readonly ILogger<CategoryService> _logger;

    public CategoryService(
        ICategoryRepository repository,
        ICacheService cacheService,
        ILogger<CategoryService> logger)
    {
        _repository = repository;
        _cacheService = cacheService;
        _logger = logger;
    }

    public async Task<List<CategoryViewModel>> GetAllAsync()
    {
        var stopwatch = Stopwatch.StartNew();

        var cached =
            await _cacheService.GetAsync<List<CategoryViewModel>>(CacheKeys.Categories);

        if (cached is not null)
        {
            stopwatch.Stop();

            _logger.LogInformation(
                "Categories loaded from Redis in {ElapsedMilliseconds} ms.",
                stopwatch.ElapsedMilliseconds);

            return cached;
        }

        var categories = await _repository.GetAllAsync();

        var result = categories
            .Select(x => x.ToViewModel())
            .ToList();

        await _cacheService.SetAsync(
            CacheKeys.Categories,
            result,
            TimeSpan.FromMinutes(5));

        stopwatch.Stop();

        _logger.LogInformation(
            "Categories loaded from SQL Server in {ElapsedMilliseconds} ms.",
            stopwatch.ElapsedMilliseconds);

        return result;
    }

    public async Task AddAsync(CreateCategoryViewModel model)
    {
        var category = model.ToEntity();

        await _repository.AddAsync(category);
        await _repository.SaveChangesAsync();

        await _cacheService.RemoveAsync(CacheKeys.Categories);
        await _cacheService.RemoveAsync(CacheKeys.Products);
    }

    public async Task<EditCategoryViewModel?> GetForEditAsync(int id)
    {
        var category = await _repository.GetTrackedByIdAsync(id);

        return category?.ToEditViewModel();
    }

    public async Task UpdateAsync(EditCategoryViewModel model)
    {
        var category = await _repository.GetTrackedByIdAsync(model.Id);

        if (category is null)
            return;

        model.UpdateEntity(category);

        await _repository.SaveChangesAsync();

        await _cacheService.RemoveAsync(CacheKeys.Categories);
        await _cacheService.RemoveAsync(CacheKeys.Products);
    }

    public async Task<CategoryDetailsViewModel?> GetDetailsAsync(int id)
    {
        var category = await _repository.GetByIdAsync(id);

        return category?.ToDetailsViewModel();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        if (await _repository.HasProductsAsync(id))
            return false;

        await _repository.DeleteAsync(id);
        await _repository.SaveChangesAsync();

        await _cacheService.RemoveAsync(CacheKeys.Categories);
        await _cacheService.RemoveAsync(CacheKeys.Products);
        return true;
    }
}
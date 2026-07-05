using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Services.Interfaces;
using ProductCatalog.ViewModels.Categories;

namespace ProductCatalog.Controllers;

public class CategoriesController : Controller
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    public async Task<IActionResult> Index()
    {
        var categories = await _categoryService.GetAllAsync();

        return View(categories);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateCategoryViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        await _categoryService.AddAsync(model);

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var category = await _categoryService.GetForEditAsync(id);

        if (category is null)
            return NotFound();

        return View(category);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(EditCategoryViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        await _categoryService.UpdateAsync(model);

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Details(int id)
    {
        var category = await _categoryService.GetDetailsAsync(id);

        if (category is null)
            return NotFound();

        return View(category);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _categoryService.DeleteAsync(id);

        if (!deleted)
        {
            TempData["Error"] =
                "Cannot delete this category because it contains products.";

            return RedirectToAction(nameof(Index));
        }

        TempData["Success"] =
            "Category deleted successfully.";

        return RedirectToAction(nameof(Index));
    }
}
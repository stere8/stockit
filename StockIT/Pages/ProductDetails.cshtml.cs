using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StockIT.BLL.Services;

namespace StockIT.Pages;

public class ProductDetailsModel : PageModel
{
    public ProductDetailsViewModel ProductModel { get; set; }
    private IProductService _productService { get; set; }
    private ICategoryService _categoryService { get; set; }
    private readonly IWebHostEnvironment _environment;

    public ProductDetailsModel(IProductService productService, ICategoryService categoryService, IWebHostEnvironment environment)
    {
        _productService = productService;
        _categoryService = categoryService;
        _environment = environment;
    }
    public async Task<IActionResult> OnGetAsync(int productId)
    {
        var viewProduct = await _productService.GetProductByIdAsync(productId);

        if (viewProduct == null)
        {
            TempData["Success"] = "false";
            TempData["Message"] = "Product not found.";  // More specific error message
            return RedirectToPage("OperationComplete"); // Redirect to a dedicated error page
        }

        var category = _categoryService.GetCategoryById(viewProduct.CategoryId);

        if (category == null)
        {
            TempData["Success"] = "false";
            TempData["Message"] = "Category not found.";  // More specific error message
            return RedirectToPage("OperationComplete"); // Redirect to a dedicated error page
        }

        string uploadsFolder = "uploads";

        ProductModel = new ProductDetailsViewModel()
        {
            Name = viewProduct.Name,
            Quantity = viewProduct.Quantity,
            Description = viewProduct.Description,
            CategoryId = viewProduct.CategoryId,
            Price = viewProduct.Price,
            category = category,
            ImagePath = Path.Combine(uploadsFolder, viewProduct.ImagePaths)
        };

        // Additional logic to populate Categories list (if needed)

        return Page(); // Render the page with populated data
    }
}
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StockIT.BLL.Services;
using StockIT.Models;
using System.IO;

namespace StockIT.Pages;

public class ProductEditModel : PageModel
{
    [BindProperty]
    public ProductEditViewModel ProductModel { get; set; }
    public List<Category> Categories { get; set; }
    private IProductService _productService { set; get; }
    private ICategoryService _categoryService { set; get; }
    private IWebHostEnvironment _environment { get; set; }


    public ProductEditModel(IProductService productService, ICategoryService categoryService, IWebHostEnvironment environment)
    {
        _productService = productService;
        _categoryService = categoryService;
        _environment = environment;
        ProductModel = new ProductEditViewModel { Images = new IFormFile[0] }; // Initialize Images as empty array
        ViewData["Title"] = "Product Edit";
    }


    public async Task<IActionResult> OnGetAsync(int productId)
    {
        // Asynchronously retrieve product data for potential database operations
        var viewProduct = await _productService.GetProductByIdAsync(productId);

        if (viewProduct == null)
        {
            TempData["Success"] = "false";
            TempData["Message"] = "Product not found.";  // More specific error message
            return RedirectToPage("OperationComplete"); // Redirect to a dedicated error page
        }

        string existingImagePaths = viewProduct.ImagePaths ?? string.Empty; // Use null-conditional operator for default empty string 

        

        var imageList = new List<string>();


        if (!string.IsNullOrEmpty(existingImagePaths))
        {
            string[] imagePaths = existingImagePaths.Split(',');
            ProductModel.Images = new IFormFile[imagePaths.Length];

            int index = 0;
            foreach (var imagePath in imagePaths)
            {
                string fullPath = Path.Combine("/uploads", imagePath); // Adjust path based on your storage location
                imageList.Add(fullPath);
                if (System.IO.File.Exists(fullPath))
                {
                    using (var fs = new FileStream(fullPath,FileMode.Open))
                    {
                        ProductModel.Images[index] = new FormFile(fs, 0,fs.Length,  imagePath, "image/jpeg"); // Assuming JPEG format, adjust mime type as needed
                    }
                }
                else
                {
                    // Handle cases where the file doesn't exist (log or display an error message)
                }
                index++;

            }
        }
        else
        {
            ProductModel.Images = new IFormFile[0];
        }

        ProductModel = new ProductEditViewModel()
        {
            CategoryId = viewProduct.CategoryId,
            Description = viewProduct.Description,
            Id = viewProduct.Id,
            ImagePaths = existingImagePaths,
            Images = new IFormFile[viewProduct.ImagePaths.Length],
            ImagesList = imageList,
            Quantity = viewProduct.Quantity
        };
        

        // Additional logic to populate Categories list (if needed)
        Categories = _categoryService.GetAllCategories(); // Assuming a GetCategoriesAsync method

        return Page(); // Render the page with populated data
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        string existingImagePaths = _productService.GetProductById(ProductModel.Id).ImagePaths; // Get existing paths

        // Handle image uploads (optional)
        if (ProductModel.Images != null && ProductModel.Images.Length > 0)
        {
            List<string> imagePaths = new List<string>();
            if (!string.IsNullOrEmpty(existingImagePaths))
            {
                imagePaths.AddRange(existingImagePaths.Split(',', StringSplitOptions.RemoveEmptyEntries));
            }

            foreach (var image in ProductModel.Images)
            {
                // Generate a unique filename
                string fileName = Path.GetRandomFileName() + Path.GetExtension(image.FileName);
                // Get the wwwroot path (adjust based on your storage location)
                string uploads = Path.Combine(_environment.WebRootPath, "uploads");
                // Create uploads directory if it doesn't exist
                if (!Directory.Exists(uploads))
                {
                    Directory.CreateDirectory(uploads);
                }

                // Save the uploaded image to the uploads folder
                string filePath = Path.Combine(uploads, fileName);
                await using (FileStream fs = new FileStream(filePath, FileMode.Create))
                {
                    await image.CopyToAsync(fs);
                }

                // Add the new image path
                imagePaths.Add(filePath);
            }

            ProductModel.ImagePaths = string.Join(",", imagePaths); // Update with combined paths
        }

        // Handle image deletion (optional)
        if (!string.IsNullOrEmpty(ProductModel.DeleteImage))
        {
            List<string> imagePaths = new List<string>();
            if (!string.IsNullOrEmpty(existingImagePaths))
            {
                imagePaths.AddRange(existingImagePaths.Split(',', StringSplitOptions.RemoveEmptyEntries));
            }

            string[] indexesToDelete = ProductModel.DeleteImage.Split(',');
            foreach (string indexStr in indexesToDelete)
            {
                int index;
                if (int.TryParse(indexStr.Trim(), out index) && index >= 0 && index < imagePaths.Count)
                {
                    // Remove the image path at the specified index
                    imagePaths.RemoveAt(index);

                    // Delete the image file from server (optional)
                    // Implement logic to delete the actual image file based on the path
                }
            }

            ProductModel.ImagePaths = string.Join(",", imagePaths); // Update with remaining paths
        }

        var product = new Product()
        {
            Id = ProductModel.Id,
            Name = ProductModel.Name,
            Price = ProductModel.Price,
            Quantity = ProductModel.Quantity,
            CategoryId = ProductModel.CategoryId,
            ImagePaths = ProductModel.ImagePaths,
            Description = ProductModel.Description,

        };

        // Update product information in your data store (including ImagePaths)
        await _productService.UpdateProductAsync(product);
        // other product properties..., Model.ImagePaths);

        return RedirectToPage("./ProductList");
    }
}
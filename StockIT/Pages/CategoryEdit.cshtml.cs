using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StockIT.BLL.Services;
using StockIT.Models;

namespace StockIT.Pages
{
    public class CategoryEditModel : PageModel
    {
        public Category Category { get; set; }
        private ICategoryService _categoryService { get; set; }
        public CategoryEditModel(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public void OnGet(int categoryId)
        {
            Category = _categoryService.GetCategoryById(categoryId);
        }

        public IActionResult OnPostEditCategory()
        {
            if (!ModelState.IsValid)
            {

            }

            try
            {
                _categoryService.UpdateCategory(Category);
            }
            catch (Exception ex)
            {
                TempData["Success"] = "false";
                TempData["Message"] = "Edit product failed";  // More specific error message
                return RedirectToPage("OperationComplete");
            }

            TempData["Success"] = "false";
            TempData["Message"] = "Edit product failed";  // More specific error message
            return RedirectToPage("OperationComplete");
        }

    }
}


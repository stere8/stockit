using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StockIT.BLL.Services;
using StockIT.Models;

namespace StockIT.Pages
{
    public class UserEditModel : PageModel
    {
        [BindProperty]
        public UserEditViewModel UserModel { get; set; }
        private IUserService _UserService { set; get; }
        private IWebHostEnvironment _environment { get; set; }
        public UserEditModel(IUserService userService, IWebHostEnvironment environment)
        {
            _UserService = userService;
            _environment = environment;
        }

        public async Task<IActionResult> OnGetAsync(int userId)
        {
            // Asynchronously retrieve product data for potential database operations
            var viewUser = await _UserService.GetUserProfileAsync(userId);

            if (viewUser == null)
            {
                TempData["Success"] = "false";
                TempData["Message"] = "Product not found.";  // More specific error message
                return RedirectToPage("OperationComplete"); // Redirect to a dedicated error page
            }

            UserModel = new UserEditViewModel()
            {
                Username = viewUser.Username,
                FirstName = viewUser.FirstName,
                LastName = viewUser.LastName,
                ID = viewUser.ID,
                ProfilePicture = viewUser.ProfilePicture
            };

            return Page(); // Render the page with populated data
        }
    }
}

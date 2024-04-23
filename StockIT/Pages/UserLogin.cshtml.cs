using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StockIT.BLL.Services;
using StockIT.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace StockIT.Pages
{
    public class UserLoginModel : PageModel
    {
        [BindProperty]
        public UserLoginViewModel UserModel { get; set; }
        private IUserService _userService;
        private IHttpContextAccessor _httpContextAccessor;
        public UserLoginModel(IUserService userService,IHttpContextAccessor context)
        {
            _userService = userService;
            _httpContextAccessor = context;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPostLogin()
        {
            if (!ModelState.IsValid)
            {
                var errors = new List<string>();
                foreach (var modelStateEntry in ModelState.Values)
                {
                    if (modelStateEntry.ValidationState == ModelValidationState.Invalid)
                    {
                        foreach (var error in modelStateEntry.Errors)
                        {
                            errors.Add(error.ErrorMessage);
                        }
                    }
                }

                TempData["Success"] = "false";
                TempData["Message"] = "Please correct validation errors.";
                errors.ForEach(error => ViewData["Message"] += $"\n {error}");
                return RedirectToPage("OperationComplete"); // Redirect to operationcomplete.cshtml
            }

            try
            {
                _userService.AuthenticateUser(UserModel.Username, UserModel.Password);
                TempData["Success"] = "true";
                TempData["Message"] = "Oepration succedd!";
                _httpContextAccessor.HttpContext.Session.SetString("inventory","true");
                return RedirectToPage("OperationComplete"); // Redirect to OperationComplete.cshtml
            }
            catch (Exception ex)
            {
                TempData["Success"] = "false";
                TempData["Message"] = $"There was an error while adding {ex}"; // Generic error message
                return RedirectToPage("OperationComplete"); // Redirect to OperationComplete.cshtml
            }
        }

    }
}

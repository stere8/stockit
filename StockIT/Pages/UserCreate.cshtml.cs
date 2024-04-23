using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StockIT.BLL.Services;
using StockIT.Models;
using System;

namespace StockIT.Pages
{
    public class UserCreateModel : PageModel
    {
        [BindProperty]
        public UserCreateViewModel UserModel { get; set; }
        private IUserService _userService;
        private IHttpContextAccessor _httpContextAccessor;
        private readonly IWebHostEnvironment _environment;

        public UserCreateModel(IUserService userService,IHttpContextAccessor context, IWebHostEnvironment environment)
        {
            _userService = userService;
            _httpContextAccessor = context;
            _environment = environment;

        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostSignup()
        {
            if (UserModel.Password != UserModel.ReapeatPassword)
            {
                ModelState.AddModelError("ConfirmPassword", "Password and Confirm Password do not match.");
            }
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
            if (UserModel.ProfilePicture != null)
            {
                string fileName = Path.GetRandomFileName() + Path.GetExtension(UserModel.ProfilePicture.FileName);
                string uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads");
                string filePath = Path.Combine(uploadsFolder, fileName);
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                await using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await UserModel.ProfilePicture.CopyToAsync(fileStream);
                }

                var user = new User
                {
                    FirstName = UserModel.FirstName,
                    LastName = UserModel.LastName,
                    Username = UserModel.Username,
                    ProfilePicture = fileName
                };

                try
                {
                    _userService.RegisterUser(user);
                    TempData["Success"] = "true";
                    TempData["Message"] = "Operation succeed!";
                    _httpContextAccessor.HttpContext.Session.SetString("inventory", "true");
                    return RedirectToPage("OperationComplete"); // Redirect to OperationComplete.cshtml
                }
                catch (Exception ex)
                {
                    TempData["Success"] = "false";
                    TempData["Message"] = $"There was an error while adding {ex}"; // Generic error message
                    return RedirectToPage("OperationComplete"); // Redirect to OperationComplete.cshtml
                }
            }

            return Page();
        }

    }
}

namespace StockIT.Models;

public class UserCreateViewModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string ReapeatPassword { get; set; }
    public IFormFile ProfilePicture { get; set; }


}
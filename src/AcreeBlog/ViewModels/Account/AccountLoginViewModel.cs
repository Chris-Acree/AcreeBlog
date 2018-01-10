using System.ComponentModel.DataAnnotations;

namespace AcreeBlog.ViewModels.Account
{
    public class AccountLoginViewModel
  {
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
    public bool RememberMe { get; set; }
  }
}

using System.ComponentModel.DataAnnotations;

namespace AcreeBlog.ViewModels.Account
{
    public class ExternalLoginConfirmationViewModel
    {
    [Required]
    [EmailAddress]
    public string Email { get; set; }
  }
}

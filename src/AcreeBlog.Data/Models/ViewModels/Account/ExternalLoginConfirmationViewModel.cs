using System.ComponentModel.DataAnnotations;

namespace AcreeBlog.Models.ViewModels.Account
{
    public class ExternalLoginConfirmationViewModel
    {
    [Required]
    [EmailAddress]
    public string Email { get; set; }
  }
}

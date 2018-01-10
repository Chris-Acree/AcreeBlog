using System.ComponentModel.DataAnnotations;

namespace AcreeBlog.Models.ViewModels.Admin
{
    public class CategoryViewModel
  {
    [Required]
    public long Id { get; set; }

    [Required]
    public string Name { get; set; }
    
    [Required]
    public string Description { get; set; }
  }
}

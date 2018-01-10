using System.ComponentModel.DataAnnotations;

namespace AcreeBlog.ViewModels.Admin
{
    public class CategoryViewModel_dated
  {
    [Required]
    public long Id { get; set; }

    [Required]
    public string Name { get; set; }
    
    [Required]
    public string Description { get; set; }
  }
}

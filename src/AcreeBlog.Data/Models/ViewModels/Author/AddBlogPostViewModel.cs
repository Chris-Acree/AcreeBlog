using System.ComponentModel.DataAnnotations;

namespace AcreeBlog.Models.ViewModels.Author
{
    public class AddBlogPostViewModel
  {
    [Required]
    public string Title { get; set; }
    [Required]
    public string Content { get; set; }
    [Required]
    public string Description { get; set; }
  }
}

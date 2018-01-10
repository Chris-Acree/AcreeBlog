using System.ComponentModel.DataAnnotations;

namespace AcreeBlog.ViewModels.Author
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

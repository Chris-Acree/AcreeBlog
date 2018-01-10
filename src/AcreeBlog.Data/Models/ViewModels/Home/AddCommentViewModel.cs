using System.ComponentModel.DataAnnotations;

namespace AcreeBlog.Models.ViewModels.Home
{
    public class AddCommentViewModel
  {
    [Required]
    public long Id { get; set; }
    [Required]
    public string Slug { get; set; }
    [Required]
    public string Name { get; set; }
    public string Email { get; set; }
    public string Website { get; set; }
    [Required]
    public string Text { get; set; }
  }
}

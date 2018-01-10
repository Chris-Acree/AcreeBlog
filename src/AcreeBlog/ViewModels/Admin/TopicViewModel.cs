using System.ComponentModel.DataAnnotations;

namespace AcreeBlog.ViewModels.Admin
{
    public class TopicViewModel_dated
  {
    [Required]
    public long Id { get; set; }

    [Required]
    public string Title { get; set; }

    [Required]
    public bool ShowOnHomePage { get; set; }

    [Required]
    public string Description { get; set; }
  }
}

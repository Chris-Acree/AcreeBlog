using AcreeBlog.Data.Models;

namespace AcreeBlog.Data.Command.Commands
{
    public class AddAuthorCommand
  {
    public string ApplicatoinUserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    // Return Author
    public Author Author { get; set; }
  }
}

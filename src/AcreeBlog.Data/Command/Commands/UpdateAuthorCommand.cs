namespace AcreeBlog.Data.Command.Commands
{
    public class UpdateAuthorCommand
  {
    public long Id { get; set; }
    public string NewFirstName { get; set; }
    public string NewLastName { get; set; }
  }
}

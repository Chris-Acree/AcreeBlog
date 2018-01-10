namespace AcreeBlog.Data.Command.Commands
{
    public class EditTopicCommand
  {
    public long Id { get; set; }
    public string NewTitle { get; set; }
    public string NewDescription { get; set; }
    public bool NewShowOnHomePage { get; set; }
  }
}

﻿namespace AcreeBlog.Data.Command.Commands
{
    public class EditCategoryCommand
  {
    public long Id { get; set; }
    public string NewName { get; set; }
    public string NewDescription { get; set; }
  }
}
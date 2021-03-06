﻿using System;

namespace AcreeBlog.Data.Command
{
    public class CommandResult<TCommand>
  {
    public bool Succeeded { get; set; }
    public TCommand Command { get; set; }
    public Exception Ex { get; set; }

    public CommandResult(TCommand command, bool succeeded, Exception ex = null)
    {
      Succeeded = succeeded;
      Command = command;
      Ex = ex;
    }
  }
}

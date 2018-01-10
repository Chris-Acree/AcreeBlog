using AcreeBlog.Data.Command.Commands;
using System;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using AcreeBlog.Data.RepositoryCommon;

namespace AcreeBlog.Data.Command.EFCoreCommandHandlers
{
    public class EditAuthorProfileCommandEFCH : EFCHBase, ICommandHandlerAsync<EditAuthorProfileCommand>
  {
    public EditAuthorProfileCommandEFCH(BlogDbContext context) : base(context)
    {
    }

    public async Task<CommandResult<EditAuthorProfileCommand>> ExecuteAsync(EditAuthorProfileCommand command)
    {
      try
      {
        var author = await _context.Authors.Where(a => a.Id == command.Id).FirstOrDefaultAsync();

        if (command.FirstName == null || command.LastName == null || command.FirstName == "" || command.LastName == "")
        {
          throw new Exception("Error - First and Last name must be provided.");
        }

        author.FirstName = command.FirstName;
        author.LastName = command.LastName;
        author.Biography = command.Biography;
        author.Blurb = command.Blurb;
        author.ImageLink = command.ImageLink;

        await _context.SaveChangesAsync();

        command.Author = author;

        return new CommandResult<EditAuthorProfileCommand>(command, true);
      }
      catch (Exception ex)
      {
        return new CommandResult<EditAuthorProfileCommand>(command, false, ex);
      }
    }
  }
}

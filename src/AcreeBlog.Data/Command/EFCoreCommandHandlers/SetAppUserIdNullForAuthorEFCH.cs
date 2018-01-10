using AcreeBlog.Data.Command.Commands;
using System;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using AcreeBlog.Data.RepositoryCommon;

namespace AcreeBlog.Data.Command.EFCoreCommandHandlers
{
    public class SetAppUserIdNullForAuthorEFCH : EFCHBase, ICommandHandlerAsync<SetAppUserIdNullForAuthorCommand>
  {
    public SetAppUserIdNullForAuthorEFCH(BlogDbContext context) : base(context)
    {
    }

    public async Task<CommandResult<SetAppUserIdNullForAuthorCommand>> ExecuteAsync(SetAppUserIdNullForAuthorCommand command)
    {
      try
      {
        var Author = await _context.Authors.Where(a => a.ApplicationUserId == command.Id).FirstOrDefaultAsync();

        Author.ApplicationUser = null;
        Author.ApplicationUserId = null;

        await _context.SaveChangesAsync();

        return new CommandResult<SetAppUserIdNullForAuthorCommand>(command, true);
      }
      catch (Exception ex)
      {
        return new CommandResult<SetAppUserIdNullForAuthorCommand>(command, false, ex);
      }
    }
  }
}

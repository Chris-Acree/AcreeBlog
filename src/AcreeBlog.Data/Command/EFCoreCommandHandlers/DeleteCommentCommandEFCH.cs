using AcreeBlog.Data.Command.Commands;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using AcreeBlog.Data.RepositoryCommon;

namespace AcreeBlog.Data.Command.EFCoreCommandHandlers
{
    public class DeleteCommentCommandEFCH : EFCHBase, ICommandHandlerAsync<DeleteCommentCommand>
  {
    public DeleteCommentCommandEFCH(BlogDbContext context) : base(context)
    {
    }

    public async Task<CommandResult<DeleteCommentCommand>> ExecuteAsync(DeleteCommentCommand command)
    {
      var comment = await _context.Comments.Where(c => c.Id == command.Id).FirstOrDefaultAsync();

      if (comment == null)
      {
        return new CommandResult<DeleteCommentCommand>(command, true);
      }
      else
      {
        try
        {
          _context.Comments.Remove(comment);

          await _context.SaveChangesAsync();

          return new CommandResult<DeleteCommentCommand>(command, true);
        }
        catch (Exception ex)
        {
          return new CommandResult<DeleteCommentCommand>(command, false, ex);
        }
      }
    }
  }
}

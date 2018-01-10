using System;
using AcreeBlog.Data.Command.Commands;
using AcreeBlog.Data.Models;
using System.Threading.Tasks;
using AcreeBlog.Data.RepositoryCommon;

namespace AcreeBlog.Data.Command.EFCoreCommandHandlers
{
    public class DeleteBlogPostCommandEFCH : EFCHBase, ICommandHandlerAsync<DeleteBlogPostCommand>
  {
    public DeleteBlogPostCommandEFCH(BlogDbContext context) : base(context)
    {
    }

    public async Task<CommandResult<DeleteBlogPostCommand>> ExecuteAsync(DeleteBlogPostCommand command)
    {
      try
      {
        BlogPost toRemove = await _context.BlogPosts.FindAsync(command.Id);

        if (toRemove != null)
        {
          _context.BlogPosts.Remove(toRemove);

          await _context.SaveChangesAsync();

          return new CommandResult<DeleteBlogPostCommand>(command, true);
        }
        else
        {
          return new CommandResult<DeleteBlogPostCommand>(command, false);
        }
      }
      catch (Exception ex)
      {
        return new CommandResult<DeleteBlogPostCommand>(command, false, ex);
      }
    }
  }
}

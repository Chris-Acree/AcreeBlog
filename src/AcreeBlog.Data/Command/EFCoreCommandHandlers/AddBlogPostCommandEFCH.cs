using AcreeBlog.Data.Command.Commands;
using System;
using System.Threading.Tasks;
using AcreeBlog.Data.Models;
using AcreeBlog.Data.RepositoryCommon;

namespace AcreeBlog.Data.Command.EFCoreCommandHandlers
{
    public class AddBlogPostCommandEFCH : EFCHBase, ICommandHandlerAsync<AddBlogPostCommand>
  {
    public AddBlogPostCommandEFCH(BlogDbContext context) : base(context)
    {
    }

    public async Task<CommandResult<AddBlogPostCommand>> ExecuteAsync(AddBlogPostCommand command)
    {
      try
      {
        var bp = new BlogPost
        {
          Slug = BlogPostExtensions.CreateSlug(command.Title),
          Author = command.Author,
          AuthorId = command.Author.Id,
          Title = command.Title,
          Content = command.Content,
          Description = command.Description,
          CreatedAt = command.CreatedAt,
          ModifiedAt = command.ModifiedAt,
          Public = command.Public,
          PublishOn = command.PublishOn,
          Image = command.Image,
          IsHtml = command.IsHtml
        };

        _context.BlogPosts.Add(bp);

        await _context.SaveChangesAsync();

        command.Id = bp.Id;

        return new CommandResult<AddBlogPostCommand>(command, true);
      }
      catch (Exception ex)
      {
        return new CommandResult<AddBlogPostCommand>(command, false, ex);
      }
    }
  }
}

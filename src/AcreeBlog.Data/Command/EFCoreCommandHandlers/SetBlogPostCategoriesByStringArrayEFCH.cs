using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using AcreeBlog.Data.Command.Commands;
using System.Threading.Tasks;
using AcreeBlog.Data.Models;
using AcreeBlog.Data.RepositoryCommon;

namespace AcreeBlog.Data.Command.EFCoreCommandHandlers
{
    public class SetBlogPostCategoriesByStringArrayEFCH : EFCHBase, ICommandHandlerAsync<SetBlogPostCategoriesByStringArrayCommand>
  {
    public SetBlogPostCategoriesByStringArrayEFCH(BlogDbContext context) : base(context)
    {
    }

    public async Task<CommandResult<SetBlogPostCategoriesByStringArrayCommand>> ExecuteAsync(SetBlogPostCategoriesByStringArrayCommand command)
    {
      if (command.Categories.Count() == 0)
      {
        return await Task.FromResult(new CommandResult<SetBlogPostCategoriesByStringArrayCommand>(command, true));
      }
      else
      {
        try {

          var bpcts = await _context.BlogPostCategory.Where(bpc => bpc.BlogPostId == command.BlogPostId).ToListAsync();

          _context.BlogPostCategory.RemoveRange(bpcts);

          await _context.SaveChangesAsync();

          foreach (var category in command.Categories)
          {
            // find the category
            var cat = await _context.Categories.Where(c => c.Name.ToLower() == category).FirstOrDefaultAsync();

            if (cat != null)
            {
              var bpc = new BlogPostCategory
              {
                BlogPostId = command.BlogPostId,
                CategoryId = cat.Id
              };

              _context.BlogPostCategory.Add(bpc);
            }
          }

          await _context.SaveChangesAsync();

          return new CommandResult<SetBlogPostCategoriesByStringArrayCommand>(command, true);
        }
        catch (Exception ex)
        {
          return new CommandResult<SetBlogPostCategoriesByStringArrayCommand>(command, false, ex);
        }
      }
    }
  }
}

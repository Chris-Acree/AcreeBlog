using AcreeBlog.Data.Command.Commands;
using System;
using System.Threading.Tasks;
using AcreeBlog.Data.Models;
using AcreeBlog.Data.RepositoryCommon;

namespace AcreeBlog.Data.Command.EFCoreCommandHandlers
{
    public class AddCategoryCommandEFCH : EFCHBase, ICommandHandlerAsync<AddCategoryCommand>
  {
    public AddCategoryCommandEFCH(BlogDbContext context) : base(context)
    {
    }

    public async Task<CommandResult<AddCategoryCommand>> ExecuteAsync(AddCategoryCommand command)
    {
      try
      {
        var c = new Category()
        {
          Name = command.Name,
          Description = command.Description
        };

        _context.Categories.Add(c);

        await _context.SaveChangesAsync();

        command.Id = c.Id;

        return new CommandResult<AddCategoryCommand>(command, true);
      }
      catch (Exception ex)
      {
        return new CommandResult<AddCategoryCommand>(command, false, ex);
      }
    }
  }
}

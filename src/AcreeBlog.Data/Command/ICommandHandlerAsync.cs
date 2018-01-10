using System.Threading.Tasks;

namespace AcreeBlog.Data.Command
{
    interface ICommandHandlerAsync<TCommand>
  {
    Task<CommandResult<TCommand>> ExecuteAsync(TCommand command);
  }
}

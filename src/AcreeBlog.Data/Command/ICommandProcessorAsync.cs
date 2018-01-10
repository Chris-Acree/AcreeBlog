using System.Threading.Tasks;

namespace AcreeBlog.Data.Command
{
    public interface ICommandProcessorAsync
  {
    Task<CommandResult<TCommand>> ProcessAsync<TCommand>(TCommand command);
  }
}

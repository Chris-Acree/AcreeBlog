using System.Threading.Tasks;

namespace AcreeBlog.Data.Query
{
    public interface IQueryProcessorAsync
  {
    Task<TResult> ProcessAsync<TResult>(IQuery<TResult> query);
  }
}

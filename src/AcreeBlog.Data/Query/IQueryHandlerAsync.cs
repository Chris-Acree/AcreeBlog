using System.Threading.Tasks;

namespace AcreeBlog.Data.Query
{
    public interface IQueryHandlerAsync<TQuery, TResult> where TQuery : IQuery<TResult>
  {
    Task<TResult> HandleAsync(TQuery query);
  }
}

using AcreeBlog.Data.RepositoryCommon;

namespace AcreeBlog.Data.Query.EFCoreQueryHandlers
{
    public abstract class EFQHBase
  {
    protected BlogDbContext _context;

    public EFQHBase(BlogDbContext context)
    {
      _context = context;
    }
  }
}

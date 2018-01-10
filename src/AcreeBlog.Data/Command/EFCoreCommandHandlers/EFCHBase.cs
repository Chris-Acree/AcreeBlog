using AcreeBlog.Data.RepositoryCommon;

namespace AcreeBlog.Data.Command.EFCoreCommandHandlers
{
    public abstract class EFCHBase
  {
    protected BlogDbContext _context;

    public EFCHBase(BlogDbContext context)
    {
      _context = context;
    }
  }
}

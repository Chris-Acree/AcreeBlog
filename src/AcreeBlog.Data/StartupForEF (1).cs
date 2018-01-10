using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore.Design;
using AcreeBlog.Data.RepositoryCommon;

namespace AcreeBlog.Data
{
    /// <summary>
    /// The whole point of this class is to allow for EF Core code to exist in a seperate project from the web application
    /// and be able to do migrations and so forth
    /// dotnet ef migrations add test1 --startup-project ..\blog
    /// or
    /// https://github.com/aspnet/EntityFramework/issues/7889   -- using this fix
    /// https://docs.microsoft.com/en-us/ef/core/miscellaneous/configuring-dbcontext
    /// </summary>
    /// 
    class StartupForEF : IDesignTimeDbContextFactory<BlogDbContext>
  {
    public BlogDbContext CreateDbContext(string[] args)
    {
      var confBuilder = new ConfigurationBuilder()
       .AddEnvironmentVariables()
       .AddUserSecrets<StartupForEF>();

      IConfiguration Configuration = confBuilder.Build();

      var ConnectionStringConfigName = "ConnectionString";
      var ConnectionString = Configuration[$"{ConnectionStringConfigName}"];

      var builder = new DbContextOptionsBuilder<BlogDbContext>();

      builder.UseSqlServer(ConnectionString, b => b.UseRowNumberForPaging());

      return new BlogDbContext(builder.Options);
    }
  }
}

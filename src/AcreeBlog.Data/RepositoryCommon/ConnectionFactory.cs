
namespace AcreeBlog.Data.RepositoryCommon
{
    public class ConnectionFactory : IConnectionFactory
    {
        public string ConnectionString
        {
            get { return "YourConStringName"; }
        }
    }
}

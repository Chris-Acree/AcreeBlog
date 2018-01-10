using System.Linq;
using AcreeBlog.Data.RepositoryCommon;
using AcreeBlog.Data.Models;

namespace AcreeBlog.Data.Repositories
{
    public interface ITopicRepository : IRepository<Topic>
    {
        bool TopicTitleExists(string title);
    }



    public class TopicRepository : Repository<Topic>
    {
        private readonly BlogDbContext _dbContext;

        public TopicRepository(BlogDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        
        public bool TopicTitleExists(string title)
        {
            //var entityExists = _dbContext.Topics.FirstOrDefault(t => t.Name == name);
            return _dbContext.Topics.Any(t => t.Title == title);
        }

        /*
        public new IEnumerable<Topic> GetAll()
        {
            return _dbContext.Topics.ToList();
        }

        public new void Update(Topic topic)
        {
            _dbContext.Topics.Attach(topic);
        }

        public new void Delete(Topic topic)
        {
            _dbContext.Topics.Remove(topic);
        }
        */
    }
}

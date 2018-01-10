
using AcreeBlog.Data.RepositoryCommon;
using AcreeBlog.Data.Repositories;
using AutoMapper;
using AcreeBlog.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using PagedList.Core;

namespace AcreeBlog.Services
{
    public class AdminService
    {
        private readonly BlogDbContext _dbContext;
        private BlogPostRepository _blogPostRepository;
        private TopicRepository _topicRepository;
        private CategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public AdminService(BlogDbContext dbContext,
            BlogPostRepository blogPostRepository,
            TopicRepository topicRepository,
            CategoryRepository categoryRepository,
            IMapper mapper)
        {
            _dbContext = dbContext;
            _blogPostRepository = blogPostRepository;
            _topicRepository = topicRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public IEnumerable<Topic> TopicsGetAll()
        {
            return _topicRepository.GetAll();
        }

        public async Task<IPagedList<BlogPost>> GetAllPostsPaginatedAsync(int currentPage, int pageSize)
        {
            var result = await _blogPostRepository.GetAllPostsPaginatedAsync(currentPage, pageSize);
            return result;
        }


        public void AddTopic(Topic topic)
        {
            if (_topicRepository.TopicTitleExists(topic.Title))
            {
                // return error that the title already exists;
                return;
            }
            else
            {
                _topicRepository.Insert(topic);
            }
            
        }

        public IEnumerable<Category> CategoriessGetAll()
        {
            return _categoryRepository.GetAll();
        }
    }
}


using System.Collections.Generic;

namespace AcreeBlog.ViewModels
{
    public class TopicViewModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool ShowOnHomePage { get; set; }
        public ICollection<BlogPostTopicViewModel> BlogPosts { get; set; }
    }
}

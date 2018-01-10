using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcreeBlog.ViewModels.Admin
{
    public class BlogPostViewModel
    {
        public long Id { get; set; }
        public long? AuthorId { get; set; }
        public AuthorViewModel AuthorViewModel { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public DateTime PublishOn { get; set; }
        public bool Public { get; set; }
        public ICollection<BlogPostCategoryViewModel> BlogPostCategoryViewModel { get; set; }
        public ICollection<BlogPostTopicViewModel> BlogPostTopicViewModel { get; set; }
        public string Image { get; set; }
        public ICollection<CommentViewModel> CommentsViewModel { get; set; }
        public bool IsHtml { get; set; }
        public Guid RowGuid { get; set; }
    }
}

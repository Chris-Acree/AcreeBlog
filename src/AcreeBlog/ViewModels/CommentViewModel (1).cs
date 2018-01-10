using System;

namespace AcreeBlog.ViewModels
{
    public class CommentViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string WebSite { get; set; }
        public long BlogPostId { get; set; }
        public BlogPostViewModel BlogPost { get; set; }
        public string Text { get; set; }
        public DateTime Added { get; set; }
    }
}

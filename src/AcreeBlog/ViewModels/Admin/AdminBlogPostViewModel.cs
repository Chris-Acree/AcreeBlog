using System;

namespace AcreeBlog.ViewModels.Admin
{
    public class AdminBlogPostViewModel
    {
        public long Id { get; set; }
        
        public long? AuthorId { get; set; }
        public string AuthorFirstName { get; set; }
        public string AuthorLastName { get; set; }
        public string AuthorApplicationUserId { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
        public DateTime PublishOn { get; set; }
        public bool Public { get; set; }
        /*
         * 
        
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        
        
        public string Image { get; set; }
        public bool IsHtml { get; set; }
        public Guid RowGuid { get; set; }
        */
    }
}

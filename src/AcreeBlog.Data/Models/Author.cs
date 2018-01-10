using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AcreeBlog.Data.RepositoryCommon;

namespace AcreeBlog.Data.Models
{
    public class Author : BaseEntity
    {
        public new long Id { get; set; }
        public string ApplicationUserId { get; set; }
        [NotMapped]
        public ApplicationUser ApplicationUser { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        public string Blurb { get; set; }

        public string Biography { get; set; }

        public string ImageLink { get; set; }

        public virtual ICollection<BlogPost> BlogPosts { get; set; }
    }
}

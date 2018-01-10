using AcreeBlog.Models.ViewModels.Home;
using System.Collections.Generic;

namespace AcreeBlog.Data.Query.Queries
{

    public class GetSearchResultBlogPostsBySearchTextViewModelQuery : IQuery<IList<SearchResultBlogPostViewModel>>
  {
    public string SearchText { get; set; }
  }

}

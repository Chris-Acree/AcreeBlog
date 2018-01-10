using System.Collections.Generic;
using AcreeBlog.Data.Models;

namespace AcreeBlog.ViewModels.Admin
{
    public class ManageCategoriesViewModel
  {
    public IEnumerable<Category> Categories { get; set; }
  }
}

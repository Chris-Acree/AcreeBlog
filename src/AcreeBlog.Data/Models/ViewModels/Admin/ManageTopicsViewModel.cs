using System.Collections.Generic;
using AcreeBlog.Data.Models;

namespace AcreeBlog.Models.ViewModels.Admin
{
    public class ManageTopicsViewModel
  {
    public IEnumerable<Topic> Topics { get; set; }
  }
}

﻿using AcreeBlog.Data.Query.Queries;
using AcreeBlog.Models.ViewModels.Home;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using AcreeBlog.Data.RepositoryCommon;

namespace AcreeBlog.Data.Query.EFCoreQueryHandlers
{
    public class GetTopicsViewModelEFQH : EFQHBase, IQueryHandlerAsync<GetTopicsViewModelQuery, TopicsViewModel>
  {
    public GetTopicsViewModelEFQH(BlogDbContext context) : base(context)
    {
    }

    public async Task<TopicsViewModel> HandleAsync(GetTopicsViewModelQuery query)
    {
      // bug in EF Core that needs a bit more code here than otherwise
      // see: https://github.com/aspnet/EntityFramework/issues/7714

      var vfvm = new TopicsViewModel();

      // cant select new into a defined type so have to use anon type for the select new here due to EF Core bug
      // code after is a work-around

      var x = from topic in _context.Topics.AsNoTracking()
              select new
              {
                Title = topic.Title,
                Description = topic.Description,
                Id = topic.Id,
                BlogCount = (from posts in _context.BlogPostTopic
                             where posts.TopicId == topic.Id && posts.BlogPost.Public == true && posts.BlogPost.PublishOn < DateTime.Now
                             select posts).AsNoTracking().Count()
              };

      var topicList = new List<TopicWithBlogCountViewModel>();

      foreach (var topic in await x.OrderByDescending(t => t.BlogCount).ToListAsync())
      {
        var fvm = new TopicWithBlogCountViewModel()
        {
          BlogCount = topic.BlogCount,
          Title = topic.Title,
          Description = topic.Description,
          Id = topic.Id
        };
        topicList.Add(fvm);
      }

      vfvm.Topics = topicList;

      return vfvm;

    }
  }
}

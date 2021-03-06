﻿using AcreeBlog.Data.RepositoryCommon;
using AcreeBlog.Data.Query.Queries;
using AcreeBlog.Data.Models;
using AcreeBlog.Models.ViewModels.Home;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcreeBlog.Data.Query.EFCoreQueryHandlers
{
    public class GetCategoryPostsByCategoryViewModelEFQH : EFQHBase, IQueryHandlerAsync<GetCategoryPostsByCategoryViewModelQuery, CategoryPostsViewModel>
  {
    public GetCategoryPostsByCategoryViewModelEFQH(BlogDbContext context) : base(context)
    {
    }

    public async Task<CategoryPostsViewModel> HandleAsync(GetCategoryPostsByCategoryViewModelQuery query)
    {
      var category = await _context.Categories.FindAsync(query.Id);

      var vcpvm = new CategoryPostsViewModel()
      {
        Category = category,
        Posts = new List<BlogPost>()
      };

      if (category != null)
      {
        var bpcs = await _context.BlogPostCategory
          .AsNoTracking()
          .Where(bpc => bpc.CategoryId == category.Id && bpc.BlogPost.Public == true && bpc.BlogPost.PublishOn < DateTime.Now)
          .Include(bpc => bpc.BlogPost)
            .ThenInclude(bp => bp.Author)
          .OrderByDescending(bpc => bpc.BlogPost.PublishOn)
          .ToListAsync();

        vcpvm.Category = category;
        vcpvm.Posts = bpcs.Select(bp => bp.BlogPost).ToList();

        return vcpvm;
      }
      else
      {
        return null;
      }
    }
  }
}

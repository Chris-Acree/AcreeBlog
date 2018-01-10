using System;
using System.Collections.Generic;
using System.Linq;
using AcreeBlog.Data.Models;
using AcreeBlog.ViewModels;
using AutoMapper;
using PagedList.Core;

namespace AcreeBlog.Common
{
    public class PagedListConverter : ITypeConverter<PagedList<BlogPost>, PagedList<BlogPostViewModel>>
    {
        public PagedList<BlogPostViewModel> Convert(ResolutionContext context)
        {
            var model = (PagedList<BlogPost>)context.SourceValue;
            var vm = model.Select(m => Mapper.Map<BlogPost, BlogPostViewModel>(m)).ToList();

            return new PagedList<BlogPostViewModel>(vm, model.PageNumber, model.PageSize);
        }
    }
 }


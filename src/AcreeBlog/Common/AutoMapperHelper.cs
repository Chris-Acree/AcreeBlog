using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AutoMapper;
using X.PagedList;

namespace AcreeBlog.Common
{
    public static class AutoMapperHelper
    {
        /// <summary>
        /// Maps properties as ignored.
        /// </summary>
        public static IMappingExpression<TSource, TDestination> Ignore<TSource, TDestination>(
            this IMappingExpression<TSource, TDestination> map,
            Expression<Func<TDestination, object>> selector)
        {
            map.ForMember(selector, config => config.Ignore());

            return map;
        }

        /// <summary>
        /// Sets mapping from source property to destination property. Convenient extension method. 
        /// </summary>
        public static IMappingExpression<TSource, TDestination> MapProperty<TSource, TDestination, TProperty>(
            this IMappingExpression<TSource, TDestination> map,
            Expression<Func<TSource, TProperty>> sourceMember,
            Expression<Func<TDestination, object>> targetMember)
        {
            map.ForMember(targetMember, opt => opt.MapFrom(sourceMember));

            return map;
        }

        /// <summary>
        /// Get Automapper to map a paged list
        /// https://stackoverflow.com/questions/2070850/can-automapper-map-a-paged-list
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static IPagedList<TDestination> ToMappedPagedList<TSource, TDestination>(this IPagedList<TSource> list)
        {
            IEnumerable<TDestination> sourceList = Mapper.Map<IEnumerable<TSource>, IEnumerable<TDestination>>(list);
            IPagedList<TDestination> pagedResult = new StaticPagedList<TDestination>(sourceList, list.GetMetaData());
            return pagedResult;

        }
    }

}

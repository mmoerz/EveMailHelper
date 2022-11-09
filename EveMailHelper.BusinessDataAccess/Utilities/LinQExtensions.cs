using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveNatTools.ServiceLibrary.Utilities
{
    public static class LinqExtensions
    {
        /// <summary>
        /// A generic Page Query Object method that limits the resultset
        /// to a specific pagenumber and number of entries per page.
        /// Calculation for the correct page offset must happen somewhere else.
        /// 
        /// ONLY WORKS IF INPUT IS ALREADY SORTED!!! Otherwise exception happens.
        /// 
        /// TODO: think about whether that really should be somewhere else.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query">query to operate on (this)</param>
        /// <param name="pageOffset">offset in number of records to skip</param>
        /// <param name="pageSize">number of records for a single page</param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static IQueryable<T> Page<T>(
            this IQueryable<T> query,
            int pageOffset,
            int pageSize)
        {
            if (pageSize == 0)
                throw new ArgumentOutOfRangeException
                    (nameof(pageSize), "pageSize cannot be zero.");
            if (pageOffset != 0)
                query = query
                    .Skip(pageOffset * pageSize);

            return query.Take(pageSize);
        }

        /// <summary>
        /// checks that all entries of ienumerable a is present in ienumerable b
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool ContainsAllItems<T>(this IEnumerable<T> a, IEnumerable<T> b)
        {
            return !b.Except(a).Any();
        }
    }
}

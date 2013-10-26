// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ListExtensions type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Infrastructure.Extensions
{
    using System.Collections.Generic;

    /// <summary>
    ///  Defines the ListExtensions type.
    /// </summary>
    public static class ListExtensions
    {
        /// <summary>
        /// Adds the range.
        /// </summary>
        /// <typeparam name="T">Type of items in the list.</typeparam>
        /// <param name="list">The list.</param>
        /// <param name="range">The range.</param>
        /// <returns>A List.</returns>
        public static IList<T> AddRange<T>(
            this IList<T> list, 
            IEnumerable<T> range)
        {
            foreach (T t in range)
            {
                list.Add(t);
            }

            return list;
        }

    }
}

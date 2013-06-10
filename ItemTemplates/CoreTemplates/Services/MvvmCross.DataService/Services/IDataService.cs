// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the IDataService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace MvvmCross.DataService.Services
{
    using System.Collections.Generic;

    /// <summary>
    /// Defines the IDataService type.
    /// </summary>
    public interface IDataService<T>
    {
        /// <summary>
        /// Gets the count.
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Gets the specified name filter.
        /// </summary>
        /// <param name="nameFilter">The name filter.</param>
        /// <returns>A list of type T.</returns>
        List<T> Get(string nameFilter);

        /// <summary>
        /// Inserts the specified t.
        /// </summary>
        /// <param name="t">The t.</param>
        void Insert(T t);

        /// <summary>
        /// Updates the specified t.
        /// </summary>
        /// <param name="t">The t.</param>
        void Update(T t);

        /// <summary>
        /// Deletes the specified t.
        /// </summary>
        /// <param name="t">The t.</param>
        void Delete(T t);
    }
}

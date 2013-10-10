// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the IBaseSqliteDataService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace MvvmCross.SqliteDataService.Services
{
    using System.Collections.Generic;

    /// <summary>
    /// Defines the IBaseSqliteDataService type.
    /// </summary>
    /// <typeparam name="T">The data type.</typeparam>
    public interface IBaseSqliteDataService<T>
    {
        /// <summary>
        /// Gets the count.
        /// </summary>
        int Count { get; }
        
        /// <summary>
        /// Gets all the items.
        /// </summary>
        /// <returns>All the items.</returns>
        List<T> GetAll();

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
// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the Grouping type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace $rootnamespace$.Mvvm
{
    using System.Collections.Generic;

    /// <summary>
    /// Grouping of items by key into ObservableRange
    /// </summary>
    public class Grouping<T, TV> : ObservableRangeCollection<TV>
    {
        /// <summary>
        /// Gets the key.
        /// </summary>
        /// <value>The key.</value>
        public T Key { get; set; }
         
        /// <summary>
        /// Initializes a new instance of the Grouping class.
        /// </summary>
        /// <param name="key">Key.</param>
        /// <param name="items">Items.</param>
        public Grouping(T key, IEnumerable<TV> items)
        {
            this.Key = key;
            this.AddRange(items);
        }
    }
}


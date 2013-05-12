// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the IConvertersView type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NinjaCoder.MvvmCross.Views.Interfaces
{
    using System.Collections.Generic;

    /// <summary>
    ///  Defines the IConvertersView type.
    /// </summary>
    public interface IConvertersView
    {
        /// <summary>
        /// Gets the required converters.
        /// </summary>
        List<string> RequiredConverters { get; }

        /// <summary>
        /// Adds the template.
        /// </summary>
        /// <param name="name">The name.</param>
        void AddTemplate(string name);
    }
}
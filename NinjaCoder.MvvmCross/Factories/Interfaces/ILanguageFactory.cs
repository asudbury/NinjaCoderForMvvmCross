// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ILanguageFactory type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Factories.Interfaces
{
    using System.Collections.Generic;

    /// <summary>
    /// Defines the ILanguageFactory type.
    /// </summary>
    public interface ILanguageFactory
    {
        /// <summary>
        /// Gets the languages.
        /// </summary>
        IEnumerable<string> Languages { get; }
    }
}

// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the IFrameworkFactory type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Factories.Interfaces
{
    using System.Collections.Generic;

    /// <summary>
    /// Defines the IFrameworkFactory type.
    /// </summary>
    public interface IFrameworkFactory
    {
        /// <summary>
        /// Gets the frameworks.
        /// </summary>
        IEnumerable<string> Frameworks { get; }

        /// <summary>
        /// Gets the allowed frameworks.
        /// </summary>
        IEnumerable<string> AllowedFrameworks { get; }

    }
}

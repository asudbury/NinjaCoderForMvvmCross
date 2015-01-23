// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the IXamarinLayoutFactory type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Factories.Interfaces
{
    using Scorchio.Infrastructure.Entities;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the IXamarinLayoutFactory type.
    /// </summary>
    public interface IXamarinLayoutFactory
    {
        /// <summary>
        /// Gets the layouts.
        /// </summary>
        IEnumerable<ImageItemWithDescription> Layouts { get; }
    }
}

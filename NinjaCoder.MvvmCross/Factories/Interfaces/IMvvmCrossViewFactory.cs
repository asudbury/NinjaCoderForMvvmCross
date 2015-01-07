// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the IMvvmCrossViewFactory type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Factories.Interfaces
{
    using System.Collections.Generic;
    using Scorchio.Infrastructure.Entities;

    /// <summary>
    /// Defines the IMvvmCrossViewFactory type.
    /// </summary>
    public interface IMvvmCrossViewFactory
    {
        /// <summary>
        /// Gets the views.
        /// </summary>
        IEnumerable<ImageItemWithDescription> Views { get; }
    }
}

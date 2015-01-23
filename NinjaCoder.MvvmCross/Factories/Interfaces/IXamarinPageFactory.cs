// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the IXamarinPageFactory type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Factories.Interfaces
{
    using Scorchio.Infrastructure.Entities;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the IXamarinPageFactory type.
    /// </summary>
    public interface IXamarinPageFactory
    {
        /// <summary>
        /// Gets the pages.
        /// </summary>
        IEnumerable<ImageItemWithDescription> Pages { get; }
    }
}

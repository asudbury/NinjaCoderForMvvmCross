// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the IProvideViewType type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Scorchio.Infrastructure.Wpf.ViewModels.Wizard
{
    using System;

    /// <summary>
    /// Defines the IProvideViewType type.
    /// </summary>
    public interface IProvideViewType
    {
        /// <summary>
        /// Gets the type of the view.
        /// </summary>
        Type ViewType { get; }
    }
}

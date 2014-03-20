// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the IViewModelAndViewsFactory type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Factories.Interfaces
{
    using System.Collections.Generic;

    using Scorchio.VisualStudio.Entities;

    /// <summary>
    ///  Defines the IViewModelAndViewsFactory type.
    /// </summary>
    public interface IViewModelAndViewsFactory
    {
        /// <summary>
        /// Gets the allowed ui views.
        /// </summary>
        IEnumerable<ItemTemplateInfo> AllowedUIViews { get; }

        /// <summary>
        /// Gets the available view types.
        /// </summary>
        /// <returns>The available view types.</returns>
        IEnumerable<string> GetAvailableViewTypes();

        /// <summary>
        /// Gets the required templates.
        /// </summary>
        /// <param name="viewModelName">Name of the view model.</param>
        /// <param name="requiredUIViews">The required UI views.</param>
        /// <param name="includeUnitTests">if set to <c>true</c> [include unit tests].</param>
        /// <returns>The required template.</returns>
        IEnumerable<ItemTemplateInfo> GetRequiredViewModelAndViews(
            string viewModelName,
            IEnumerable<ItemTemplateInfo> requiredUIViews,
            bool includeUnitTests);
    }
}
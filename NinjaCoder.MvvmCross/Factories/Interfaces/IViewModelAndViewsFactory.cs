// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the IViewModelAndViewsFactory type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Factories.Interfaces
{
    using Entities;
    using Scorchio.VisualStudio.Entities;
    using System.Collections.Generic;

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
        /// Gets the required view model and views.
        /// </summary>
        /// <param name="view">The view.</param>
        /// <param name="viewModelName">Name of the view model.</param>
        /// <param name="requiredUIViews">The required UI views.</param>
        /// <param name="unitTestsRequired">if set to <c>true</c> [unit tests required].</param>
        /// <returns></returns>
        IEnumerable<ItemTemplateInfo> GetRequiredViewModelAndViews(
            View view,
            string viewModelName,
            IEnumerable<ItemTemplateInfo> requiredUIViews,
            bool unitTestsRequired);
    }
}
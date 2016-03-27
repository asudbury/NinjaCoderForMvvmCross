// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the IMvvmCrossViewFactory type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Factories.Interfaces
{
    using Entities;
    using Scorchio.Infrastructure.Entities;
    using Scorchio.VisualStudio.Entities;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    /// <summary>
    /// Defines the IMvvmCrossViewFactory type.
    /// </summary>
    public interface IMvvmCrossViewFactory
    {
        /// <summary>
        /// Gets the views.
        /// </summary>
        ObservableCollection<ImageItemWithDescription> Views { get; }

        /// <summary>
        /// Gets the MVVM cross view.
        /// </summary>
        /// <param name="viewModelName">Name of the view model.</param>
        /// <param name="itemTemplateInfo">The item template information.</param>
        /// <param name="view">The view.</param>
        /// <returns>A TextTemplateInfo.</returns>
        TextTemplateInfo GetMvvmCrossView(
            string viewModelName, 
            ItemTemplateInfo itemTemplateInfo, 
            View view);

        /// <summary>
        /// Gets the MVVM cross view.
        /// </summary>
        /// <param name="itemTemplateInfo">The item template information.</param>
        /// <param name="tokens">The tokens.</param>
        /// <param name="viewTemplateName">Name of the view template.</param>
        /// <param name="viewName">Name of the view.</param>
        /// <returns>A TextTemplateInfo.</returns>
        TextTemplateInfo GetMvvmCrossView(
            ItemTemplateInfo itemTemplateInfo,
            Dictionary<string, string> tokens,
            string viewTemplateName,
            string viewName);
    }
}

// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the IViewModelViewsService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Services.Interfaces
{
    using NinjaCoder.MvvmCross.Entities;

    using Scorchio.VisualStudio.Entities;
    using Scorchio.VisualStudio.Services.Interfaces;
    using System.Collections.Generic;

    /// <summary>
    ///  Defines the IViewModelViewsService type.
    /// </summary>
    public interface IViewModelViewsService
    {
        /// <summary>
        /// Adds the view model and views.
        /// </summary>
        /// <param name="coreProjectService">The core project service.</param>
        /// <param name="templateInfos">The template infos.</param>
        /// <param name="textTemplateInfos">The text template infos.</param>
        /// <param name="viewModelName">Name of the view model.</param>
        /// <param name="addUnitTests">if set to <c>true</c> [add unit tests].</param>
        /// <param name="viewModelInitiateFrom">The view model initiate from.</param>
        /// <param name="viewModelNavigateTo">The view model navigate to.</param>
        /// <returns>
        /// The messages.
        /// </returns>
        IEnumerable<string> AddViewModelAndViews(
            IProjectService coreProjectService,
            IEnumerable<ItemTemplateInfo> templateInfos,
            IEnumerable<TextTemplateInfo> textTemplateInfos,
            string viewModelName,
            bool addUnitTests,
            string viewModelInitiateFrom,
            string viewModelNavigateTo);

        /// <summary>
        /// Adds the view models and views.
        /// </summary>
        /// <param name="views">The views.</param>
        /// <returns></returns>
        IEnumerable<string> AddViewModelsAndViews(IEnumerable<View> views);
    }
}
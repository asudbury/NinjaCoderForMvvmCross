// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the IViewModelViewsService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Services.Interfaces
{
    using System.Collections.Generic;

    using Scorchio.VisualStudio.Entities;
    using Scorchio.VisualStudio.Services.Interfaces;

    /// <summary>
    ///  Defines the IViewModelViewsService type.
    /// </summary>
    internal interface IViewModelViewsService
    {
        /// <summary>
        /// Adds the view model and views.
        /// </summary>
        /// <param name="coreProjectService">The core project service.</param>
        /// <param name="visualStudioService">The visual studio service.</param>
        /// <param name="templateInfos">The template infos.</param>
        /// <param name="viewModelName">Name of the view model.</param>
        /// <param name="addUnitTests">if set to <c>true</c> [add unit tests].</param>
        /// <param name="viewModelInitiateFrom">The view model initiate from.</param>
        /// <param name="viewModelNavigateTo">The view model navigate to.</param>
        /// <returns>The messages.</returns>
        IEnumerable<string> AddViewModelAndViews(
            IProjectService coreProjectService,
            IVisualStudioService visualStudioService,
            IEnumerable<ItemTemplateInfo> templateInfos,
            string viewModelName,
            bool addUnitTests,
            string viewModelInitiateFrom,
            string viewModelNavigateTo);
    }
}
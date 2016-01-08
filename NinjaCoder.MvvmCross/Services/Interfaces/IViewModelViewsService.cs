// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the IViewModelViewsService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Services.Interfaces
{
    using NinjaCoder.MvvmCross.Entities;
    using Scorchio.VisualStudio.Entities;
    using System.Collections.Generic;

    /// <summary>
    ///  Defines the IViewModelViewsService type.
    /// </summary>
    public interface IViewModelViewsService
    {
        /// <summary>
        /// Adds the view model and views.
        /// </summary>
        /// <param name="templateInfos">The template infos.</param>
        /// <param name="textTemplateInfos">The text template infos.</param>
        /// <returns>
        /// The messages.
        /// </returns>
        IEnumerable<string> AddViewModelAndViews(
            IEnumerable<ItemTemplateInfo> templateInfos,
            IEnumerable<TextTemplateInfo> textTemplateInfos);

        /// <summary>
        /// Adds the view models and views.
        /// </summary>
        /// <param name="views">The views.</param>
        /// <returns></returns>
        IEnumerable<string> AddViewModelsAndViews(IEnumerable<View> views);
    }
}
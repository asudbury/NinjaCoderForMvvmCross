// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the IViewModelViewsService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Services.Interfaces
{
    using Entities;
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
        /// <param name="textTemplateInfos">The text template infos.</param>
        /// <returns>
        /// The messages.
        /// </returns>
        IEnumerable<string> AddViewModelAndViews(IEnumerable<TextTemplateInfo> textTemplateInfos);

        /// <summary>
        /// Adds the view models and views.
        /// </summary>
        /// <param name="views">The views.</param>
        /// <returns>A list of viewmodels and views.</returns>
        IEnumerable<string> AddViewModelsAndViews(IEnumerable<View> views);
        
        /// <summary>
        /// Gets the nuget commands.
        /// </summary>
        /// <returns>A list of nuget commands.</returns>
        IEnumerable<string> GetNugetCommands();
    }
}
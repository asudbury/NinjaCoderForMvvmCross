// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the IFormsService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Services.Interfaces
{
    using System.Collections.Generic;

    using NinjaCoder.MvvmCross.Entities;
    using NinjaCoder.MvvmCross.Views;
    using NinjaCoder.MvvmCross.Views.Interfaces;

    using Scorchio.VisualStudio.Entities;

    /// <summary>
    ///  Defines the IFormsService type.
    /// </summary>
    public interface IFormsService
    {
        /// <summary>
        /// Gets the solution options form.
        /// </summary>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="defaultProjectsLocation">The default projects location.</param>
        /// <param name="defaultProjectName">Default name of the project.</param>
        /// <param name="projectInfos">The project infos.</param>
        /// <returns>the solution options form.</returns>
        IProjectsView GetSolutionOptionsForm(
            ISettingsService settingsService,
            string defaultProjectsLocation,
            string defaultProjectName,
            IEnumerable<ProjectTemplateInfo> projectInfos);

        /// <summary>
        /// Gets the view model options form.
        /// </summary>
        /// <param name="itemTemplateInfos">The item template infos.</param>
        /// <returns>The View Model Options form.</returns>
        IViewModelViewsView GetViewModelOptionsForm(IEnumerable<ItemTemplateInfo> itemTemplateInfos);

        /// <summary>
        /// Gets the plugins form.
        /// </summary>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="viewModelNames">The view model names.</param>
        /// <param name="plugins">The plugins.</param>
        /// <returns>The plugins form.</returns>
        IPluginsView GetPluginsForm(
            ISettingsService settingsService, 
            IEnumerable<string> viewModelNames, 
            Plugins plugins);

        /// <summary>
        /// Gets the item templates form.
        /// </summary>
        /// <param name="itemTemplateInfos">The item template infos.</param>
        /// <param name="settingsService">The settings service.</param>
        /// <returns>The item templates form.</returns>
        IItemTemplatesView GetItemTemplatesForm(
            IEnumerable<ItemTemplateInfo> itemTemplateInfos, 
            ISettingsService settingsService);

        /// <summary>
        /// Gets the services form.
        /// </summary>
        /// <param name="viewModelNames">The view model names.</param>
        /// <param name="itemTemplateInfos">The item template infos.</param>
        /// <param name="settingsService">The settings service.</param>
        /// <returns>The services form.</returns>
        IServicesView GetServicesForm(
            IEnumerable<string>viewModelNames, 
            IEnumerable<ItemTemplateInfo>itemTemplateInfos, 
            ISettingsService settingsService);

        /// <summary>
        /// Gets the options form.
        /// </summary>
        /// <param name="settingsService">The settings service.</param>
        /// <returns>The options form.</returns>
        IOptionsView GetOptionsForm(ISettingsService settingsService);

        /// <summary>
        /// Gets the about box form.
        /// </summary>
        /// <returns>The about box form.</returns>
        AboutBoxForm GetAboutBoxForm();
    }
}
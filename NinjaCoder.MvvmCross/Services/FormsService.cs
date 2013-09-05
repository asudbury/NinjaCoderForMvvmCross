// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the FormsService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using NinjaCoder.MvvmCross.Views.Interfaces;

namespace NinjaCoder.MvvmCross.Services
{
    using Entities;
    using Interfaces;
    using Scorchio.VisualStudio.Entities;
    using System.Collections.Generic;
    using Views;

    /// <summary>
    ///  Defines the FormsService type.
    /// </summary>
    public class FormsService : IFormsService
    {
        /// <summary>
        /// Gets the solution options form.
        /// </summary>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="defaultProjectsLocation">The default projects location.</param>
        /// <param name="defaultProjectName">Default name of the project.</param>
        /// <param name="projectInfos">The project infos.</param>
        /// <returns>the solution options form.</returns>
        public IProjectsView GetSolutionOptionsForm(
            ISettingsService settingsService, 
            string defaultProjectsLocation, 
            string defaultProjectName, 
            IEnumerable<ProjectTemplateInfo> projectInfos)
        {
            return new ProjectsForm(
                settingsService, 
                defaultProjectsLocation,
                defaultProjectName,
                projectInfos);
        }

        /// <summary>
        /// Gets the view model options view.
        /// </summary>
        /// <param name="itemTemplateInfos">The item template infos.</param>
        /// <returns>The View Model Options view.</returns>
        public IViewModelViewsView GetViewModelOptionsForm(IEnumerable<ItemTemplateInfo> itemTemplateInfos)
        {
            return new ViewModelViewsForm(itemTemplateInfos);
        }

        /// <summary>
        /// Gets the plugins form.
        /// </summary>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="viewModelNames">The view model names.</param>
        /// <param name="plugins">The plugins.</param>
        /// <returns>The plugins form.</returns>
        public IPluginsView GetPluginsForm(
            ISettingsService settingsService, 
            IEnumerable<string> viewModelNames, 
            Plugins plugins)
        {
            return new PluginsForm(
                settingsService, 
                viewModelNames, 
                plugins);
        }

        /// <summary>
        /// Gets the item templates form.
        /// </summary>
        /// <param name="itemTemplateInfos">The item template infos.</param>
        /// <param name="settingsService">The settings service.</param>
        /// <returns>The item templates form.</returns>
        public IItemTemplatesView GetItemTemplatesForm(IEnumerable<ItemTemplateInfo> itemTemplateInfos, ISettingsService settingsService)
        {
            return new ItemTemplatesForm(
                itemTemplateInfos, 
                settingsService);
        }

        /// <summary>
        /// Gets the services form.
        /// </summary>
        /// <param name="viewModelNames">The view model names.</param>
        /// <param name="itemTemplateInfos">The item template infos.</param>
        /// <param name="settingsService">The settings service.</param>
        /// <returns>The services form.</returns>
        public IServicesView GetServicesForm(
            IEnumerable<string> viewModelNames, 
            IEnumerable<ItemTemplateInfo> itemTemplateInfos, 
            ISettingsService settingsService)
        {
            return new ServicesForm(
                viewModelNames, 
                itemTemplateInfos, 
                settingsService);
        }

        /// <summary>
        /// Gets the options form.
        /// </summary>
        /// <param name="settingsService">The settings service.</param>
        /// <returns>The options form.</returns>
        public IOptionsView GetOptionsForm(ISettingsService settingsService)
        {
            return new OptionsForm(settingsService);
        }

        /// <summary>
        /// Gets the about box form.
        /// </summary>
        /// <returns>The about box form.</returns>
        public AboutBoxForm GetAboutBoxForm()
        {
            return new AboutBoxForm();
        }
    }
}

// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ViewModelAndViewsFactory type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Factories
{
    using System.Collections.Generic;

    using NinjaCoder.MvvmCross.Constants;
    using NinjaCoder.MvvmCross.Factories.Interfaces;
    using NinjaCoder.MvvmCross.Infrastructure.Services;
    using NinjaCoder.MvvmCross.Services.Interfaces;

    using Scorchio.VisualStudio.Entities;

    /// <summary>
    ///  Defines the ViewModelAndViewsFactory type.
    /// </summary>
    public class ViewModelAndViewsFactory : IViewModelAndViewsFactory
    {
        /// <summary>
        /// The visual studio service.
        /// </summary>
        private readonly IVisualStudioService visualStudioService;

        /// <summary>
        /// The settings service.
        /// </summary>
        private readonly ISettingsService settingsService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewModelAndViewsFactory" /> class.
        /// </summary>
        /// <param name="visualStudioService">The visual studio service.</param>
        /// <param name="settingsService">The settings service.</param>
        public ViewModelAndViewsFactory(
            IVisualStudioService visualStudioService,
            ISettingsService settingsService)
        {
            this.visualStudioService = visualStudioService;
            this.settingsService = settingsService;
        }

        /// <summary>
        /// Gets the allowed ui views.
        /// </summary>
        public IEnumerable<ItemTemplateInfo> AllowedUIViews
        {
            get
            {
                List<ItemTemplateInfo> itemTemplateInfos = new List<ItemTemplateInfo>();

                if (this.visualStudioService.iOSProjectService != null)
                {
                    itemTemplateInfos.Add(new ItemTemplateInfo
                    {
                        FriendlyName = FriendlyNames.iOS,
                        ProjectSuffix = ProjectSuffixes.iOS,
                        PreSelected = true
                    });
                }

                if (this.visualStudioService.DroidProjectService != null)
                {
                    itemTemplateInfos.Add(new ItemTemplateInfo
                    {
                        FriendlyName = FriendlyNames.Droid,
                        ProjectSuffix = ProjectSuffixes.Droid,
                        PreSelected = true
                    });
                }

                if (this.visualStudioService.WindowsPhoneProjectService != null)
                {
                    itemTemplateInfos.Add(new ItemTemplateInfo
                    {
                        FriendlyName = FriendlyNames.WindowsPhone,
                        ProjectSuffix = ProjectSuffixes.WindowsPhone,
                        PreSelected = true
                    });
                }

                if (this.visualStudioService.WindowsStoreProjectService != null)
                {
                    itemTemplateInfos.Add(new ItemTemplateInfo
                    {
                        FriendlyName = FriendlyNames.WindowsStore,
                        ProjectSuffix = ProjectSuffixes.WindowsStore,
                        PreSelected = true
                    });
                }

                if (this.visualStudioService.WpfProjectService != null)
                {
                    itemTemplateInfos.Add(new ItemTemplateInfo
                    {
                        FriendlyName = FriendlyNames.WindowsWpf,
                        ProjectSuffix = ProjectSuffixes.WindowsWpf,
                        PreSelected = true
                    });
                }

                return itemTemplateInfos;
            }
        }

        /// <summary>
        /// Gets the available views.
        /// </summary>
        /// <returns>The list of available views.</returns>
        public IEnumerable<string> GetAvailableViewTypes()
        {
            return new List<string>
            {
                "Blank", "SampleData", "Web"
            };
        }

        /// <summary>
        /// Gets the required templates.
        /// </summary>
        /// <param name="viewModelName">Name of the view model.</param>
        /// <param name="requiredUIViews">The required UI views.</param>
        /// <param name="solutionAlreadyCreated">if set to <c>true</c> [solution already created].</param>
        /// <returns>
        /// The required template.
        /// </returns>
        public IEnumerable<ItemTemplateInfo> GetRequiredViewModelAndViews(
            string viewModelName,
            IEnumerable<ItemTemplateInfo> requiredUIViews,
            bool solutionAlreadyCreated)
        {
            const string ViewModelSuffix = "ViewModel";

            List<ItemTemplateInfo> itemTemplateInfos = new List<ItemTemplateInfo>();

            //// first add the view model

            if (solutionAlreadyCreated == false)
            {
                ItemTemplateInfo viewModelTemplateInfo = new ItemTemplateInfo
                {
                    ProjectSuffix = ProjectSuffixes.Core,
                    TemplateName = this.GetViewModelTemplate(),
                    FileName = viewModelName + ".cs",
                };

                itemTemplateInfos.Add(viewModelTemplateInfo);
            }

            string viewName = viewModelName.Remove(viewModelName.Length - ViewModelSuffix.Length) + "View.cs";

            foreach (ItemTemplateInfo itemTemplateInfo in requiredUIViews)
            {
                itemTemplateInfo.TemplateName = this.GetViewTemplate(itemTemplateInfo.ProjectSuffix);
                itemTemplateInfo.FileName = viewName;
                itemTemplateInfos.Add(itemTemplateInfo);
            }

            //// do we require a Test ViewModel?

            if (solutionAlreadyCreated == false)
            {
                ItemTemplateInfo viewModelTemplateInfo = new ItemTemplateInfo
                {
                    ProjectSuffix = ProjectSuffixes.CoreTests,
                    TemplateName = this.GetTestViewModelTemplate(),
                    FileName = "Test" + viewModelName + ".cs",
                };

                itemTemplateInfos.Add(viewModelTemplateInfo);
            }

            return itemTemplateInfos;
        }

        /// <summary>
        /// Gets the view model template.
        /// </summary>
        /// <returns>The View Model Template.</returns>
        internal string GetViewModelTemplate()
        {
            return string.Format(
                "MvvmCross.ViewModel.{0}.zip",
                this.settingsService.SelectedViewType);
        }

        /// <summary>
        /// Gets the test view model template.
        /// </summary>
        /// <returns>The Test View Model Template.</returns>
        internal string GetTestViewModelTemplate()
        {
            return string.Format(
                "MvvmCross.{0}.TestViewModel.{1}.zip",
                this.settingsService.TestingFramework,
                this.settingsService.SelectedViewType);
        }

        /// <summary>
        /// Gets the view template.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>The View Template.</returns>
        internal string GetViewTemplate(string type)
        {
            return string.Format(
                "MvvmCross{0}.View.{1}.zip",
                type,
                this.settingsService.SelectedViewType);
        }

    }
}

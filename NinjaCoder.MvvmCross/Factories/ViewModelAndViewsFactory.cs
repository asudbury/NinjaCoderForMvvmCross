// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ViewModelAndViewsFactory type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Factories
{
    using Entities;

    using Interfaces;

    using Scorchio.Infrastructure.Extensions;
    using Scorchio.VisualStudio.Entities;
    using Services.Interfaces;
    using System.Collections.Generic;

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
                        FriendlyName = ProjectType.iOS.GetDescription(),
                        ProjectSuffix = ProjectSuffix.iOS.GetDescription(),
                        PreSelected = true
                    });
                }

                if (this.visualStudioService.DroidProjectService != null)
                {
                    itemTemplateInfos.Add(new ItemTemplateInfo
                    {
                        FriendlyName = ProjectType.Droid.GetDescription(),
                        ProjectSuffix = ProjectSuffix.Droid.GetDescription(),
                        PreSelected = true
                    });
                }

                if (this.visualStudioService.WindowsPhoneProjectService != null)
                {
                    itemTemplateInfos.Add(new ItemTemplateInfo
                    {
                        FriendlyName = ProjectType.WindowsPhone.GetDescription(),
                        ProjectSuffix = ProjectSuffix.WindowsPhone.GetDescription(),
                        PreSelected = true
                    });
                }

                if (this.visualStudioService.WindowsStoreProjectService != null)
                {
                    itemTemplateInfos.Add(new ItemTemplateInfo
                    {
                        FriendlyName = ProjectType.WindowsStore.GetDescription(),
                        ProjectSuffix = ProjectSuffix.WindowsStore.GetDescription(),
                        PreSelected = true
                    });
                }

                if (this.visualStudioService.WpfProjectService != null)
                {
                    itemTemplateInfos.Add(new ItemTemplateInfo
                    {
                        FriendlyName = ProjectType.WindowsWpf.GetDescription(),
                        ProjectSuffix = ProjectSuffix.Wpf.GetDescription(),
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
                "Blank", 
                "SampleData", 
                "Web"
            };
        }

        /// <summary>
        /// Gets the required templates.
        /// </summary>
        /// <param name="view">The view.</param>
        /// <param name="viewModelName">Name of the view model.</param>
        /// <param name="requiredUIViews">The required UI views.</param>
        /// <param name="unitTestsRequired">if set to <c>true</c> [unit tests required].</param>
        /// <returns>
        /// The required template.
        /// </returns>
        public IEnumerable<ItemTemplateInfo> GetRequiredViewModelAndViews(
            View view,
            string viewModelName,
            IEnumerable<ItemTemplateInfo> requiredUIViews,
            bool unitTestsRequired)
        {
            const string ViewModelSuffix = "ViewModel";

            List<ItemTemplateInfo> itemTemplateInfos = new List<ItemTemplateInfo>();

            //// first add the view model

            if (this.settingsService.FrameworkType == FrameworkType.MvvmCross)
            {
                ItemTemplateInfo viewModelTemplateInfo = new ItemTemplateInfo
                {
                    ProjectSuffix = ProjectSuffix.Core.GetDescription(),
                    TemplateName = this.GetViewModelTemplate(),
                    FileName = viewModelName + ".cs",
                };

                itemTemplateInfos.Add(viewModelTemplateInfo);

                string viewName = viewModelName.Remove(viewModelName.Length - ViewModelSuffix.Length) + "View.cs";

                foreach (ItemTemplateInfo itemTemplateInfo in requiredUIViews)
                {
                    itemTemplateInfo.TemplateName = this.GetViewTemplate(itemTemplateInfo.ProjectSuffix);
                    itemTemplateInfo.FileName = viewName;
                    itemTemplateInfos.Add(itemTemplateInfo);
                }                
            }
            
            else if (this.settingsService.FrameworkType == FrameworkType.XamarinForms)
            {
                ItemTemplateInfo viewModelTemplateInfo = new ItemTemplateInfo
                {
                    ProjectSuffix = ProjectSuffix.Core.GetDescription(),
                    TemplateName = this.GetViewModelTemplate(),
                    FileName = viewModelName + ".cs",
                };

                itemTemplateInfos.Add(viewModelTemplateInfo);

                string viewName = viewModelName.Remove(viewModelName.Length - ViewModelSuffix.Length) + "View.cs";

                ItemTemplateInfo viewTemplateInfo = new ItemTemplateInfo
                {
                    ProjectSuffix = ProjectSuffix.Forms.GetDescription(),
                    TemplateName = this.GetViewModelTemplate(),
                    FileName = viewName,
                };

                itemTemplateInfos.Add(viewTemplateInfo);
            }

            //// do we require a Test ViewModel?

            if (unitTestsRequired)
            {
                ItemTemplateInfo viewModelTemplateInfo = new ItemTemplateInfo
                                            {
                                                ProjectSuffix = ProjectSuffix.CoreTests.GetDescription(),
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

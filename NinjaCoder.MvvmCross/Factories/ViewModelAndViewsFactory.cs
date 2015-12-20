// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ViewModelAndViewsFactory type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Factories
{
    using Entities;
    using Interfaces;
    using NinjaCoder.MvvmCross.UserControls.AddViews;
    using NinjaCoder.MvvmCross.ViewModels.AddViews;
    using Scorchio.Infrastructure.Extensions;
    using Scorchio.Infrastructure.Wpf.ViewModels.Wizard;
    using Scorchio.VisualStudio.Entities;
    using Scorchio.VisualStudio.Services;
    using Services.Interfaces;
    using System.Collections.Generic;

    using ViewsFinishedControl = NinjaCoder.MvvmCross.UserControls.AddViews.ViewsFinishedControl;

    /// <summary>
    ///  Defines the ViewModelAndViewsFactory type.
    /// </summary>
    public class ViewModelAndViewsFactory : IViewModelAndViewsFactory
    {
        /// <summary>
        /// The view model suffix.
        /// </summary>
        private const string ViewModelSuffix = "ViewModel";

        /// <summary>
        /// The visual studio service.
        /// </summary>
        private readonly IVisualStudioService visualStudioService;

        /// <summary>
        /// The settings service.
        /// </summary>
        private readonly ISettingsService settingsService;

        /// <summary>
        /// The resolver service.
        /// </summary>
        private readonly IResolverService resolverService;

        /// <summary>
        /// The register service.
        /// </summary>
        private readonly IRegisterService registerService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewModelAndViewsFactory" /> class.
        /// </summary>
        /// <param name="visualStudioService">The visual studio service.</param>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="resolverService">The resolver service.</param>
        /// <param name="registerService">The register service.</param>
        public ViewModelAndViewsFactory(
            IVisualStudioService visualStudioService,
            ISettingsService settingsService,
            IResolverService resolverService,
            IRegisterService registerService)
        {
            this.visualStudioService = visualStudioService;
            this.settingsService = settingsService;
            this.resolverService = resolverService;
            this.registerService = registerService;
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
            TraceService.WriteLine("ViewModelAndViewsFactory::GetRequiredViewModelAndViews");

            List<ItemTemplateInfo> itemTemplateInfos = new List<ItemTemplateInfo>();

            //// first add the view model

            if (view.Framework == FrameworkType.MvvmCross.GetDescription())
            {
                string templateName = this.GetViewModelTemplate(FrameworkType.MvvmCross.GetValueFromDescription<FrameworkType>(view.Framework), view.PageType);

                if (templateName.EndsWith(Constants.Settings.T4Suffix) == false)
                {
                    ItemTemplateInfo viewModelTemplateInfo = new ItemTemplateInfo
                    {
                        ProjectSuffix = ProjectSuffix.Core.GetDescription(),
                        TemplateName = templateName,
                        FileName = viewModelName + ".cs"
                    };

                    itemTemplateInfos.Add(viewModelTemplateInfo);
                }

                string viewName = viewModelName.Remove(viewModelName.Length - ViewModelSuffix.Length) + "View.cs";

                foreach (ItemTemplateInfo itemTemplateInfo in requiredUIViews)
                {
                    itemTemplateInfo.TemplateName = this.GetViewTemplate(FrameworkType.MvvmCross.GetValueFromDescription<FrameworkType>(view.Framework), 
                        itemTemplateInfo.ProjectSuffix, 
                        view.PageType);

                    itemTemplateInfo.FileName = viewName;

                    itemTemplateInfos.Add(itemTemplateInfo);
                }
            }
            else if (view.Framework == FrameworkType.XamarinForms.GetDescription())
            {
                ItemTemplateInfo viewModelTemplateInfo = new ItemTemplateInfo
                {
                    ProjectSuffix = ProjectSuffix.Core.GetDescription(),
                    TemplateName = this.GetViewModelTemplate(FrameworkType.MvvmCross.GetValueFromDescription<FrameworkType>(view.Framework), view.PageType),
                    FileName = viewModelName + ".cs",
                };

                itemTemplateInfos.Add(viewModelTemplateInfo);

                string viewName = viewModelName.Remove(viewModelName.Length - ViewModelSuffix.Length) + "View.cs";

                ItemTemplateInfo viewTemplateInfo = new ItemTemplateInfo
                {
                    ProjectSuffix = ProjectSuffix.XamarinForms.GetDescription(),
                    TemplateName = this.GetViewTemplate(FrameworkType.MvvmCross.GetValueFromDescription<FrameworkType>(view.Framework), 
                    string.Empty, 
                        view.PageType),
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
                                                TemplateName = this.GetTestViewModelTemplate(FrameworkType.MvvmCross.GetValueFromDescription<FrameworkType>(view.Framework), view.PageType),
                                                FileName = "Test" + viewModelName + ".cs",
                                            };

                itemTemplateInfos.Add(viewModelTemplateInfo);
            }

            return itemTemplateInfos;
        }

        /// <summary>
        /// Gets the required text templates.
        /// </summary>
        /// <param name="view">The view.</param>
        /// <param name="viewModelName">Name of the view model.</param>
        /// <param name="requiredUIViews">The required UI views.</param>
        /// <param name="unitTestsRequired">if set to <c>true</c> [unit tests required].</param>
        /// <returns></returns>
        public IEnumerable<TextTemplateInfo> GetRequiredTextTemplates(
            View view,
            string viewModelName,
            IEnumerable<ItemTemplateInfo> requiredUIViews,
            bool unitTestsRequired)
        {
            TraceService.WriteLine("ViewModelAndViewsFactory::GetRequiredTextTemplates");

            List<TextTemplateInfo> textTemplateInfos = new List<TextTemplateInfo>();

            if (view.Framework == FrameworkType.MvvmCross.GetDescription())
            {
                string templateName = this.GetViewModelTemplate(FrameworkType.MvvmCross.GetValueFromDescription<FrameworkType>(view.Framework), view.PageType);

                if (templateName.EndsWith(Constants.Settings.T4Suffix))
                {
                    Dictionary<string, string> tokens = new Dictionary<string, string>
                        {
                            { "ClassName", viewModelName },
                            { "NameSpace",  this.visualStudioService.CoreProjectService.Name + ".ViewModels" }
                        };

                    TextTemplateInfo textTemplateInfo = new TextTemplateInfo
                                                            {
                                                                ProjectSuffix = ProjectSuffix.Core.GetDescription(),
                                                                FileName = viewModelName + ".cs",
                                                                ProjectFolder = "ViewModels",
                                                                Tokens = tokens,
                                                                ShortTemplateName = templateName,
                                                                TemplateName = this.settingsService.ItemTemplatesDirectory + "\\" + templateName
                                                            };

                    textTemplateInfo.TextOutput = this.visualStudioService.GetTextTransformationService().Transform(
                                                                this.settingsService.UseSimpleTextTemplatingEngine,
                                                                textTemplateInfo.TemplateName,
                                                                textTemplateInfo.Tokens);

                    textTemplateInfos.Add(textTemplateInfo);
                }
            }

            //// do we require a Test ViewModel?

            if (unitTestsRequired)
            {
                Dictionary<string, string> tokens = new Dictionary<string, string>
                        {
                            { "ClassName", viewModelName },
                            { "NameSpace",  this.visualStudioService.CoreTestsProjectService.Name + ".ViewModels" },
                            { "TestingLibrary",  string.Empty },
                            { "TestingClassAttribute",  string.Empty },
                            { "TestingMethodAttribute",  string.Empty },
                            { "TestableObject",  string.Empty },
                            { "TestableObjectInstance",  string.Empty }
                        };

                string templateName = this.GetTestViewModelTemplate(FrameworkType.MvvmCross.GetValueFromDescription<FrameworkType>(view.Framework), view.PageType);

                TextTemplateInfo textTemplateInfo = new TextTemplateInfo
                {
                    ProjectSuffix = ProjectSuffix.Core.GetDescription(),
                    FileName = "Test" + viewModelName + ".cs",
                    ProjectFolder = "ViewModels",
                    Tokens = tokens,
                    ShortTemplateName = templateName,
                    TemplateName = this.settingsService.ItemTemplatesDirectory + "\\" + templateName
                };

                textTemplateInfo.TextOutput = this.visualStudioService.GetTextTransformationService().Transform(
                                                            this.settingsService.UseSimpleTextTemplatingEngine,
                                                            textTemplateInfo.TemplateName,
                                                            textTemplateInfo.Tokens);

                textTemplateInfos.Add(textTemplateInfo);
            }

            return textTemplateInfos;
        }

        /// <summary>
        /// Gets the wizards steps.
        /// </summary>
        /// <returns>
        /// The wizard steps.
        /// </returns>
        public List<WizardStepViewModel> GetWizardsSteps()
        {
            List<WizardStepViewModel> wizardSteps = new List<WizardStepViewModel>
            {
                new WizardStepViewModel
                {
                    ViewModel = this.resolverService.Resolve<ViewsViewModel>(),
                    ViewType = typeof(ViewsControl)
                },
                new WizardStepViewModel
                {
                    ViewModel = this.resolverService.Resolve<ViewsFinishedViewModel>(),
                    ViewType = typeof(ViewsFinishedControl)
                }
            };

            return wizardSteps;
        }

        /// <summary>
        /// Registers the wizard data.
        /// </summary>
        public void RegisterWizardData()
        {
            WizardData wizardData = new WizardData
            {
                WindowTitle = "Add ViewModel and Views",
                WindowHeight = 600,
                WindowWidth = 680,
                WizardSteps = this.GetWizardsSteps()
            };

            this.registerService.Register<IWizardData>(wizardData);
        }

        /// <summary>
        /// Gets the view model template.
        /// </summary>
        /// <param name="frameworkType">Type of the framework.</param>
        /// <param name="pageType">Type of the page.</param>
        /// <returns>
        /// The View Model Template.
        /// </returns>
        internal string GetViewModelTemplate(
            FrameworkType frameworkType,
            string pageType)
        {
            if (frameworkType == FrameworkType.XamarinForms)
            {
                return "XamarinForms.ViewModel.Blank.zip";
            }

            switch (pageType)
            {
                case "Blank":
                    return "BlankViewModel.t4";

                case "SampleData":
                    return "MvxSampleDataViewModel.t4";

                case "Web":
                    return "WebViewModel.t4";
            }
            
            return string.Format(
                "MvvmCross.ViewModel.{0}.zip",
                pageType);
        }

        /// <summary>
        /// Gets the test view model template.
        /// </summary>
        /// <param name="frameworkType">Type of the framework.</param>
        /// <param name="pageType">Type of the page.</param>
        /// <returns>
        /// The Test View Model Template.
        /// </returns>
        internal string GetTestViewModelTemplate(
            FrameworkType frameworkType,
            string pageType)
        {
            if (frameworkType == FrameworkType.XamarinForms)
            {
                return string.Format(
                    "MvvmCross.{0}.TestViewModel.Blank.zip",
                    this.settingsService.TestingFramework);
            }

            switch (pageType)
            {
                case "Blank":
                    return "TestBlankViewModel.t4";

                case "SampleData":
                    return "TestMvxSampleDataViewModel.t4";

                case "Web":
                    return "TestWebViewModel.t4";
            }

            return string.Format(
                "MvvmCross.{0}.TestViewModel.{1}.zip",
                this.settingsService.TestingFramework,
                pageType);
        }

        /// <summary>
        /// Gets the view template.
        /// </summary>
        /// <param name="frameworkType">Type of the framework.</param>
        /// <param name="type">The type.</param>
        /// <param name="pageType">Type of the page.</param>
        /// <returns>
        /// The View Template.
        /// </returns>
        internal string GetViewTemplate(
            FrameworkType frameworkType,
            string type,
            string pageType)
        {
            if (frameworkType == FrameworkType.XamarinForms)
            {
                return "XamarinForms.View.Blank.zip";
            }

            return string.Format(
                "MvvmCross{0}.View.{1}.zip",
                type,
                pageType);
        }
    }
}

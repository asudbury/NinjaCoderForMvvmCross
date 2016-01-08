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
    using Scorchio.Infrastructure.Constants;
    using Scorchio.Infrastructure.Extensions;
    using Scorchio.Infrastructure.Wpf.ViewModels.Wizard;
    using Scorchio.VisualStudio.Entities;
    using Scorchio.VisualStudio.Services;
    using Services.Interfaces;
    using System;
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
        /// The blank view type.
        /// </summary>
        private const string BlankViewType = "Blank";

        /// <summary>
        /// The sample data view type.
        /// </summary>
        private const string SampleDataViewType = "SampleData";

        /// <summary>
        /// The web view type.
        /// </summary>
        private const string WebViewType = "Web";

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
                    itemTemplateInfos.Add(this.GetView(ProjectType.iOS.GetDescription(), ProjectSuffix.iOS.GetDescription()));
                }

                if (this.visualStudioService.DroidProjectService != null)
                {
                    itemTemplateInfos.Add(this.GetView(ProjectType.Droid.GetDescription(), ProjectSuffix.Droid.GetDescription()));
                }

                if (this.visualStudioService.WindowsPhoneProjectService != null)
                {
                    itemTemplateInfos.Add(this.GetView(ProjectType.WindowsPhone.GetDescription(), ProjectSuffix.WindowsPhone.GetDescription()));
                }

                if (this.visualStudioService.WpfProjectService != null)
                {
                    itemTemplateInfos.Add(this.GetView(ProjectType.WindowsWpf.GetDescription(), ProjectSuffix.Wpf.GetDescription()));
                }

                return itemTemplateInfos;
            }
        }

        /// <summary>
        /// Gets the view.
        /// </summary>
        /// <param name="friendlyName">Name of the friendly.</param>
        /// <param name="projectSuffix">The project suffix.</param>
        /// <returns></returns>
        public ItemTemplateInfo GetView(
            string friendlyName,
            string projectSuffix)
        {
            return new ItemTemplateInfo
                       {
                           FriendlyName = friendlyName,
                           ProjectSuffix = projectSuffix,
                           PreSelected = true
                       };
        }

        /// <summary>
        /// Gets the available views.
        /// </summary>
        /// <returns>The list of available views.</returns>
        public IEnumerable<string> GetAvailableViewTypes()
        {
            return new List<string>
            {
                BlankViewType, 
                SampleDataViewType, 
                WebViewType
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
                    string viewTemplateName = this.GetViewTemplate(FrameworkType.MvvmCross.GetValueFromDescription<FrameworkType>(view.Framework),
                                                                itemTemplateInfo.ProjectSuffix,
                                                                 view.PageType);

                     if (viewTemplateName.EndsWith(Constants.Settings.T4Suffix))
                     {
                        itemTemplateInfo.TemplateName = viewTemplateName;

                        itemTemplateInfo.FileName = viewName;

                        itemTemplateInfos.Add(itemTemplateInfo);
                    }
                }
            }
            else if (view.Framework == FrameworkType.XamarinForms.GetDescription())
            {
                string templateName = this.GetViewModelTemplate(FrameworkType.MvvmCross.GetValueFromDescription<FrameworkType>(view.Framework), view.PageType);

                if (templateName.EndsWith(Constants.Settings.T4Suffix) == false)
                {
                    ItemTemplateInfo viewModelTemplateInfo = new ItemTemplateInfo
                    {
                        ProjectSuffix = ProjectSuffix.Core.GetDescription(),
                        TemplateName = templateName,
                        FileName = viewModelName + ".cs",
                    };

                    itemTemplateInfos.Add(viewModelTemplateInfo);
                }

                string viewTemplateName = this.GetViewTemplate(FrameworkType.MvvmCross.GetValueFromDescription<FrameworkType>(view.Framework), string.Empty, view.PageType);

                if (viewTemplateName.EndsWith(Constants.Settings.T4Suffix) == false)
                {
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
            }

            //// do we require a Test ViewModel?

            if (unitTestsRequired)
            {
                string templateName = this.GetTestViewModelTemplate(FrameworkType.MvvmCross.GetValueFromDescription<FrameworkType>(view.Framework), view.PageType);

                if (templateName.EndsWith(Constants.Settings.T4Suffix) == false)
                {
                    ItemTemplateInfo viewModelTemplateInfo = new ItemTemplateInfo
                                            {
                                                ProjectSuffix = ProjectSuffix.CoreTests.GetDescription(),
                                                TemplateName = templateName,
                                                FileName = "Test" + viewModelName + ".cs",
                                            };

                    itemTemplateInfos.Add(viewModelTemplateInfo);
                }
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

            if (view.Framework == FrameworkType.MvvmCross.GetDescription() ||
                view.Framework == FrameworkType.XamarinForms.GetDescription())
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
                                                                TemplateName = this.settingsService.ItemTemplatesDirectory + "\\ViewModels\\" + templateName
                                                            };

                    textTemplateInfo.TextOutput = this.visualStudioService.GetTextTransformationService().Transform(
                                                                textTemplateInfo.TemplateName,
                                                                textTemplateInfo.Tokens);

                    textTemplateInfos.Add(textTemplateInfo);
                }

                if (view.Framework == FrameworkType.MvvmCross.GetDescription())
                {
                    string viewName = viewModelName.Remove(viewModelName.Length - ViewModelSuffix.Length) + "View.cs";

                    foreach (ItemTemplateInfo itemTemplateInfo in requiredUIViews)
                    {
                        string viewTemplateName = this.GetViewTemplate(FrameworkType.MvvmCross.GetValueFromDescription<FrameworkType>(view.Framework),
                                                        itemTemplateInfo.ProjectSuffix,
                                                        view.PageType);

                        if (templateName.EndsWith(Constants.Settings.T4Suffix))
                        {
                            Dictionary<string, string> tokens = new Dictionary<string, string>
                            {
                                { "ClassName", viewName },
                                { "NameSpace",  this.visualStudioService.GetProjectServiceBySuffix(itemTemplateInfo.ProjectSuffix).Name + ".Views"}
                            };
                        
                            TextTemplateInfo textTemplateInfo = new TextTemplateInfo
                            {
                                ProjectSuffix = itemTemplateInfo.ProjectSuffix,
                                FileName = viewName + ".cs",
                                ProjectFolder = "Views",
                                Tokens = tokens,
                                ShortTemplateName = viewTemplateName,
                                TemplateName = this.settingsService.ItemTemplatesDirectory + "\\" + viewTemplateName
                            };

                            textTemplateInfo.TextOutput = this.visualStudioService.GetTextTransformationService().Transform(
                                                                        textTemplateInfo.TemplateName,
                                                                        textTemplateInfo.Tokens);

                            textTemplateInfos.Add(textTemplateInfo);
                        }
                    }
                }
                else if (view.Framework == FrameworkType.XamarinForms.GetDescription())
                {
                    string viewTemplateName = this.GetViewTemplate(FrameworkType.MvvmCross.GetValueFromDescription<FrameworkType>(view.Framework), string.Empty, view.PageType);

                    if (viewTemplateName.EndsWith(Constants.Settings.T4Suffix))
                    {
                        string viewName = viewModelName.Remove(viewModelName.Length - ViewModelSuffix.Length) + "View";
                        string pageType = view.PageType.Replace(" ", string.Empty);

                        Dictionary<string, string> tokens = new Dictionary<string, string>
                            {
                                { "ClassName", viewName },
                                { "CoreProjectName", this.visualStudioService.CoreProjectService.Name },
                                { "NameSpace",  this.visualStudioService.XamarinFormsProjectService.Name + ".Views" },
                                { "PageType", pageType },
                                { "Content", this.GetFormsViewContent(view)}
                            };
                        
                       TextTemplateInfo textTemplateInfo = new TextTemplateInfo
                       {
                           ProjectSuffix = ProjectSuffix.XamarinForms.GetDescription(),
                           FileName = viewName + ".xaml",
                           ProjectFolder = "Views",
                           Tokens = tokens,
                           ShortTemplateName = viewTemplateName,
                           TemplateName = this.settingsService.ItemTemplatesDirectory + "\\XamarinForms\\" + viewTemplateName
                       };

                       textTemplateInfo.TextOutput = this.visualStudioService.GetTextTransformationService().Transform(
                                                                   textTemplateInfo.TemplateName,
                                                                   textTemplateInfo.Tokens);

                       textTemplateInfos.Add(textTemplateInfo);

                       //// we also need to add a build action for the xaml file!

                        textTemplateInfo.FileOperations = new List<FileOperation>
                                                              {
                                                                  new FileOperation
                                                                      {
                                                                          PlatForm = ProjectSuffix.XamarinForms.GetDescription(),
                                                                          CommandType = "Properties",
                                                                          Directory = "Views",
                                                                          File = textTemplateInfo.FileName,
                                                                          From = "BuildAction",
                                                                          To = "3"
                                                                      }
                                                              };
                        
                        TextTemplateInfo childtextTemplateInfo = new TextTemplateInfo
                       {
                           ProjectSuffix = ProjectSuffix.XamarinForms.GetDescription(),
                           FileName = viewName + ".xaml.cs",
                           ProjectFolder = "Views",
                           Tokens = tokens,
                           ShortTemplateName = viewTemplateName,
                           TemplateName = this.settingsService.ItemTemplatesDirectory + "\\" + "BlankViewCodeBehind.t4"
                       };

                       childtextTemplateInfo.TextOutput = this.visualStudioService.GetTextTransformationService().Transform(
                                                                   textTemplateInfo.TemplateName,
                                                                   textTemplateInfo.Tokens);

                        textTemplateInfo.ChildItem = childtextTemplateInfo;
                    }
                }
            }

            //// do we require a Test ViewModel?

            if (unitTestsRequired)
            {
                string templateName = this.GetTestViewModelTemplate(FrameworkType.MvvmCross.GetValueFromDescription<FrameworkType>(view.Framework), view.PageType);

                if (templateName.EndsWith(Constants.Settings.T4Suffix))
                {
                    Dictionary<string, string> tokens = new Dictionary<string, string>
                        {
                            { "ClassName", "Test" + viewModelName },
                            { "NameSpace",  this.visualStudioService.CoreTestsProjectService.Name + ".ViewModels" },
                            { "TestingLibrary",  this.GetTestingLibrary() },
                            { "TestingClassAttribute",  this.GetTestingClassAttribute() },
                            { "TestingMethodAttribute",  this.GetTestingMethodAttribute() },
                            { "TestableObject",  viewModelName },
                            { "TestableObjectInstance",  viewModelName.LowerCaseFirstCharacter() }
                        };

                    TextTemplateInfo textTemplateInfo = new TextTemplateInfo
                    {
                        ProjectSuffix = ProjectSuffix.CoreTests.GetDescription(),
                        FileName = "Test" + viewModelName + ".cs",
                        ProjectFolder = "ViewModels",
                        Tokens = tokens,
                        ShortTemplateName = templateName,
                        TemplateName = this.settingsService.ItemTemplatesDirectory + "\\TestViewModels\\" + templateName
                    };

                    textTemplateInfo.TextOutput = this.visualStudioService.GetTextTransformationService().Transform(
                                                                textTemplateInfo.TemplateName,
                                                                textTemplateInfo.Tokens);

                    textTemplateInfos.Add(textTemplateInfo);
                }
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
            switch (pageType)
            {
                case SampleDataViewType:

                    if (frameworkType == FrameworkType.XamarinForms)
                    {
                        return "SampleDataViewModel.t4";
                    }

                    return "MvxSampleDataViewModel.t4";

                case WebViewType:
                    return "WebViewModel.t4";

                default:
                    return "BlankViewModel.t4";
            }
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
            switch (pageType)
            {
                case SampleDataViewType:
                    return frameworkType == FrameworkType.XamarinForms ? "TestSampleDataViewModel.t4" : "TestMvxSampleDataViewModel.t4";

                case WebViewType:
                    return "TestWebViewModel.t4";

                default:
                    return "TestBlankViewModel.t4";
            }
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
                return "XamarinFormsBlankView.t4";
                ////return "XamarinForms.View.Blank.zip";
            }

            return string.Format(
                "MvvmCross{0}.View.{1}.zip",
                type,
                pageType);
        }

        /// <summary>
        /// Gets the testing class attribute.
        /// </summary>
        /// <returns></returns>
        internal string GetTestingClassAttribute()
        {
            switch (this.settingsService.TestingFramework)
            {
                case TestingConstants.MsTest.Name:
                    return TestingConstants.MsTest.ClassAttribute;

                case TestingConstants.XUnit.Name:
                    return TestingConstants.XUnit.ClassAttribute;

                default:
                    return TestingConstants.NUnit.ClassAttribute;
            }
        }

        /// <summary>
        /// Gets the testing method attribute.
        /// </summary>
        /// <returns></returns>
        internal string GetTestingMethodAttribute()
        {
            switch (this.settingsService.TestingFramework)
            {
                case TestingConstants.MsTest.Name:
                    return TestingConstants.MsTest.MethodAttribute;

                case TestingConstants.XUnit.Name:
                    return TestingConstants.XUnit.MethodAttribute;

                default:
                    return TestingConstants.NUnit.MethodAttribute;
            }
        }

        /// <summary>
        /// Gets the testing library.
        /// </summary>
        /// <returns></returns>
        internal string GetTestingLibrary()
        {
            switch (this.settingsService.TestingFramework)
            {
                case TestingConstants.MsTest.Name:
                    return TestingConstants.MsTest.Library;

                case TestingConstants.XUnit.Name:
                    return TestingConstants.XUnit.Library;

                default:
                    return TestingConstants.NUnit.Library;
            }
        }

        /// <summary>
        /// Updates the file.
        /// </summary>
        /// <param name="view">The view.</param>
        internal string GetFormsViewContent(View view)
        {
            TraceService.WriteLine("ViewModelAndViewsFactory::GetFormsViewContent");

            string page = view.PageType.Replace(" ", string.Empty);
            string layout = view.LayoutType.Replace(" ", string.Empty);

            string bindingContext = string.Empty;
            string binding = string.Empty;

            if (this.settingsService.BindContextInXamlForXamarinForms)
            {
                bindingContext = string.Format("<{0}.BindingContext>{1}        <viewModels:{2} />{1}        </{0}.BindingContext>{1}",
                                                page,
                                                Environment.NewLine,
                                                view.Name + "ViewModel");
            }

            if (this.settingsService.BindXamlForXamarinForms)
            {
                binding = "<Label Text='{Binding SampleText}' VerticalOptions='Center' HorizontalOptions='Center'/>";
            }

            string content = string.Format("{1}<{0}>{2}</{0}>", layout, bindingContext, binding);

            return content;
        }
    }
}

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
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Scorchio.VisualStudio.Extensions;

    using ViewsFinishedControl = NinjaCoder.MvvmCross.UserControls.AddViews.ViewsFinishedControl;

    /// <summary>
    ///  Defines the ViewModelAndViewsFactory type.
    /// </summary>
    public class ViewModelAndViewsFactory : BaseViewFactory, IViewModelAndViewsFactory
    {
        /// <summary>
        /// The resolver service.
        /// </summary>
        private readonly IResolverService resolverService;

        /// <summary>
        /// The register service.
        /// </summary>
        private readonly IRegisterService registerService;

        /// <summary>
        /// The testing framework factory.
        /// </summary>
        private readonly ITestingFrameworkFactory testingFrameworkFactory;

        /// <summary>
        /// The MVVM cross view factory.
        /// </summary>
        private readonly IMvvmCrossViewFactory mvvmCrossViewFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewModelAndViewsFactory" /> class.
        /// </summary>
        /// <param name="visualStudioService">The visual studio service.</param>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="resolverService">The resolver service.</param>
        /// <param name="registerService">The register service.</param>
        /// <param name="testingFrameworkFactory">The testing framework factory.</param>
        /// <param name="mvvmCrossViewFactory">The MVVM cross view factory.</param>
        public ViewModelAndViewsFactory(
            IVisualStudioService visualStudioService,
            ISettingsService settingsService,
            IResolverService resolverService,
            IRegisterService registerService,
            ITestingFrameworkFactory testingFrameworkFactory,
            IMvvmCrossViewFactory mvvmCrossViewFactory)
            :base(
            settingsService, 
            visualStudioService)
        {
            this.resolverService = resolverService;
            this.registerService = registerService;
            this.testingFrameworkFactory = testingFrameworkFactory;
            this.mvvmCrossViewFactory = mvvmCrossViewFactory;
        }

        /// <summary>
        /// Gets the allowed ui views.
        /// </summary>
        public IEnumerable<ItemTemplateInfo> AllowedUIViews
        {
            get
            {
                List<ItemTemplateInfo> itemTemplateInfos = new List<ItemTemplateInfo>();

                if (this.VisualStudioService.iOSProjectService != null)
                {
                    itemTemplateInfos.Add(this.GetView(ProjectType.iOS.GetDescription(), ProjectSuffix.iOS.GetDescription()));
                }

                if (this.VisualStudioService.DroidProjectService != null)
                {
                    itemTemplateInfos.Add(this.GetView(ProjectType.Droid.GetDescription(), ProjectSuffix.Droid.GetDescription()));
                }

                if (this.VisualStudioService.WindowsPhoneProjectService != null)
                {
                    itemTemplateInfos.Add(this.GetView(ProjectType.WindowsPhone.GetDescription(), ProjectSuffix.WindowsPhone.GetDescription()));
                }

                if (this.VisualStudioService.WpfProjectService != null)
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
        public ItemTemplateInfo GetView(string friendlyName, string projectSuffix)
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
            return (from ViewType enumValue in Enum.GetValues(typeof(ViewType))
                    select enumValue.GetDescription()).ToList();
        }

        /// <summary>
        /// Gets the available MVVM cross ios view types.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> GetAvailableMvvmCrossiOSViewTypes()
        {
            return (from MvvmCrossSampleViewType enumValue in Enum.GetValues(typeof(MvvmCrossSampleViewType)) 
                                   select enumValue.GetDescription()).ToList();
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
                textTemplateInfos.Add(this.GetViewModelTextTemplateInfo(view, viewModelName));

                if (view.Framework == FrameworkType.MvvmCross.GetDescription())
                {
                    foreach (ItemTemplateInfo itemTemplateInfo in requiredUIViews)
                    {
                        TextTemplateInfo textTemplateInfo = this.mvvmCrossViewFactory.GetMvvmCrossView(viewModelName, itemTemplateInfo, view);

                        textTemplateInfos.Add(textTemplateInfo);
                    }
                }
                else if (view.Framework == FrameworkType.XamarinForms.GetDescription())
                {
                    TextTemplateInfo textTemplateInfo = this.GetXamarinFormsView(viewModelName, view);

                    textTemplateInfos.Add(textTemplateInfo);
                }
            }

            //// do we require a Test ViewModel?

            if (unitTestsRequired)
            {
                textTemplateInfos.Add(this.GetUnitTestingItemTemplate(view, viewModelName));
            }

            return textTemplateInfos;
        }

        /// <summary>
        /// Gets the xamarin forms view.
        /// </summary>
        /// <param name="viewModelName">Name of the view model.</param>
        /// <param name="view">The view.</param>
        /// <returns></returns>
        internal TextTemplateInfo GetXamarinFormsView(string viewModelName, View view)
        {
            string viewTemplateName =
                this.GetViewTemplate(
                    FrameworkType.MvvmCross.GetValueFromDescription<FrameworkType>(view.Framework),
                    string.Empty,
                    view.PageType);

            string viewName = viewModelName.Remove(viewModelName.Length - ViewModelSuffix.Length) + "View";
            string pageType = view.PageType.Replace(" ", string.Empty);

            Dictionary<string, string> tokens = new Dictionary<string, string>
            {
                { "ClassName", viewName },
                { "CoreProjectName", this.VisualStudioService.CoreProjectService.Name },
                { "NameSpace", this.VisualStudioService.XamarinFormsProjectService.Name+ ".Views" },
                { "PageType", pageType },
                { "CompileOption", this.SettingsService.UseXamarinFormsXamlCompilation ? XamarinFormsCompileOption.Compile.GetDescription() : XamarinFormsCompileOption.Skip.GetDescription() },
                { "Content", this.GetFormsViewContent(view)
                }
            };

            TextTemplateInfo textTemplateInfo = new TextTemplateInfo
                {
                    ProjectSuffix = ProjectSuffix.XamarinForms.GetDescription(),
                    ProjectFolder = "Views",
                    Tokens = tokens,
                    ShortTemplateName = viewTemplateName,
                    TemplateName = this.SettingsService.ItemTemplatesDirectory + "\\XamarinForms\\" + viewTemplateName
                };

            TextTransformationRequest textTransformationRequest = new TextTransformationRequest
            {
                SourceFile = textTemplateInfo.TemplateName,
                Parameters = textTemplateInfo.Tokens,
                RemoveFileHeaders = this.SettingsService.RemoveDefaultFileHeaders,
                RemoveXmlComments = this.SettingsService.RemoveDefaultComments,
                RemoveThisPointer = this.SettingsService.RemoveThisPointer
            };

            TextTransformation textTransformation = this.GetTextTransformationService().Transform(textTransformationRequest);

            textTemplateInfo.TextOutput = textTransformation.Output;
            textTemplateInfo.FileName = viewName + "." + textTransformation.FileExtension;

            //// we also need to add a build action for the xaml file!

            textTemplateInfo.FileOperations.Add(
                this.GetEmbeddedResourceFileOperation(
                    ProjectSuffix.XamarinForms.GetDescription(),
                    textTemplateInfo.FileName));

            //// add code behind file

            TextTemplateInfo childTextTemplateInfo =
                this.GetCodeBehindTextTemplateInfo(
                    ProjectSuffix.XamarinForms.GetDescription(),
                    viewName,
                    viewTemplateName,
                    tokens);

            textTemplateInfo.ChildItems.Add(childTextTemplateInfo);

            return textTemplateInfo;
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
        /// Gets the view model text template information.
        /// </summary>
        /// <param name="view">The view.</param>
        /// <param name="viewModelName">Name of the view model.</param>
        /// <returns></returns>
        internal TextTemplateInfo GetViewModelTextTemplateInfo(
            View view, 
            string viewModelName)
        {
            string templateName = this.GetViewModelTemplate(FrameworkType.MvvmCross.GetValueFromDescription<FrameworkType>(view.Framework), view.PageType);

            Dictionary<string, string> tokens = new Dictionary<string, string>
            {
                { "ClassName", viewModelName },
                { "NameSpace",  this.VisualStudioService.CoreProjectService.Name+ ".ViewModels" }
            };

            TextTemplateInfo textTemplateInfo = new TextTemplateInfo
            {
                ProjectSuffix = ProjectSuffix.Core.GetDescription(),
                FileName = viewModelName + ".cs",
                ProjectFolder = "ViewModels",
                Tokens = tokens,
                ShortTemplateName = templateName,
                TemplateName = this.SettingsService.ItemTemplatesDirectory + "\\ViewModels\\" + templateName
            };

            TextTransformationRequest textTransformationRequest = new TextTransformationRequest
            {
                SourceFile = textTemplateInfo.TemplateName,
                Parameters = textTemplateInfo.Tokens,
                RemoveFileHeaders = this.SettingsService.RemoveDefaultFileHeaders,
                RemoveXmlComments = this.SettingsService.RemoveDefaultComments,
                RemoveThisPointer = this.SettingsService.RemoveThisPointer
            };

            textTemplateInfo.TextOutput = this.GetTextTransformationService().Transform(textTransformationRequest).Output;

            return textTemplateInfo;
        }

        /// <summary>
        /// Gets the unit testing item template.
        /// </summary>
        /// <param name="view">The view.</param>
        /// <param name="viewModelName">Name of the view model.</param>
        /// <returns></returns>
        internal TextTemplateInfo GetUnitTestingItemTemplate(
            View view,
            string viewModelName)
        {
            string templateName = this.GetTestViewModelTemplate(FrameworkType.MvvmCross.GetValueFromDescription<FrameworkType>(view.Framework), view.PageType);

            Dictionary<string, string> tokens = new Dictionary<string, string>
            {
                { "ClassName", "Test" + viewModelName },
                { "NameSpace", this.VisualStudioService.CoreTestsProjectService.Name + ".ViewModels" },
                { "TestingLibrary", this.testingFrameworkFactory.GetTestingLibrary() },
                { "TestingClassAttribute", this.testingFrameworkFactory.GetTestingClassAttribute() },
                { "TestingMethodAttribute", this.testingFrameworkFactory.GetTestingMethodAttribute() },
                { "TestableObject", viewModelName },
                { "TestableObjectInstance", viewModelName.LowerCaseFirstCharacter() }
            };

            TextTemplateInfo textTemplateInfo = new TextTemplateInfo
            {
                ProjectSuffix = ProjectSuffix.CoreTests.GetDescription(),
                FileName = "Test" + viewModelName + ".cs",
                ProjectFolder = "ViewModels",
                Tokens = tokens,
                ShortTemplateName = templateName,
                TemplateName = this.SettingsService.ItemTemplatesDirectory + "\\TestViewModels\\" + templateName
            };
            
            TextTransformationRequest textTransformationRequest = new TextTransformationRequest
            {
                SourceFile = textTemplateInfo.TemplateName,
                Parameters = textTemplateInfo.Tokens,
                RemoveFileHeaders = this.SettingsService.RemoveDefaultFileHeaders,
                RemoveXmlComments = this.SettingsService.RemoveDefaultComments,
                RemoveThisPointer = this.SettingsService.RemoveThisPointer
            };

            textTemplateInfo.TextOutput = this.GetTextTransformationService().Transform(textTransformationRequest).Output;

            return textTemplateInfo;
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
            if (pageType == ViewType.SampleData.GetDescription())
            {
                return frameworkType == FrameworkType.XamarinForms ? "SampleDataViewModel.t4" : "MvxSampleDataViewModel.t4";
            }

            return pageType == ViewType.Web.GetDescription() ? "WebViewModel.t4" : "BlankViewModel.t4";
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
            if (pageType == ViewType.SampleData.GetDescription())
            {
                return frameworkType == FrameworkType.XamarinForms ? "TestSampleDataViewModel.t4" : "TestMvxSampleDataViewModel.t4";
            }

            return pageType == ViewType.Web.GetDescription() ? "TestWebViewModel.t4" : "TestBlankViewModel.t4";
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

            if (this.SettingsService.BindContextInXamlForXamarinForms)
            {
                bindingContext = string.Format("<{0}.BindingContext>{1}        <viewModels:{2} />{1}        </{0}.BindingContext>{1}",
                                                page,
                                                Environment.NewLine,
                                                view.Name + "ViewModel");
            }

            if (this.SettingsService.BindXamlForXamarinForms)
            {
                binding = "<Label Text='{Binding SampleText}' VerticalOptions='Center' HorizontalOptions='Center'/>";
            }

            string content = string.Format("{1}<{0}>{2}</{0}>", layout, bindingContext, binding);

            return content;
        }
    }
}

// --------------------------------------------------------------------------------------------------------------------
// <summary>
// 	Defines the DependencyServicesFactory type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Factories
{
    using NinjaCoder.MvvmCross.Entities;
    using NinjaCoder.MvvmCross.Factories.Interfaces;
    using NinjaCoder.MvvmCross.Services.Interfaces;
    using NinjaCoder.MvvmCross.UserControls.AddCustomerRenderers;
    using NinjaCoder.MvvmCross.ViewModels.AddCustomRenderers;
    using Scorchio.Infrastructure.Wpf.ViewModels.Wizard;
    using Scorchio.VisualStudio.Entities;
    using Scorchio.VisualStudio.Services;
    using Scorchio.VisualStudio.Services.Interfaces;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the DependencyServicesFactory type.
    /// </summary>
    public class CustomRendererFactory : BaseTextTemplateFactory, ICustomRendererFactory
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
        /// The resolver service.
        /// </summary>
        private readonly IResolverService resolverService;

        /// <summary>
        /// The register service.
        /// </summary>
        private readonly IRegisterService registerService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomRendererFactory" /> class.
        /// </summary>
        /// <param name="visualStudioService">The visual studio service.</param>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="resolverService">The resolver service.</param>
        /// <param name="registerService">The register service.</param>
        public CustomRendererFactory(
            IVisualStudioService visualStudioService,
            ISettingsService settingsService,
            IResolverService resolverService,
            IRegisterService registerService)
        {
            TraceService.WriteLine("CustomRendererFactory::Constructor");

            this.visualStudioService = visualStudioService;
            this.settingsService = settingsService;
            this.resolverService = resolverService;
            this.registerService = registerService;
        }

        /// <summary>
        /// Gets the wizards steps.
        /// </summary>
        public List<WizardStepViewModel> GetWizardsSteps()
        {
            List<WizardStepViewModel> wizardSteps = new List<WizardStepViewModel>
            {
                new WizardStepViewModel
                {
                    ViewModel = this.resolverService.Resolve<CustomRendererViewModel>(),
                    ViewType = typeof(CustomRendererControl)
                },
                                new WizardStepViewModel
                    {
                        ViewModel = this.resolverService.Resolve<CustomRendererFinishedViewModel>(),
                        ViewType =  typeof(CustomRendereFinishedControl)
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
                WindowTitle = "Add Custom Renderer",
                WindowHeight = 600,
                WindowWidth = 600,
                WizardSteps = this.GetWizardsSteps()
            };

            this.registerService.Register<IWizardData>(wizardData);
        }

        /// <summary>
        /// Gets the text templates.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="directory">The directory.</param>
        /// <returns></returns>
        public IEnumerable<TextTemplateInfo> GetTextTemplates(
            string name,
            string directory)
        {
            TraceService.WriteLine("CustomRendererFactory::GetTextTemplates");

            List<TextTemplateInfo> textTemplates = new List<TextTemplateInfo>();

            Dictionary<string, string> baseDictionary = this.GetBaseDictionary(
                name,
                directory);

            IProjectService formsProjectService = this.visualStudioService.XamarinFormsProjectService;

            if (formsProjectService != null)
            {
                TraceService.WriteLine("building formsProject textTemplate");

                textTemplates.Add(
                    this.GetTextTemplateInfo(
                        formsProjectService,
                        this.settingsService.BaseCustomRendererTextTemplate,
                        name,
                        directory,
                        ProjectSuffix.XamarinForms,
                        baseDictionary));
            }

            IProjectService iOSProjectService = this.visualStudioService.iOSProjectService;

            if (iOSProjectService != null)
            {
                TraceService.WriteLine("building iosProject textTemplate");

                textTemplates.Add(
                    this.GetTextTemplateInfo(
                        iOSProjectService,
                        this.settingsService.CustomRendererTextTemplate,
                        name,
                        directory,
                        ProjectSuffix.iOS,
                        baseDictionary));
            }

            IProjectService windowsPhoneProjectService = this.visualStudioService.WindowsPhoneProjectService;

            if (windowsPhoneProjectService != null)
            {
                TraceService.WriteLine("building windowsPhoneProject textTemplate");

                textTemplates.Add(
                    this.GetTextTemplateInfo(
                        windowsPhoneProjectService,
                        this.settingsService.CustomRendererTextTemplate,
                        name,
                        directory,
                        ProjectSuffix.WindowsPhone,
                        baseDictionary));
            }

            IProjectService droidProjectService = this.visualStudioService.DroidProjectService;

            if (droidProjectService != null)
            {
                TraceService.WriteLine("building droidProject textTemplate");

                textTemplates.Add(
                    this.GetTextTemplateInfo(
                        droidProjectService,
                        this.settingsService.CustomRendererTextTemplate,
                        name,
                        directory,
                        ProjectSuffix.Droid,
                        baseDictionary));
            }

            return textTemplates;
        }

        /// <summary>
        /// Gets the base dictionary.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="directory">The directory.</param>
        /// <returns></returns>
        internal Dictionary<string, string> GetBaseDictionary(
            string name,
            string directory)
        {
            TraceService.WriteLine("CustomRendererFactory::GetBaseDictionary");

            Dictionary<string, string> dictionary = new Dictionary<string, string>
                {
                    {
                        "NameSpace", 
                        string.Empty
                    },
                    {
                        "Directory", 
                        directory
                    },
                    {
                        "ClassName", 
                        string.Empty
                    },
                    {
                        "InterfaceName", 
                        "I" + name
                    }
                };

            return dictionary; 
        }
    }
}

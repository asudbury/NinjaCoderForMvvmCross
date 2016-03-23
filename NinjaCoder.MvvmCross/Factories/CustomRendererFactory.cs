// --------------------------------------------------------------------------------------------------------------------
// <summary>
//  Defines the CustomRendererFactory type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Factories
{
    using Entities;
    using Interfaces;
    using Scorchio.Infrastructure.Wpf.ViewModels.Wizard;
    using Scorchio.VisualStudio.Entities;
    using Scorchio.VisualStudio.Services;
    using Scorchio.VisualStudio.Services.Interfaces;
    using Services.Interfaces;
    using System.Collections.Generic;
    using UserControls.AddCustomerRenderers;
    using ViewModels.AddCustomRenderers;

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
                    ViewType = typeof(CustomRendereFinishedControl)
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
                WindowTitle = "Add Xamarin Forms Custom Renderer",
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
        /// <param name="renderer">The renderer.</param>
        /// <param name="codeBlock">The code block.</param>
        /// <returns></returns>
        public IEnumerable<TextTemplateInfo> GetTextTemplates(
            string name,
            string directory,
            string renderer,
            string codeBlock)
        {
            TraceService.WriteLine("CustomRendererFactory::GetTextTemplates");

            List<TextTemplateInfo> textTemplates = new List<TextTemplateInfo>();

            IProjectService formsProjectService = this.visualStudioService.XamarinFormsProjectService;

            if (formsProjectService == null)
            {
                return textTemplates;
            }

            Dictionary<string, string> baseDictionary = this.GetBaseDictionary(
                name,
                directory,
                renderer.Replace("Renderer", string.Empty),
                formsProjectService.Name,
                codeBlock);

            TraceService.WriteLine("building formsProject textTemplate");

            textTemplates.Add(
                this.GetTextTemplateInfo(
                    formsProjectService,
                    this.settingsService.BaseCustomRendererTextTemplate,
                    name,
                    directory,
                    ProjectSuffix.XamarinForms,
                    baseDictionary,
                    true));

            IProjectService iOSProjectService = this.visualStudioService.iOSProjectService;

            if (iOSProjectService != null)
            {
                TraceService.WriteLine("building iosProject textTemplate");

                baseDictionary["Platform"] = "iOS";

                textTemplates.Add(
                    this.GetTextTemplateInfo(
                        iOSProjectService,
                        this.settingsService.CustomRendererTextTemplate,
                        name,
                        directory,
                        ProjectSuffix.iOS,
                        baseDictionary,
                        true));
            }

            IProjectService windowsPhoneProjectService = this.visualStudioService.WindowsPhoneProjectService;

            if (windowsPhoneProjectService != null)
            {
                TraceService.WriteLine("building windowsPhoneProject textTemplate");

                baseDictionary["Platform"] = "WinPhone";

                textTemplates.Add(
                    this.GetTextTemplateInfo(
                        windowsPhoneProjectService,
                        this.settingsService.CustomRendererTextTemplate,
                        name,
                        directory,
                        ProjectSuffix.WindowsPhone,
                        baseDictionary,
                        true));
            }

            IProjectService droidProjectService = this.visualStudioService.DroidProjectService;

            if (droidProjectService != null)
            {
                TraceService.WriteLine("building droidProject textTemplate");

                baseDictionary["Platform"] = "Android";

                textTemplates.Add(
                    this.GetTextTemplateInfo(
                        droidProjectService,
                        this.settingsService.CustomRendererTextTemplate,
                        name,
                        directory,
                        ProjectSuffix.Droid,
                        baseDictionary,
                        true));
            }

            return textTemplates;
        }

        /// <summary>
        /// Gets the base dictionary.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="directory">The directory.</param>
        /// <param name="renderer">The renderer.</param>
        /// <param name="formsProjectName">Name of the forms project.</param>
        /// <param name="codeBlock">The code block.</param>
        /// <returns></returns>
        internal Dictionary<string, string> GetBaseDictionary(
            string name,
            string directory,
            string renderer,
            string formsProjectName,
            string codeBlock)
        {
            TraceService.WriteLine("CustomRendererFactory::GetBaseDictionary");

            Dictionary<string, string> dictionary = new Dictionary<string, string>
                {
                    {
                        "CodeBlock",
                        codeBlock
                    },
                    {
                        "RendererBaseType", 
                        name.Replace("Renderer", string.Empty)
                    },
                    {
                        "RendererType", 
                        renderer
                    },
                    {
                        "Platform", 
                        string.Empty
                    },
                    {
                        "NameSpace", 
                        string.Empty
                    },
                    {
                        "FormsProject",
                        formsProjectName
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
                        "BaseClassName", 
                        string.Empty
                    }
                };

            return dictionary; 
        }
    }
}

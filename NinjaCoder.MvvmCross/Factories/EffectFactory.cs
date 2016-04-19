// --------------------------------------------------------------------------------------------------------------------
// <summary>
//  Defines the EffectFactory type.
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
    using UserControls.AddEffects;
    using ViewModels.AddEffects;

    /// <summary>
    ///  Defines the EffectFactory type.
    /// </summary>
    public class EffectFactory : BaseTextTemplateFactory, IEffectFactory
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
        /// The settings service.
        /// </summary>
        private readonly ISettingsService settingsService;

        /// <summary>
        /// The visual studio service.
        /// </summary>
        private readonly IVisualStudioService visualStudioService;

        /// <summary>
        /// Initializes a new instance of the <see cref="EffectFactory" /> class.
        /// </summary>
        /// <param name="resolverService">The resolver service.</param>
        /// <param name="registerService">The register service.</param>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="visualStudioService">The visual studio service.</param>
        public EffectFactory(
            IResolverService resolverService,
            IRegisterService registerService,
            ISettingsService settingsService,
            IVisualStudioService visualStudioService)
        {
            this.resolverService = resolverService;
            this.registerService = registerService;
            this.settingsService = settingsService;
            this.visualStudioService = visualStudioService;
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
                    ViewModel = this.resolverService.Resolve<EffectViewModel>(),
                    ViewType = typeof(EffectControl)
                },
                new WizardStepViewModel
                    {
                        ViewModel = this.resolverService.Resolve<EffectFinishedViewModel>(),
                        ViewType = typeof(EffectFinishedControl)
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
                WindowTitle = "Add Xamarin Forms Effect",
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
        /// <returns>A list of TextTemplateInfos.</returns>
        public IEnumerable<TextTemplateInfo> GetTextTemplates(
            string name,
            string directory)
        {
            TraceService.WriteLine("EffectFactory::GetTextTemplates");

            List<TextTemplateInfo> textTemplates = new List<TextTemplateInfo>();

            Dictionary<string, string> baseDictionary = this.GetBaseDictionary(
                name,
                string.Empty);

            // ReSharper disable once InconsistentNaming
            IProjectService iOSProjectService = this.visualStudioService.iOSProjectService;

            if (iOSProjectService != null)
            {
                TraceService.WriteLine("building iosProject textTemplate");

                textTemplates.Add(
                    this.GetTextTemplateInfo(
                        iOSProjectService,
                        this.settingsService.EffectsTextTemplate,
                        name,
                        directory,
                        ProjectSuffix.iOS,
                        this.settingsService.iOSProjectSuffix,
                        baseDictionary,
                        false));
            }

            IProjectService windowsPhoneProjectService = this.visualStudioService.WindowsPhoneProjectService;

            if (windowsPhoneProjectService != null)
            {
                TraceService.WriteLine("building windowsPhoneProject textTemplate");

                textTemplates.Add(
                    this.GetTextTemplateInfo(
                        windowsPhoneProjectService,
                        this.settingsService.EffectsTextTemplate,
                        name,
                        directory,
                        ProjectSuffix.WindowsPhone,
                        this.settingsService.WindowsPhoneProjectSuffix,
                        baseDictionary,
                        false,
                        ".WinPhone"));
            }

            IProjectService droidProjectService = this.visualStudioService.DroidProjectService;

            if (droidProjectService != null)
            {
                TraceService.WriteLine("building droidProject textTemplate");

                textTemplates.Add(
                    this.GetTextTemplateInfo(
                        droidProjectService,
                        this.settingsService.EffectsTextTemplate,
                        name,
                        directory,
                        ProjectSuffix.Droid,
                        this.settingsService.DroidProjectSuffix,
                        baseDictionary,
                        false,
                        ".Android"));
            }

            IProjectService universalProjectService = this.visualStudioService.WindowsUniversalProjectService;

            if (universalProjectService != null)
            {
                TraceService.WriteLine("building universalProject textTemplate");

                textTemplates.Add(
                    this.GetTextTemplateInfo(
                        universalProjectService,
                        this.settingsService.EffectsTextTemplate,
                        name,
                        directory,
                        ProjectSuffix.WindowsUniversal,
                        this.settingsService.WindowsUniversalProjectSuffix,
                        baseDictionary,
                        false,
                        ".UWP"));
            }
            return textTemplates;
        }

        /// <summary>
        /// Gets the base dictionary.
        /// </summary>
        /// <param name="classNamespace">The class namespace.</param>
        /// <param name="className">Name of the class.</param>
        /// <returns>A Dictionary.</returns>
        internal Dictionary<string, string> GetBaseDictionary(
            string classNamespace,
            string className)
        {
            TraceService.WriteLine("EffectFactory::GetBaseDictionary");

            Dictionary<string, string> dictionary = new Dictionary<string, string>
                {
                    {
                        "NameSpace", classNamespace
                    },
                    {
                        "ClassName", className
                    }
                };

            return dictionary;
        }
    }
}

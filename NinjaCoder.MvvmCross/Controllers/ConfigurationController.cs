// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ConfigurationController type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Controllers
{
    using NinjaCoder.MvvmCross.Infrastructure.Services;

    using Scorchio.VisualStudio.Services;
    using Services.Interfaces;

    /// <summary>
    /// Defines the ConfigurationController type.
    /// </summary>
    public class ConfigurationController : BaseController
    {
        /// <summary>
        /// The configuration service.
        /// </summary>
        private readonly IConfigurationService configurationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationController" /> class.
        /// </summary>
        /// <param name="configurationService">The configuration service.</param>
        /// <param name="visualStudioService">The visual studio service.</param>
        /// <param name="readMeService">The read me service.</param>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="messageBoxService">The message box service.</param>
        /// <param name="dialogService">The dialog service.</param>
        /// <param name="formsService">The forms service.</param>
        public ConfigurationController(
            IConfigurationService configurationService,
            IVisualStudioService visualStudioService,
            IReadMeService readMeService,
            ISettingsService settingsService,
            IMessageBoxService messageBoxService,
            IDialogService dialogService,
            IFormsService formsService)
            : base(
            visualStudioService, 
            readMeService, 
            settingsService, 
            messageBoxService,
            dialogService,
            formsService)
        {
            TraceService.WriteLine("ConfigurationController::Constructor");

            this.configurationService = configurationService;
        }

        /// <summary>
        /// Runs this instance.
        /// </summary>
        public void Run()
        {
            TraceService.WriteLine("ConfigurationController::Run");

            this.configurationService.CreateUserDirectories();
        }
    }
}

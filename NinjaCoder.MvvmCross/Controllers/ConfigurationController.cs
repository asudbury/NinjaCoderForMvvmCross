// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ConfigurationController type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Controllers
{
    using Scorchio.VisualStudio.Services;
    using Services;
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
        public ConfigurationController(
            IConfigurationService configurationService,
            IVisualStudioService visualStudioService)
            : base(visualStudioService, new ReadMeService(), new SettingsService())
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

// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the PluginsController type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Controllers
{
    using System.Collections.Generic;

    using NinjaCoder.MvvmCross.Services;
    using NinjaCoder.MvvmCross.Services.Interfaces;
    using NinjaCoder.MvvmCross.Views;

    using Scorchio.VisualStudio.Extensions;

    /// <summary>
    /// Defines the PluginsController type.
    /// </summary>
    public class PluginsController : BaseController
    {
        /// <summary>
        /// The plugins service.
        /// </summary>
        private readonly IPluginsService pluginsService;

        /// <summary>
        /// Initializes a new instance of the <see cref="PluginsController" /> class.
        /// </summary>
        public PluginsController()
            : this(new PluginsService())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PluginsController"/> class.
        /// </summary>
        /// <param name="pluginsService">The plugins service.</param>
        public PluginsController(
            IPluginsService pluginsService)
            : base(new VisualStudioService(), new ReadMeService(), new SettingsService())
        {
            this.pluginsService = pluginsService;
        }

        /// <summary>
        /// Runs this instance.
        /// </summary>
        public void Run()
        {
            this.AddTraceHeader("PluginsController::Run");

            if (this.VisualStudioService.IsMvvmCrossSolution)
            {
                PluginsForm form = new PluginsForm(this.GetViewModelNames());

                form.ShowDialog();

                if (form.Continue)
                {
                    this.WriteStatusBarMessage("Ninja Coder is running....");

                    IEnumerable<string> messages = this.pluginsService.AddPlugins(
                        this.VisualStudioService,
                        form.RequiredPlugins,
                        form.ImplementInViewModel,
                        this.SettingsService.CodeSnippetsPath + @"\Plugins");

                    //// close any open documents.
                    this.VisualStudioService.DTE2.CloseDocuments();

                    //// now collapse the solution!
                    this.VisualStudioService.DTE2.CollapseSolution();

                    //// show the readme.
                    this.ShowReadMe("Add Plugins", messages);

                    this.WriteStatusBarMessage("Ninja Coder has completed the adding of the plugins.");
                }
            }
            else
            {
                this.ShowNotMvvmCrossSolutionMessage();
            }
        }
    }
}

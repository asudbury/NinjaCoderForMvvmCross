// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the PluginsController type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO.Abstractions;
    using Scorchio.VisualStudio.Extensions;
    using Scorchio.VisualStudio.Services;
    using Services;
    using Services.Interfaces;
    using Translators;
    using Views;

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
            : this(new PluginsService(new FileSystem(), new SettingsService(), new SnippetService(new FileSystem(), new CodeSnippetTranslator())))
        {
            TraceService.WriteLine("PluginsController::Constructor");
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PluginsController"/> class.
        /// </summary>
        /// <param name="pluginsService">The plugins service.</param>
        public PluginsController(
            IPluginsService pluginsService)
            : base(
                new VisualStudioService(), 
                new ReadMeService(), 
                new SettingsService())
        {
            this.pluginsService = pluginsService;
        }

        /// <summary>
        /// Runs this instance.
        /// </summary>
        public void Run()
        {
            this.AddTraceHeader("PluginsController", "Run");

            if (this.VisualStudioService.IsMvvmCrossSolution)
            {
                PluginsForm form = new PluginsForm(this.GetViewModelNames(), this.SettingsService.DisplayLogo);

                form.ShowDialog();

                if (form.Continue)
                {
                    this.WriteStatusBarMessage("Ninja Coder is running....");

                    try
                    {
                        IEnumerable<string> messages = this.pluginsService.AddPlugins(
                            this.VisualStudioService,
                            form.RequiredPlugins,
                            form.ImplementInViewModel,
                            form.IncludeUnitTests);

                        //// needs fixing - this is when we create the constructor parameters for the unit tests.
                        this.VisualStudioService.DTE2.ReplaceText(",)", ")", false);

                        this.VisualStudioService.DTE2.SaveAll();

                        //// show the readme.
                        this.ShowReadMe("Add Plugins", messages);

                        this.WriteStatusBarMessage("Ninja Coder has completed the adding of the plugins.");
                    }
                    catch (Exception exception)
                    {
                        TraceService.WriteError("Cannot create plugins exception=" + exception.Message);
                    }
                }
            }
            else
            {
                this.ShowNotMvvmCrossSolutionMessage();
            }
        }
    }
}

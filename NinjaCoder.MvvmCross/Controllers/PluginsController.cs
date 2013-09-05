// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the PluginsController type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Controllers
{
    using Constants;
    using Entities;
    using Scorchio.VisualStudio.Services;
    using Scorchio.VisualStudio.Services.Interfaces;
    using Services.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;
    using Translators;
    using Views.Interfaces;

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
        /// The nuget service.
        /// </summary>
        private readonly INugetService nugetService;

        /// <summary>
        /// The translator.
        /// </summary>
        private readonly ITranslator<string, Plugins> translator;

        /// <summary>
        /// Initializes a new instance of the <see cref="PluginsController" /> class.
        /// </summary>
        /// <param name="pluginsService">The plugins service.</param>
        /// <param name="nugetService">The nuget service.</param>
        /// <param name="visualStudioService">The visual studio service.</param>
        /// <param name="readMeService">The read me service.</param>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="messageBoxService">The message box service.</param>
        /// <param name="dialogService">The dialog service.</param>
        /// <param name="formsService">The forms service.</param>
        /// <param name="translator">The translator.</param>
        public PluginsController(
            IPluginsService pluginsService,
            INugetService nugetService,
            IVisualStudioService visualStudioService,
            IReadMeService readMeService,
            ISettingsService settingsService,
            IMessageBoxService messageBoxService,
            IDialogService dialogService,
            IFormsService formsService,
            ITranslator<string, Plugins> translator)
            : base(
            visualStudioService, 
            readMeService, 
            settingsService, 
            messageBoxService,
            dialogService,
            formsService)
        {
            TraceService.WriteLine("PluginsController::Constructor");

            this.pluginsService = pluginsService;
            this.nugetService = nugetService;
            this.translator = translator;
        }

        /// <summary>
        /// Runs this instance.
        /// </summary>
        public void Run()
        {
            TraceService.WriteHeader("PluginsController::Run");

            //// we open the nuget package manager console so we don't have
            //// a wait condition later!
            if (this.SettingsService.UseNugetForPlugins)
            {
                this.nugetService.OpenNugetWindow(this.VisualStudioService);
            }

            if (this.VisualStudioService.IsMvvmCrossSolution)
            {
                IProjectService projectService = this.VisualStudioService.CoreProjectService;

                if (projectService != null)
                {
                    IEnumerable<string> viewModelNames = projectService.GetFolderItems("ViewModels", false);

                    Plugins plugins = this.translator.Translate(this.SettingsService.CorePluginsPath);

                    IPluginsView view = this.FormsService.GetPluginsForm(this.SettingsService, viewModelNames, plugins);

                    DialogResult result = this.DialogService.ShowDialog(view as Form);

                    if (result == DialogResult.OK)
                    {
                        this.Process(view.RequiredPlugins, view.ImplementInViewModel, view.IncludeUnitTests);
                    }
                }
            }
            else
            {
                this.ShowNotMvvmCrossSolutionMessage();
            }
        }

        /// <summary>
        /// Processes the specified form.
        /// </summary>
        /// <param name="plugins">The plugins.</param>
        /// <param name="implementInViewModel">if set to <c>true</c> [implement in view model].</param>
        /// <param name="includeUnitTests">if set to <c>true</c> [include unit tests].</param>
        internal void Process(
            List<Plugin> plugins,
            string implementInViewModel,
            bool includeUnitTests)
        {
            TraceService.WriteLine("PluginsController::Process");

            this.VisualStudioService.WriteStatusBarMessage(NinjaMessages.NinjaIsRunning);

            try
            {
                if (this.SettingsService.SuspendReSharperDuringBuild)
                {
                    this.VisualStudioService.DTEService.ExecuteCommand(Settings.SuspendReSharperCommand);
                }

                IEnumerable<string> messages = this.pluginsService.AddPlugins(
                    this.VisualStudioService,
                    plugins,
                    implementInViewModel,
                    includeUnitTests);

                //// needs fixing - this is when we create the constructor parameters for the unit tests.
                this.VisualStudioService.DTEService.ReplaceText(",)", ")", false);

                this.VisualStudioService.WriteStatusBarMessage(NinjaMessages.UpdatingFiles);

                this.VisualStudioService.DTEService.SaveAll();

                if (this.SettingsService.UseNugetForPlugins)
                {
                    string nugetCommands = string.Join(Environment.NewLine, this.pluginsService.NugetCommands);

                    this.nugetService.Execute(
                        this.VisualStudioService,
                        this.GetReadMePath(),
                        nugetCommands,
                        this.SettingsService.SuspendReSharperDuringBuild);

                    this.VisualStudioService.WriteStatusBarMessage(NinjaMessages.NugetDownload);
                }
                else
                {
                    this.VisualStudioService.WriteStatusBarMessage(NinjaMessages.PluginsCompleted);

                    if (this.SettingsService.SuspendReSharperDuringBuild)
                    {
                        this.VisualStudioService.DTEService.ExecuteCommand(Settings.ResumeReSharperCommand);
                    }
                }

                //// show the readme.
                this.ShowReadMe("Add Plugins", messages, this.SettingsService.UseNugetForPlugins);
            }
            catch (Exception exception)
            {
                TraceService.WriteError("Cannot create plugins exception=" + exception.Message);
            }
        }
    }
}

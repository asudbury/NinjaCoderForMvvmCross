// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the NugetPackagesController type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Controllers
{
    using Constants;
    using Entities;
    using NinjaCoder.MvvmCross.Factories.Interfaces;
    using NinjaCoder.MvvmCross.ViewModels.AddNugetPackages;
    using NinjaCoder.MvvmCross.ViewModels.Wizard;
    using NinjaCoder.MvvmCross.Views.Wizard;
    using Scorchio.Infrastructure.Services;
    using Scorchio.VisualStudio.Services;
    using Services.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Defines the NugetPackagesController type.
    /// </summary>
    internal class NugetPackagesController : BaseController
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
        /// The nuget packages factory.
        /// </summary>
        private readonly INugetPackagesFactory nugetPackagesFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="NugetPackagesController" /> class.
        /// </summary>
        /// <param name="pluginsService">The plugins service.</param>
        /// <param name="nugetService">The nuget service.</param>
        /// <param name="visualStudioService">The visual studio service.</param>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="messageBoxService">The message box service.</param>
        /// <param name="resolverService">The resolver service.</param>
        /// <param name="readMeService">The read me service.</param>
        /// <param name="nugetPackagesFactory">The nuget packages factory.</param>
        public NugetPackagesController(
            IPluginsService pluginsService,
            INugetService nugetService,
            IVisualStudioService visualStudioService,
            ISettingsService settingsService,
            IMessageBoxService messageBoxService,
            IResolverService resolverService,
            IReadMeService readMeService,
            INugetPackagesFactory nugetPackagesFactory)
            : base(
            visualStudioService, 
            settingsService, 
            messageBoxService,
            resolverService,
            readMeService)
        {
            TraceService.WriteLine("NugetPackagesController::Constructor");

            this.pluginsService = pluginsService;
            this.nugetService = nugetService;
            this.nugetPackagesFactory = nugetPackagesFactory;
        }

        /// <summary>
        /// Runs this instance.
        /// </summary>
        public void Run()
        {
            TraceService.WriteHeader("NugetPackagesController::Run");

            //// we open the nuget package manager console so we don't have
            //// a wait condition later!
            this.nugetService.OpenNugetWindow();

            this.nugetPackagesFactory.RegisterWizardData();

            WizardFrameViewModel viewModel = this.ShowDialog<WizardFrameViewModel>(new WizardView());

            if (viewModel.Continue)
            {
                NugetPackagesViewModel nugetPackagesViewModel = (NugetPackagesViewModel)viewModel.GetWizardStepViewModel("NugetPackagesViewModel").ViewModel;
                XamarinFormsLabsViewModel xamarinFormsLabsViewModel = (XamarinFormsLabsViewModel)viewModel.GetWizardStepViewModel("XamarinFormsLabsViewModel").ViewModel;

                this.Process(
                    nugetPackagesViewModel,
                    xamarinFormsLabsViewModel);

            }
        }

        /// <summary>
        /// Processes the specified form.
        /// </summary>
        /// <param name="nugetPackagesViewModel">The nuget packages view model.</param>
        /// <param name="xamarinFormsLabsViewModel">The xamarin forms labs view model.</param>
        internal void Process(
             NugetPackagesViewModel nugetPackagesViewModel,
             XamarinFormsLabsViewModel xamarinFormsLabsViewModel)
        {
            TraceService.WriteLine("NugetPackagesController::Process");

            this.VisualStudioService.WriteStatusBarMessage(NinjaMessages.NinjaIsRunning);

            TraceService.WriteLine("ProjectsController::Process");

            string commands = string.Empty;

            this.VisualStudioService.WriteStatusBarMessage(NinjaMessages.NinjaIsRunning);

            List<string> messages = new List<string>();
            
            if (nugetPackagesViewModel != null)
            {
                List<Plugin> packages = nugetPackagesViewModel.GetRequiredPackages().ToList();

                if (packages.Any())
                {
                    commands += string.Join(
                        Environment.NewLine, 
                        this.pluginsService.GetNugetCommands(packages, false));

                    messages.AddRange(this.pluginsService.GetNugetMessages(packages));
                }
            }

            if (xamarinFormsLabsViewModel != null)
            {
                List<Plugin> plugins = xamarinFormsLabsViewModel.GetRequiredPlugins().ToList();

                if (plugins.Any())
                {
                    commands += string.Join(
                                Environment.NewLine, 
                                pluginsService.GetNugetCommands(plugins, false));

                    messages.AddRange(this.pluginsService.GetNugetMessages(plugins));
                }
            }

            if (this.SettingsService.OutputNugetCommandsToReadMe)
            {
                messages.Add(Environment.NewLine);
                messages.Add(this.ReadMeService.GetSeperatorLine());
                messages.Add(commands);
                messages.Add(this.ReadMeService.GetSeperatorLine());
            }

            this.ReadMeService.AddLines(
                this.GetReadMePath(),
                "Add Nuget Packages",
                messages);

            TraceService.WriteHeader("RequestedNugetCommands=" + commands);

            if (this.SettingsService.ProcessNugetCommands)
            {
                this.nugetService.Execute(
                    this.GetReadMePath(),
                    commands);
            }

            this.VisualStudioService.WriteStatusBarMessage(NinjaMessages.NugetDownload);
        }
    }
}

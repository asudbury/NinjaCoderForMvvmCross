// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ProjectsController type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Controllers
{
    using Constants;
    using Entities;
    using NinjaCoder.MvvmCross.Factories.Interfaces;
    using NinjaCoder.MvvmCross.ViewModels.AddNugetPackages;
    using NinjaCoder.MvvmCross.ViewModels.AddViews;
    using NinjaCoder.MvvmCross.ViewModels.Wizard;
    using NinjaCoder.MvvmCross.Views.Wizard;
    using Scorchio.Infrastructure.Extensions;
    using Scorchio.Infrastructure.Services;
    using Scorchio.VisualStudio.Entities;
    using Scorchio.VisualStudio.Services;
    using Scorchio.VisualStudio.Services.Interfaces;
    using Services.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;
    using ViewModels.AddProjects;

    using PluginsViewModel = NinjaCoder.MvvmCross.ViewModels.AddProjects.PluginsViewModel;

    /// <summary>
    /// Defines the ProjectsController type.
    /// </summary>
    internal class ProjectsController : BaseController
    {
        /// <summary>
        /// The projects service.
        /// </summary>
        private readonly IProjectsService projectsService;

        /// <summary>
        /// The nuget service.
        /// </summary>
        private readonly INugetService nugetService;

        /// <summary>
        /// The view model views service.
        /// </summary>
        private readonly IViewModelViewsService viewModelViewsService;

        /// <summary>
        /// The plugins service.
        /// </summary>
        private readonly IPluginsService pluginsService;

        /// <summary>
        /// The project factory.
        /// </summary>
        private readonly IProjectFactory projectFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectsController" /> class.
        /// </summary>
        /// <param name="projectsService">The projects service.</param>
        /// <param name="nugetService">The nuget service.</param>
        /// <param name="visualStudioService">The visual studio service.</param>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="messageBoxService">The message box service.</param>
        /// <param name="resolverService">The resolver service.</param>
        /// <param name="viewModelViewsService">The view model views service.</param>
        /// <param name="readMeService">The read me service.</param>
        /// <param name="pluginsService">The plugins service.</param>
        /// <param name="projectFactory">The project factory.</param>
        public ProjectsController(
            IProjectsService projectsService,
            INugetService nugetService,
            IVisualStudioService visualStudioService,
            ISettingsService settingsService,
            IMessageBoxService messageBoxService,
            IResolverService resolverService,
            IViewModelViewsService viewModelViewsService,
            IReadMeService readMeService,
            IPluginsService pluginsService,
            IProjectFactory projectFactory)
            : base(visualStudioService, settingsService, messageBoxService, resolverService, readMeService)
        {
            TraceService.WriteLine("ProjectsController::Constructor");

            this.projectsService = projectsService;
            this.nugetService = nugetService;
            this.viewModelViewsService = viewModelViewsService;
            this.pluginsService = pluginsService;
            this.projectFactory = projectFactory;
        }

        /// <summary>
        /// Runs this instance.
        /// </summary>
        public void Run()
        {
            TraceService.WriteHeader("ProjectsController::Run");
          
            //// we open the nuget package manager console so we don't have
            //// a wait condition later!
            
            this.nugetService.OpenNugetWindow();

            this.projectFactory.RegisterWizardData();

            WizardFrameViewModel viewModel = this.ShowDialog<WizardFrameViewModel>(new WizardView());

            if (viewModel.Continue)
            {
                ProjectsViewModel projectsViewModel = (ProjectsViewModel)viewModel.GetWizardStepViewModel("ProjectsViewModel").ViewModel;
                ViewsViewModel viewsViewModel = (ViewsViewModel)viewModel.GetWizardStepViewModel("ViewsViewModel").ViewModel;
                PluginsViewModel pluginsViewModel = (PluginsViewModel)viewModel.GetWizardStepViewModel("PluginsViewModel").ViewModel;
                NugetPackagesViewModel nugetPackagesViewModel = (NugetPackagesViewModel)viewModel.GetWizardStepViewModel("NugetPackagesViewModel").ViewModel;
                XamarinFormsLabsViewModel xamarinFormsLabsViewModel = (XamarinFormsLabsViewModel)viewModel.GetWizardStepViewModel("XamarinFormsLabsViewModel").ViewModel;

                this.Process(
                    projectsViewModel, 
                    viewsViewModel, 
                    pluginsViewModel, 
                    nugetPackagesViewModel,
                    xamarinFormsLabsViewModel);
            }
        }

        /// <summary>
        /// Processes the specified solution path.
        /// </summary>
        /// <param name="projectsViewModel">The projects view model.</param>
        /// <param name="viewsViewModel">The views view model.</param>
        /// <param name="pluginsViewModel">The plugins view model.</param>
        /// <param name="nugetPackagesViewModel">The nuget packages view model.</param>
        /// <param name="xamarinFormsLabsViewModel">The xamarin forms labs view model.</param>
        internal void Process(
            ProjectsViewModel projectsViewModel,
            ViewsViewModel viewsViewModel,
            PluginsViewModel pluginsViewModel,
            NugetPackagesViewModel nugetPackagesViewModel,
            XamarinFormsLabsViewModel xamarinFormsLabsViewModel)
        {
            //// TODO : this method is too big and needs refactoring!!!
            
            TraceService.WriteLine("ProjectsController::Process");

            string commands = string.Empty;

            this.VisualStudioService.WriteStatusBarMessage(NinjaMessages.NinjaIsRunning);

            //// create the solution if we don't have one!
            if (this.VisualStudioService.SolutionAlreadyCreated == false)
            {
                this.VisualStudioService.SolutionService.CreateEmptySolution(projectsViewModel.GetSolutionPath(), projectsViewModel.Project);
            }

            List<string> messages =
                this.projectsService.AddProjects(
                    this.VisualStudioService,
                    projectsViewModel.GetSolutionPath(),
                    projectsViewModel.GetFormattedRequiredTemplates())
                    .ToList();
            
            //// there is a bug in the xamarin iOS code that means it doesnt apply a couple of xml elements
            //// in the info.plist - here we fix that issue.

            if (this.SettingsService.FixInfoPlist)
            {
                this.FixInfoPlist(projectsViewModel);
            }

            if (this.SettingsService.ProcessViewModelAndViews && 
                this.SettingsService.FrameworkType != FrameworkType.NoFramework &&
                viewsViewModel != null)
            {
                IEnumerable<string> viewModelMessages = this.viewModelViewsService.AddViewModelsAndViews(viewsViewModel.Views);
            
                messages.AddRange(viewModelMessages);
            }

            if (pluginsViewModel != null &&
                (this.SettingsService.FrameworkType == FrameworkType.MvvmCross ||
                 this.SettingsService.FrameworkType == FrameworkType.MvvmCrossAndXamarinForms))
            {
                List<Plugin> plugins = pluginsViewModel.GetRequiredPlugins().ToList();

                messages.AddRange(this.pluginsService.AddPlugins(
                    plugins,
                    string.Empty,
                    false));

                IEnumerable<string> pluginCommands = this.pluginsService.GetNugetCommands(
                    plugins,
                    this.SettingsService.UsePreReleaseMvvmCrossNugetPackages);

                commands += string.Join(Environment.NewLine, pluginCommands);
            }

            this.VisualStudioService.WriteStatusBarMessage(NinjaMessages.UpdatingFiles);

            this.VisualStudioService.CodeTidyUp(
                this.SettingsService.RemoveDefaultFileHeaders,
                this.SettingsService.RemoveDefaultComments);

            commands += this.nugetService.GetNugetCommands(projectsViewModel.GetFormattedRequiredTemplates());

            if (nugetPackagesViewModel != null)
            {
                List<Plugin> packages = nugetPackagesViewModel.GetRequiredPackages().ToList();

                if (packages.Any())
                { 
                    messages.Add(string.Empty);

                    commands += string.Join(Environment.NewLine, this.pluginsService.GetNugetCommands(
                                    packages,
                                    this.SettingsService.UsePreReleaseXamarinFormsNugetPackages));

                    messages.AddRange(this.pluginsService.GetNugetMessages(packages));
                }
            }

            if (xamarinFormsLabsViewModel != null)
            {
                List<Plugin> plugins = xamarinFormsLabsViewModel.GetRequiredPlugins().ToList();
               
                if (plugins.Any())
                {
                    messages.Add(string.Empty);

                    commands += string.Join(Environment.NewLine, this.pluginsService.GetNugetCommands(
                                    plugins,
                                    this.SettingsService.UsePreReleaseXamarinFormsNugetPackages));

                    messages.AddRange(this.pluginsService.GetNugetMessages(plugins));
                }
            }

            //// a bit of (unnecessary) tidying up - replace double new lines!
            commands = commands.Replace(Environment.NewLine + Environment.NewLine, Environment.NewLine); 

            if (this.SettingsService.OutputNugetCommandsToReadMe)
            {
                messages.Add(string.Empty);
                messages.Add(this.ReadMeService.GetSeperatorLine());
                messages.Add("Nuget Commands");
                messages.Add(this.ReadMeService.GetSeperatorLine());
                messages.Add(string.Empty);
                messages.Add(commands);
            }

            this.ReadMeService.AddLines(
                this.GetReadMePath(),
                "Add Projects",
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

        /// <summary>
        /// Fixes the information plist.
        /// </summary>
        /// <param name="projectsViewModel">The projects view model.</param>
        private void FixInfoPlist(ProjectsViewModel projectsViewModel)
        {
            TraceService.WriteLine("ProjectsController::FixInfoPlist");

            IProjectService iosProjectService = this.VisualStudioService.iOSProjectService;

            if (iosProjectService != null)
            {
                //// first check its in the new projects being added!

                ProjectTemplateInfo projectTemplateInfo =
                    projectsViewModel.GetFormattedRequiredTemplates()
                        .FirstOrDefault(x => x.ProjectSuffix == ProjectSuffix.iOS.GetDescription());

                if (projectTemplateInfo != null)
                {
                    IProjectItemService projectItemService = iosProjectService.GetProjectItem("Info.plist");

                    if (projectItemService != null)
                    {
                        XDocument doc = XDocument.Load(projectItemService.FileName);

                        if (doc.Root != null)
                        {
                            XElement element = doc.Root.Element("dict");

                            if (element != null)
                            {
                                //// first look for the elements

                                XElement childElement = element.Elements("key").FirstOrDefault(x => x.Value == "CFBundleDisplayName");

                                if (childElement == null)
                                {
                                    element.Add(new XElement("key", "CFBundleDisplayName"));
                                    element.Add(new XElement("string", iosProjectService.Name));
                                }

                                childElement = element.Elements("key").FirstOrDefault(x => x.Value == "CFBundleVersion");

                                if (childElement == null)
                                {
                                    element.Add(new XElement("key", "CFBundleVersion"));
                                    element.Add(new XElement("string", "1.0"));
                                }

                                childElement = element.Elements("key").FirstOrDefault(x => x.Value == "CFBundleIdentifier");

                                if (childElement == null)
                                {
                                    element.Add(new XElement("key", "CFBundleIdentifier"));
                                    element.Add(new XElement("string", "1"));
                                }
                            }

                            doc.Save(projectItemService.FileName);
                        }
                    }
                }
            }
        }
    }
}

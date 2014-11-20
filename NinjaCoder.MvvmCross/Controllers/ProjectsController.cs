// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ProjectsController type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Controllers
{
    using Constants;
    using Entities;
    using Factories.Interfaces;
    using NinjaCoder.MvvmCross.Extensions;
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
    using ViewModels;
    using ViewModels.AddProjects;
    using Views;

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
        /// The view model and views factory.
        /// </summary>
        private readonly IViewModelAndViewsFactory viewModelAndViewsFactory;

        /// <summary>
        /// The read me service.
        /// </summary>
        private readonly IReadMeService readMeService;

        /// <summary>
        /// The plugins service.
        /// </summary>
        private readonly IPluginsService pluginsService;

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
        /// <param name="viewModelAndViewsFactory">The view model and views factory.</param>
        /// <param name="readMeService">The read me service.</param>
        /// <param name="pluginsService">The plugins service.</param>
        public ProjectsController(
            IProjectsService projectsService,
            INugetService nugetService,
            IVisualStudioService visualStudioService,
            ISettingsService settingsService,
            IMessageBoxService messageBoxService,
            IResolverService resolverService,
            IViewModelViewsService viewModelViewsService,
            IViewModelAndViewsFactory viewModelAndViewsFactory,
            IReadMeService readMeService,
            IPluginsService pluginsService)
            : base(visualStudioService, settingsService, messageBoxService, resolverService, readMeService)
        {
            TraceService.WriteLine("ProjectsController::Constructor");

            this.projectsService = projectsService;
            this.nugetService = nugetService;
            this.viewModelViewsService = viewModelViewsService;
            this.viewModelAndViewsFactory = viewModelAndViewsFactory;
            this.readMeService = readMeService;
            this.pluginsService = pluginsService;
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

            ProjectsViewModel2 viewModel2 = this.ShowDialog<ProjectsViewModel2>(new ProjectsView2());

            if (viewModel2.Continue)
            {
                ProjectsViewModel projectsViewModel = (ProjectsViewModel)viewModel2.GetWizardStepViewModel("ProjectsViewModel").ViewModel;
                ViewsViewModel viewsViewModel = (ViewsViewModel)viewModel2.GetWizardStepViewModel("ViewsViewModel").ViewModel;
                PluginsViewModel pluginsViewModel = (PluginsViewModel)viewModel2.GetWizardStepViewModel("PluginsViewModel").ViewModel;
                NugetPackagesViewModel nugetPackagesViewModel = (NugetPackagesViewModel)viewModel2.GetWizardStepViewModel("NugetPackagesViewModel").ViewModel;

                this.Process(
                    projectsViewModel, 
                    viewsViewModel, 
                    pluginsViewModel, 
                    nugetPackagesViewModel);
            }
        }

        /// <summary>
        /// Processes the specified solution path.
        /// </summary>
        /// <param name="projectsViewModel">The projects view model.</param>
        /// <param name="viewsViewModel">The views view model.</param>
        /// <param name="pluginsViewModel">The plugins view model.</param>
        /// <param name="nugetPackagesViewModel">The nuget packages view model.</param>
        internal void Process(
            ProjectsViewModel projectsViewModel,
            ViewsViewModel viewsViewModel,
            PluginsViewModel pluginsViewModel,
            NugetPackagesViewModel nugetPackagesViewModel)
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
                this.VisualStudioService.WriteStatusBarMessage(NinjaMessages.AddingViewModelAndViews);

                //// we need to store the xamarin forms views to change the names after nuget has run.
                this.SettingsService.XamarinFormsViews = viewsViewModel.Views.Where(
                    x => x.Framework == FrameworkType.XamarinForms.GetDescription()).ToList().SerializeToString();

                foreach (View view in viewsViewModel.Views)
                {
                    if (view.Existing == false)
                    { 
                        string viewModelName = view.Name + "ViewModel";

                        IEnumerable<ItemTemplateInfo> itemTemplateInfos =
                            this.viewModelAndViewsFactory.GetRequiredViewModelAndViews(
                                view,
                                viewModelName,
                                this.viewModelAndViewsFactory.AllowedUIViews,
                                true,
                                false);

                        IEnumerable<string> viewModelMessages = this.viewModelViewsService.AddViewModelAndViews(
                            this.VisualStudioService.CoreProjectService,
                            itemTemplateInfos,
                            viewModelName,
                            true,
                            null,
                            null);

                        messages.AddRange(viewModelMessages);
                    }
                }
            }

            if (pluginsViewModel != null &&
                (this.SettingsService.FrameworkType == FrameworkType.MvvmCross ||
                 this.SettingsService.FrameworkType == FrameworkType.MvvmCrossAndXamarinForms))
            {
                IEnumerable<Plugin> plugins = pluginsViewModel.GetRequiredPlugins();

                Plugin[] pluginsArray = plugins as Plugin[] ?? plugins.ToArray();

                IEnumerable<string> pluginMessages = this.pluginsService.AddPlugins(
                    this.VisualStudioService,
                    pluginsArray,
                    string.Empty,
                    false);

                TraceService.WriteLine("pluginMessages=" + pluginMessages);

                messages.AddRange(pluginMessages);

                List<string> pluginCommands = pluginsArray.Select(plugin => plugin.GetNugetCommandStrings(this.VisualStudioService)).ToList();

                commands += string.Join(Environment.NewLine, pluginCommands);
            }

            this.VisualStudioService.WriteStatusBarMessage(NinjaMessages.UpdatingFiles);

            this.VisualStudioService.CodeTidyUp(
                this.SettingsService.RemoveDefaultFileHeaders,
                this.SettingsService.RemoveDefaultComments);
                
            commands += this.nugetService.GetNugetCommands(projectsViewModel.GetFormattedRequiredTemplates());
            
            if (nugetPackagesViewModel != null)
            {
                IEnumerable<Plugin> packages = nugetPackagesViewModel.GetRequiredPackages();

                Plugin[] packagesArray = packages as Plugin[] ?? packages.ToArray();

                List<string> packageCommands = packagesArray.Select(plugin => plugin.GetNugetCommandStrings(this.VisualStudioService)).ToList();

                List<string> packageMessages = packagesArray.Select(plugin => plugin.GetNugetCommandMessages(this.VisualStudioService)).ToList();

                messages.AddRange(packageMessages);

                commands += string.Join(Environment.NewLine, packageCommands);
            }
            
            if (this.SettingsService.OutputNugetCommandsToReadMe)
            {
                messages.Add(commands);
            }

            this.readMeService.AddLines(
                this.GetReadMePath(),
                "Add Projects",
                messages);

            this.SettingsService.RequestedNugetCommands = commands;

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

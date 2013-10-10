// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ProjectsController type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Controllers
{
    using Constants;
    using Scorchio.VisualStudio.Entities;
    using Scorchio.VisualStudio.Services;
    using Scorchio.VisualStudio.Services.Interfaces;
    using Services.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Forms;
    using Views.Interfaces;
    using VSLangProj;

    /// <summary>
    /// Defines the ProjectsController type.
    /// </summary>
    public class ProjectsController : BaseController
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
        /// Initializes a new instance of the <see cref="ProjectsController" /> class.
        /// </summary>
        /// <param name="projectsService">The projects service.</param>
        /// <param name="nugetService">The nuget service.</param>
        /// <param name="visualStudioService">The visual studio service.</param>
        /// <param name="readMeService">The read me service.</param>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="messageBoxService">The message box service.</param>
        /// <param name="dialogService">The dialog service.</param>
        /// <param name="formsService">The forms service.</param>
        public ProjectsController(
            IProjectsService projectsService,
            INugetService nugetService,
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
            TraceService.WriteLine("ProjectsController::Constructor");

            this.projectsService = projectsService;
            this.nugetService = nugetService;
        }

        /// <summary>
        /// Runs this instance.
        /// </summary>
        public void Run()
        {
            TraceService.WriteHeader("ProjectsController::Run");

            //// we open the nuget package manager console so we don't have
            //// a wait condition later!
            this.nugetService.OpenNugetWindow(this.VisualStudioService);

            string defaultLocation;
            string defaultProjectName = this.VisualStudioService.GetDefaultProjectName();

            //// if this fails it will almost certainly be the COM integration with VStudio.
            try
            {
                defaultLocation = this.VisualStudioService.DTEService.GetDefaultProjectsLocation();
            }
            catch (Exception exception)
            {
                this.MessageBoxService.Show(@"Error Communicating with Visual Studio Error:-" + exception.Message, Settings.ApplicationName);
                return;
            }

            IEnumerable<ProjectTemplateInfo> projectTemplateInfos = this.VisualStudioService.AllowedProjectTemplates;

            if (projectTemplateInfos.Any())
            {
                IProjectsView view = this.FormsService.GetProjectsForm(
                    this.SettingsService,
                    defaultLocation, 
                    defaultProjectName, 
                    projectTemplateInfos);
                
                DialogResult result = this.DialogService.ShowDialog(view as Form);

                if (result == DialogResult.OK)
                {
                    this.Process(
                        view.Presenter.GetSolutionPath(),
                        view.ProjectName, 
                        view.Presenter.GetRequiredTemplates());
                }
            }
            else
            {
                this.MessageBoxService.Show(NinjaMessages.SolutionSetUp, Settings.ApplicationName);
            }
        }

        /// <summary>
        /// Processes the specified solution path.
        /// </summary>
        /// <param name="solutionPath">The solution path.</param>
        /// <param name="projectName">Name of the project.</param>
        /// <param name="requireTemplates">The template infos.</param>
        internal void Process(
            string solutionPath,
            string projectName,
            IEnumerable<ProjectTemplateInfo> requireTemplates)
        {
            TraceService.WriteLine("ProjectsController::Process");

            this.VisualStudioService.WriteStatusBarMessage(NinjaMessages.NinjaIsRunning);

            if (this.SettingsService.SuspendReSharperDuringBuild)
            {
                this.VisualStudioService.DTEService.ExecuteCommand(Settings.SuspendReSharperCommand);
            }

            //// create the solution if we don't have one!
            if (string.IsNullOrEmpty(this.VisualStudioService.SolutionService.FullName))
            {
                this.VisualStudioService.SolutionService.CreateEmptySolution(solutionPath, projectName);
            }

            List<string> messages =
                this.projectsService.AddProjects(
                    this.VisualStudioService,
                    solutionPath,
                    requireTemplates,
                    true,
                    this.SettingsService.IncludeLibFolderInProjects).ToList();

            this.VisualStudioService.WriteStatusBarMessage(NinjaMessages.UpdatingFiles);

            this.VisualStudioService.CodeTidyUp(
                this.SettingsService.RemoveDefaultFileHeaders,
                this.SettingsService.RemoveDefaultComments);

            //// create the version files.
            this.VisualStudioService.SolutionService.CreateFile(Settings.NinjaVersionFile, this.SettingsService.ApplicationVersion);
            this.VisualStudioService.SolutionService.CreateFile(Settings.MvxVersionFile, this.SettingsService.MvvmCrossVersion);

            if (this.SettingsService.UseNugetForProjectTemplates)
            {
                //// we shouldnt have to do this!
                //// but we have so many problems with nuget and iOS AND droid projects
                //// we try and make it as simple as possible by removing the mvx references!

                //// THIS CODE IS ALSO IN THE TEMPLATE WIZARD! - we could drop the template wizard code?
                this.RemoveMvxReferences(this.VisualStudioService.CoreProjectService);
                this.RemoveMvxReferences(this.VisualStudioService.CoreTestsProjectService);
                this.RemoveMvxReferences(this.VisualStudioService.iOSProjectService);
                this.RemoveMvxReferences(this.VisualStudioService.DroidProjectService);
                this.RemoveMvxReferences(this.VisualStudioService.WindowsPhoneProjectService);
                this.RemoveMvxReferences(this.VisualStudioService.WindowsStoreProjectService);
                this.RemoveMvxReferences(this.VisualStudioService.WpfProjectService);

                string commands = this.nugetService.GetNugetCommands(
                    this.VisualStudioService, 
                    requireTemplates, 
                    this.SettingsService.VerboseNugetOutput, 
                    this.SettingsService.DebugNuget);

                this.nugetService.Execute(
                    this.VisualStudioService, 
                    this.GetReadMePath(), 
                    commands,
                    this.SettingsService.SuspendReSharperDuringBuild);

                this.VisualStudioService.WriteStatusBarMessage(NinjaMessages.NugetDownload);
            }
            else
            {
                this.VisualStudioService.WriteStatusBarMessage(NinjaMessages.SolutionBuilt);

                if (this.SettingsService.SuspendReSharperDuringBuild)
                {
                    this.VisualStudioService.DTEService.ExecuteCommand(Settings.ResumeReSharperCommand);
                }
            }

            //// show the readme.
            this.ShowReadMe("Add Projects", messages, this.SettingsService.UseNugetForProjectTemplates);
        }

        /// <summary>
        /// Removes the MVX references.
        /// </summary>
        /// <param name="projectService">The project service.</param>
        internal void RemoveMvxReferences(IProjectService projectService)
        {
            if (projectService != null)
            {
                IEnumerable<Reference> references = projectService.GetProjectReferences();

                foreach (Reference reference in references)
                {
                    if (reference.Name.StartsWith("Cirrious"))
                    {
                        reference.Remove();
                    }
                }
            }
        }
    }
}

// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the BaseController type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Controllers
{
    using System.Collections.Generic;
    using Constants;
    using EnvDTE;
    using EnvDTE80;
    using NinjaCoder.MvvmCross.Infrastructure.Extensions;
    using NinjaCoder.MvvmCross.Infrastructure.Services;
    using Scorchio.VisualStudio.Services;
    using Scorchio.VisualStudio.Services.Interfaces;
    using Services.Interfaces;

    /// <summary>
    ///  Defines the BaseController type.
    /// </summary>
    internal abstract class BaseController
    {
        /// <summary>
        /// The readme lines
        /// </summary>
        private IList<string> readmeLines;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseController" /> class.
        /// </summary>
        /// <param name="visualStudioService">The visual studio service.</param>
        /// <param name="readMeService">The read me service.</param>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="messageBoxService">The message box service.</param>
        /// <param name="dialogService">The dialog service.</param>
        /// <param name="formsService">The forms service.</param>
        protected BaseController(
            IVisualStudioService visualStudioService,
            IReadMeService readMeService,
            ISettingsService settingsService,
            IMessageBoxService messageBoxService,
            IDialogService dialogService,
            IFormsService formsService)
        {
            //// init the tracing service first!
            TraceService.Initialize(
                settingsService.LogToTrace,
                false, //// log to console.
                settingsService.LogToFile,
                settingsService.LogFilePath,
                settingsService.DisplayErrors);

            TraceService.WriteLine("BaseController::Constructor");

            this.VisualStudioService = visualStudioService;
            this.ReadMeService = readMeService;
            this.SettingsService = settingsService;
            this.MessageBoxService = messageBoxService;
            this.DialogService = dialogService;
            this.FormsService = formsService;
        }

        /// <summary>
        /// Sets the visual studio instance.
        /// </summary>
        public DTE2 DTE2
        {
            set { this.VisualStudioService.DTE2 = value; }
        }

        /// <summary>
        /// Gets the visual studio service.
        /// </summary>
        protected IVisualStudioService VisualStudioService { get; private set; }

        /// <summary>
        /// Gets the read me service.
        /// </summary>
        protected IReadMeService ReadMeService { get; private set; }

        /// <summary>
        /// Gets the settings service.
        /// </summary>
        protected ISettingsService SettingsService { get; private set; }

        /// <summary>
        /// Gets the message box service.
        /// </summary>
        protected IMessageBoxService MessageBoxService { get; private set; }

        /// <summary>
        /// Gets the dialog service.
        /// </summary>
        protected IDialogService DialogService { get; private set; }

        /// <summary>
        /// Gets the forms service.
        /// </summary>
        protected IFormsService FormsService { get; private set; }

        /// <summary>
        /// Gets the read me lines.
        /// </summary>
        protected IList<string> ReadMeLines
        {
            get
            {
                if (this.readmeLines == null)
                {
                    this.ReadMeService.Initialize(this.SettingsService.ApplicationVersion, this.SettingsService.MvvmCrossVersion);
                    this.readmeLines = new List<string>();
                }

                return this.readmeLines;
            }
        }

        /// <summary>
        /// Project Item added event handler.
        /// </summary>
        /// <param name="projectItem">The project item.</param>
        internal void ProjectItemsEventsItemAdded(ProjectItem projectItem)
        {
            string message = string.Format(
                "BaseController::ProjectItemsEventsItemAdded file={0}",
                projectItem.Name);

            TraceService.WriteLine(message);

            ProjectItemService projectItemService = new ProjectItemService(projectItem);

            this.ProjectItemAdded(projectItemService);
        }

        /// <summary>
        /// Projects the item added.
        /// </summary>
        /// <param name="projectItemService">The project item service.</param>
        internal void ProjectItemAdded(IProjectItemService projectItemService)
        {
            TraceService.WriteLine("BaseController::ProjectItemAdded");

            bool saveFile = false;

            if (projectItemService.IsCSharpFile())
            {
                if (this.SettingsService.RemoveDefaultComments)
                {
                    projectItemService.RemoveComments();
                    saveFile = true;
                }

                if (this.SettingsService.RemoveDefaultFileHeaders)
                {
                    projectItemService.RemoveHeader();
                    saveFile = true;
                }
            }

            if (saveFile)
            {
                this.VisualStudioService.DTEService.SaveAll();
            }
        }

        /// <summary>
        /// Gets the read me path.
        /// </summary>
        /// <returns>The path of the ReadMe file.</returns>
        protected string GetReadMePath()
        {
            TraceService.WriteLine("BaseController::GetReadMePath");

            string path = this.VisualStudioService.SolutionService.GetDirectoryName() + Settings.NinjaReadMeFile;

            TraceService.WriteLine("BaseController::GetReadMePath path=" + path);
            return path;
        }

        /// <summary>
        /// Shows the not MVVM cross solution message.
        /// </summary>
        protected void ShowNotMvvmCrossSolutionMessage()
        {
            TraceService.WriteLine(Settings.NonMvvmCrossSolution);
            this.MessageBoxService.Show(Settings.NonMvvmCrossSolution, Settings.ApplicationName);
        }

        /// <summary>
        /// Shows the read me.
        /// </summary>
        /// <param name="function">The function.</param>
        /// <param name="messages">The messages.</param>
        /// <param name="nugetInProgress">if set to <c>true</c> [nuget in progress].</param>
        /// <param name="closeDocuments">if set to <c>true</c> [close documents].</param>
        /// <param name="collapseSolution">if set to <c>true</c> [collapse solution].</param>
        protected void ShowReadMe(
            string function,
            IEnumerable<string> messages,
            bool nugetInProgress = false,
            bool closeDocuments = true,
            bool collapseSolution = true)
        {
            TraceService.WriteLine("BaseController::ShowReadMe " + function + " nugetInProgress=" + nugetInProgress);

            //// close any open documents.
            if (closeDocuments)
            {
                this.VisualStudioService.DTEService.CloseDocuments();
            }

            //// now collapse the solution!
            if (collapseSolution)
            {
                this.VisualStudioService.DTEService.CollapseSolution();
            }

            string readMePath = this.GetReadMePath();

            TraceService.WriteLine("BaseController::ShowReadMe path=" + readMePath);
            
            //// now construct the ReadMe.txt
            this.ReadMeLines.AddRange(messages);

            this.ReadMeService.AddLines(readMePath, function, this.ReadMeLines);

            //// now show the ReadMe.txt.
            IProjectItemService projectItemService = this.VisualStudioService.SolutionService.AddSolutionItem("Solution Items", readMePath);

            if (projectItemService != null)
            {
                if (nugetInProgress == false)
                {
                    projectItemService.Open();
                }
            }
            else
            {
                TraceService.WriteError("BaseController::ShowReadMe Cannot open file :-" + readMePath);
            }

            //// reset the messages - if we don't do this we get the previous messages!
            this.readmeLines = new List<string>();
        }
    }
}

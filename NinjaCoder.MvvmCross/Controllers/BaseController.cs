// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the BaseController type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Controllers
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Windows.Forms;

    using EnvDTE;

    using EnvDTE80;

    using NinjaCoder.MvvmCross.Constants;
    using NinjaCoder.MvvmCross.Services.Interfaces;

    using Scorchio.VisualStudio.Extensions;
    using Scorchio.VisualStudio.Services;

    /// <summary>
    ///  Defines the BaseController type.
    /// </summary>
    public abstract class BaseController
    {
        /// <summary>
        /// The readme lines
        /// </summary>
        private List<string> readmeLines;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseController" /> class.
        /// </summary>
        /// <param name="visualStudioService">The visual studio service.</param>
        /// <param name="readMeService">The read me service.</param>
        /// <param name="settingsService">The settings service.</param>
        protected BaseController(
            IVisualStudioService visualStudioService,
            IReadMeService readMeService,
            ISettingsService settingsService)
        {
            this.VisualStudioService = visualStudioService;
            this.ReadMeService = readMeService;
            this.SettingsService = settingsService;
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
        /// Gets the read me lines.
        /// </summary>
        protected List<string> ReadMeLines
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
        /// Gets the read me path.
        /// </summary>
        /// <returns>The path of the ReadMe file.</returns>
        protected string GetReadMePath()
        {
            Solution2 solution3 = this.VisualStudioService.DTE2.GetSolution() as Solution2;

            return solution3.GetDirectoryName() + @"\NinjaReadMe.txt";
        }

        /// <summary>
        /// Writes the status bar message.
        /// </summary>
        /// <param name="message">The message.</param>
        protected void WriteStatusBarMessage(string message)
        {
            this.VisualStudioService.DTE2.WriteStatusBarMessage(message);
        }

        /// <summary>
        /// Adds the trace header.
        /// </summary>
        /// <param name="methodName">Name of the method.</param>
        protected void AddTraceHeader(string methodName)
        {
            TraceService.WriteLine("--------------------------------------------------------");
            TraceService.WriteLine("MvvmCrossController::" + methodName);
            TraceService.WriteLine("--------------------------------------------------------");
        }

        /// <summary>
        /// Shows the not MVVM cross solution message.
        /// </summary>
        protected void ShowNotMvvmCrossSolutionMessage()
        {
            MessageBox.Show(@"This solution is not a MvvmCross solution.", Settings.ApplicationName);
        }

        /// <summary>
        /// Gets the view model names.
        /// </summary>
        /// <returns>A List of view model names.</returns>
        protected List<string> GetViewModelNames()
        {
            List<string> viewModelNames = new List<string>();

            Project coreProject = this.VisualStudioService.CoreProject;

            //// look for the current view models in the project.
            if (coreProject != null)
            {
                ProjectItem projectItem = coreProject.GetFolder("ViewModels");

                IEnumerable<ProjectItem> projectItems = projectItem.GetSubProjectItems();

                viewModelNames.AddRange(projectItems.Select(item => Path.GetFileNameWithoutExtension(item.Name)));
            }

            return viewModelNames;
        }

        /// <summary>
        /// Shows the read me.
        /// </summary>
        /// <param name="function">The function.</param>
        /// <param name="messages">The messages.</param>
        protected void ShowReadMe(
            string function,
            IEnumerable<string> messages)
        {
            Solution2 solution2 = this.VisualStudioService.DTE2.GetSolution() as Solution2;

            //// now construct the ReadMe.txt
            this.ReadMeLines.AddRange(messages);
            this.ReadMeService.AddLines(this.GetReadMePath(), function, this.ReadMeLines);

            //// now show the ReadMe.txt.
            ProjectItem projectItem = solution2.AddSolutionItem("Solution Items", this.GetReadMePath());
            projectItem.Open();
        }
    }
}

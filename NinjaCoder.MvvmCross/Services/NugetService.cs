// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the NugetService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Constants;
    using EnvDTE;

    using Interfaces;
    using Scorchio.VisualStudio.Entities;
    using Scorchio.VisualStudio.Services;
    using Scorchio.VisualStudio.Services.Interfaces;
    
    /// <summary>
    /// Defines the NugetService type.
    /// </summary>
    public class NugetService : INugetService
    {
        /// <summary>
        /// The document events.
        /// </summary>
        private DocumentEvents documentEvents;

        /// <summary>
        /// Gets or sets the visual studio service.
        /// </summary>
        public IVisualStudioService VisualStudioService { get; set; }
       
        /// <summary>
        /// Gets or sets a value indicating whether [resume re sharper].
        /// </summary>
        private bool ResumeReSharper { get; set; }

        /// <summary>
        /// Gets the init nuget messages.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>The messages.</returns>
        public IEnumerable<string> GetInitNugetMessages(string message)
        {
            TraceService.WriteLine("NugetService::InitNugetMessages");

            return new List<string>
                        {
                            message, 
                            NinjaMessages.PmConsole, 
                            string.Empty
                        };
        }

        /// <summary>
        /// Gets the nuget commands.
        /// </summary>
        /// <param name="visualStudioService">The visual studio service.</param>
        /// <param name="templates">The templates.</param>
        /// <param name="verboseOutput">if set to <c>true</c> [verbose output].</param>
        /// <param name="debug">if set to <c>true</c> [debug].</param>
        /// <returns>The nuget commands.</returns>
        public string GetNugetCommands(
            IVisualStudioService visualStudioService,
            IEnumerable<ProjectTemplateInfo> templates,
            bool verboseOutput,
            bool debug)
        {
            TraceService.WriteLine("NugetService::ExecuteNugetCommands");

            string nugetCommandsString = string.Empty;

            string verboseOption = this.GetVerboseOption(verboseOutput);

            string debugOption = this.GetDebugOption(debug);

            foreach (ProjectTemplateInfo projectTemplateInfo in templates
                .Where(x => x.NugetCommands != null))
            {
                //// we need to work out if the project was successfully added to the solution
                //// in cases where the project is not supported (SDK not installed) it will
                //// not have been added (and therefore no nuget commands to run)

                IProjectService projectService = visualStudioService.GetProjectServiceBySuffix(projectTemplateInfo.ProjectSuffix);

                if (projectService != null)
                {
                    foreach (string nugetCommand in projectTemplateInfo.NugetCommands)
                    {
                        nugetCommandsString += string.Format(
                            "{0} {1} {2} {3}",
                            nugetCommand,
                            verboseOption,
                            debugOption,
                            Environment.NewLine);
                    }
                }
            }

            TraceService.WriteLine("commands=" + nugetCommandsString);

            return nugetCommandsString;
        }

        /// <summary>
        /// Opens the nuget window.
        /// </summary>
        /// <param name="visualStudioService">The visual studio service.</param>
        public void OpenNugetWindow(IVisualStudioService visualStudioService)
        {
            TraceService.WriteLine("NugetService::OpenNugetWindow");

            this.VisualStudioService = visualStudioService;

            this.VisualStudioService.DTEService.ExecuteNugetCommand(string.Empty);
        }

        /// <summary>
        /// Executes the specified commands.
        /// </summary>
        /// <param name="visualStudioService">The visual studio service.</param>
        /// <param name="readMePath">The read me path.</param>
        /// <param name="commands">The commands.</param>
        /// <param name="resumeReSharper">if set to <c>true</c> [resume re sharper].</param>
        /// <param name="setupEventHandlers">if set to <c>true</c> [setup event handlers].</param>
        public void Execute(
            IVisualStudioService visualStudioService,
            string readMePath,
            IEnumerable<string> commands,
            bool resumeReSharper,
            bool setupEventHandlers = true)
        {
            TraceService.WriteLine("NugetService::Execute");

            string nugetCommandsString = string.Join(Environment.NewLine, commands);

            this.Execute(
                visualStudioService,
                readMePath,
                nugetCommandsString,
                resumeReSharper,
                setupEventHandlers);
        }

        /// <summary>
        /// Executes the specified visual studio service.
        /// </summary>
        /// <param name="visualStudioService">The visual studio service.</param>
        /// <param name="readMePath">The read me path.</param>
        /// <param name="commands">The commands.</param>
        /// <param name="resumeReSharper">if set to <c>true</c> [resume re sharper].</param>
        /// <param name="setupEventHandlers">if set to <c>true</c> [setup event handlers].</param>
        public void Execute(
            IVisualStudioService visualStudioService,
            string readMePath,
            string commands,
            bool resumeReSharper,
            bool setupEventHandlers = true)
        {
            TraceService.WriteLine("NugetService::Execute");

            this.VisualStudioService = visualStudioService;
            this.ResumeReSharper = resumeReSharper;

            if (setupEventHandlers)
            {
                this.SetupEventHandlers();
            }

            //// add in the open readme operation - this is how we know nuget has finished!
            commands += Environment.NewLine + "$DTE.ItemOperations.OpenFile('" + readMePath + "')";
            
            this.VisualStudioService.DTEService.ExecuteNugetCommand(commands);
        }

        /// <summary>
        /// Setups the event handlers.
        /// </summary>
        internal void SetupEventHandlers()
        {
            TraceService.WriteLine("NugetService::SetupEventHandlers");

            this.documentEvents = this.VisualStudioService.DTEService.GetDocumentEvents();
            
            this.documentEvents.DocumentOpened += this.DocumentEventsDocumentOpened;
        }

        /// <summary>
        /// Removes the event handlers.
        /// </summary>
        internal void RemoveEventHandlers()
        {
            TraceService.WriteLine("NugetService::RemoveEventHandlers");

            this.documentEvents.DocumentOpened -= this.DocumentEventsDocumentOpened;
            this.documentEvents = null;
        }

        /// <summary>
        /// This runs when the NinjaReadMe.txt is opened after the nuget commands have run.
        /// </summary>
        /// <param name="document">The document.</param>
        internal void DocumentEventsDocumentOpened(Document document)
        {
            TraceService.WriteLine("NugetService::DocumentEventsDocumentOpened name" + document.FullName);
            this.NugetCompleted();
        }

        /// <summary>
        /// Completed the nuget updates.
        /// </summary>
        internal void NugetCompleted()
        {
            TraceService.WriteLine("NugetService::NugetCompleted");
            
            if (this.documentEvents != null)
            {
                this.RemoveEventHandlers();
            }

            this.FixTestProject();

            if (this.ResumeReSharper)
            {
                this.VisualStudioService.DTEService.ExecuteCommand(Settings.ResumeReSharperCommand);
            }
        }

        /// <summary>
        /// Fixes the test project. 
        /// At the moment we just remove the Cirrious.CrossCore.Wpf assembly.
        /// </summary>
        internal void FixTestProject()
        {
            TraceService.WriteLine("NugetService::FixTestProject");

            IProjectService projectService = this.VisualStudioService.CoreTestsProjectService;

            if (projectService != null)
            {
                projectService.RemoveReferences(".wpf");
                projectService.RemoveFolder("Bootstrap");
                projectService.RemoveFolder("ToDo-MvvmCross");
            }
        }

        /// <summary>
        /// Gets the verbose option.
        /// </summary>
        /// <param name="verboseOutput">if set to <c>true</c> [verbose output].</param>
        /// <returns>The verbose option.</returns>
        internal string GetVerboseOption(bool verboseOutput)
        {
            string verboseOption = string.Empty;

            if (verboseOutput)
            {
                verboseOption = "-verbose";
            }

            return verboseOption;
        }

        /// <summary>
        /// Gets the debug option.
        /// </summary>
        /// <param name="debug">if set to <c>true</c> [debug].</param>
        /// <returns>The debug option.</returns>
        internal string GetDebugOption(bool debug)
        {
            string debugOption = string.Empty;

            if (debug)
            {
                debugOption = "-debug";
            }

            return debugOption;
        }
    }
}

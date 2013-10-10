// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the NugetService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Services
{
    using System;
    using System.Collections.Generic;
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
        public bool ResumeReSharper { get; set; }

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

            string nugetCommands = string.Empty;

            string verboseOption = string.Empty;

            if (verboseOutput)
            {
                verboseOption = "-verbose";
            }

            string debugOption = string.Empty;

            if (debug)
            {
                debugOption = "-debug";
            }


            foreach (ProjectTemplateInfo projectTemplateInfo in templates)
            {
                if (projectTemplateInfo.NugetCommands != null)
                {
                    //// we need to work out if the project was sucessfully added to the solution
                    //// in cases where the project is not supported (SDK not installed) it will
                    //// not have been added (and therefore no nuget commands to run)

                    IProjectService projectService = visualStudioService.GetProjectServiceBySuffix(projectTemplateInfo.ProjectSuffix);

                    if (projectService != null)
                    {
                        foreach (string nugetCommand in projectTemplateInfo.NugetCommands)
                        {
                            nugetCommands += string.Format(
                                "{0} {1} {2} {3}", 
                                nugetCommand, 
                                verboseOption, 
                                debugOption, 
                                Environment.NewLine);
                        }
                    }
                }
            }

            return nugetCommands;
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
            List<string> commands,
            bool resumeReSharper,
            bool setupEventHandlers = true)
        {
            TraceService.WriteLine("NugetService::Execute");

            string nugetCommands = string.Join(Environment.NewLine, commands);

            this.Execute(
                visualStudioService,
                readMePath,
                nugetCommands,
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
                projectService.RemoveReference(Settings.MvxWpfAssembly);
            }
        }
    }
}

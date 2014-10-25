 // --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the NugetService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Services
{
    using EnvDTE;
    using Interfaces;

    using NinjaCoder.MvvmCross.Constants;

    using Scorchio.VisualStudio.Entities;
    using Scorchio.VisualStudio.Services;
    using Scorchio.VisualStudio.Services.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    
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
        /// Gets the init nuget messages.
        /// </summary>
        /// <returns>
        /// The messages.
        /// </returns>
        public IEnumerable<string> GetInitNugetMessages()
        {
            TraceService.WriteLine("NugetService::InitNugetMessages");

            return new List<string>
                        {
                            string.Empty
                        };
        }

        /// <summary>
        /// Gets the nuget commands.
        /// </summary>
        /// <param name="visualStudioService">The visual studio service.</param>
        /// <param name="templates">The templates.</param>
        /// <returns>
        /// The nuget commands.
        /// </returns>
        public string GetNugetCommands(
            IVisualStudioService visualStudioService,
            IEnumerable<ProjectTemplateInfo> templates)
        {
            TraceService.WriteLine("NugetService::ExecuteNugetCommands");

            string nugetCommandsString = string.Empty;

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
                            "{0} {1} {2}",
                            nugetCommand,
                            projectTemplateInfo.Name,
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
        public void Execute(
            IVisualStudioService visualStudioService,
            string readMePath,
            IEnumerable<string> commands)
        {
            TraceService.WriteLine("NugetService::Execute");

            string nugetCommandsString = string.Join(Environment.NewLine, commands);

            this.Execute(
                visualStudioService,
                readMePath,
                nugetCommandsString);
        }

        /// <summary>
        /// Executes the specified visual studio service.
        /// </summary>
        /// <param name="visualStudioService">The visual studio service.</param>
        /// <param name="readMePath">The read me path.</param>
        /// <param name="commands">The commands.</param>
        public void  Execute(
            IVisualStudioService visualStudioService,
            string readMePath,
            string commands)
        {
            TraceService.WriteLine("NugetService::Execute");

            this.VisualStudioService = visualStudioService;

            this.SetupEventHandlers();

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

            if (document.FullName.Contains(Settings.NinjaReadMeFile))
            {
                this.NugetCompleted();
            }

            else 
            {
                //// dont show the nsubstitue readme.
                document.ActiveWindow.Close();
            }
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

            this.VisualStudioService.DTEService.CollapseSolution();
        }

        /// <summary>
        /// Fixes the test project. 
        /// At the moment we  remove the *.Wpf assemblies.
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
    }
}

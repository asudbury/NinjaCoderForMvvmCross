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
        /// The visual studio service.
        /// </summary>
        private readonly IVisualStudioService visualStudioService;

        /// <summary>
        /// The settings service.
        /// </summary>
        private readonly ISettingsService settingsService;

        /// <summary>
        /// The caching service.
        /// </summary>
        private readonly ICachingService cachingService;

        /// <summary>
        /// The file operation service.
        /// </summary>
        private readonly IFileOperationService fileOperationService;

        /// <summary>
        /// The document events.
        /// </summary>
        private DocumentEvents documentEvents;

        /// <summary>
        /// Gets or sets a value indicating whether [resume re sharper].
        /// </summary>
        private bool ResumeReSharper { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="NugetService" /> class.
        /// </summary>
        /// <param name="visualStudioService">The visual studio service.</param>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="cachingService">The caching service.</param>
        /// <param name="fileOperationService">The file operation service.</param>
        public NugetService(
            IVisualStudioService visualStudioService,
            ISettingsService settingsService,
            ICachingService cachingService,
            IFileOperationService fileOperationService)
        {
            TraceService.WriteLine("NugetService::Constructor");

            this.visualStudioService = visualStudioService;
            this.settingsService = settingsService;
            this.cachingService = cachingService;
            this.fileOperationService = fileOperationService;
        }

        /// <summary>
        /// Gets the init nuget messages.
        /// </summary>
        /// <returns>
        /// The messages.
        /// </returns>
        public IEnumerable<string> GetInitNugetMessages()
        {
            TraceService.WriteLine("NugetService::InitNugetMessages");
            return new List<string> { string.Empty };
        }

        /// <summary>
        /// Gets the nuget commands.
        /// </summary>
        /// <param name="templates">The templates.</param>
        /// <returns>
        /// The nuget commands.
        /// </returns>
        public string GetNugetCommands(IEnumerable<ProjectTemplateInfo> templates)
        {
            TraceService.WriteLine("NugetService::GetNugetCommands");

            string nugetCommandsString = string.Empty;

            foreach (ProjectTemplateInfo projectTemplateInfo in templates
                .Where(x => x.NugetCommands != null))
            {
                //// we need to work out if the project was successfully added to the solution
                //// in cases where the project is not supported (SDK not installed) it will
                //// not have been added (and therefore no nuget commands to run)

                IProjectService projectService = this.visualStudioService.GetProjectServiceBySuffix(projectTemplateInfo.ProjectSuffix);

                if (projectService != null)
                {
                    foreach (string nugetCommand in projectTemplateInfo.NugetCommands)
                    {
                        string thisNugetCommand = nugetCommand;
                        
                        //// check to see if we are going to use local nuget

                        if (this.settingsService.UseLocalNuget && 
                            this.settingsService.LocalNugetName != string.Empty)
                        {
                            thisNugetCommand = thisNugetCommand.Replace(
                                "-ProjectName", 
                                " -Source " + this.settingsService.LocalNugetName + " -ProjectName");
                        }
                        
                        nugetCommandsString += string.Format(
                            "{0} {1} {2}",
                            thisNugetCommand,
                            projectTemplateInfo.Name,
                            Environment.NewLine);
                    }

                    if (this.settingsService.UseStyleCop)
                    {
                        nugetCommandsString += string.Format(
                        "{0} {1} {2}",
                        Settings.NugetInstallPackage.Replace("%s", Settings.StyleCopMsBuildPackage),
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
        public void OpenNugetWindow()
        {
            TraceService.WriteLine("NugetService::OpenNugetWindow");

            this.visualStudioService.DTEService.ExecuteNugetCommand(string.Empty);
        }

        /// <summary>
        /// Executes the specified commands.
        /// </summary>
        /// <param name="readMePath">The read me path.</param>
        /// <param name="commands">The commands.</param>
        /// <param name="resumeReSharper">if set to <c>true</c> [resume re sharper].</param>
        public void Execute(
            string readMePath,
            IEnumerable<string> commands,
            bool resumeReSharper)
        {
            TraceService.WriteLine("NugetService::Execute");

            string nugetCommandsString = string.Join(Environment.NewLine, commands);

            this.Execute(
                readMePath,
                nugetCommandsString,
                resumeReSharper);
        }

        /// <summary>
        /// Executes the specified visual studio service.
        /// </summary>
        /// <param name="readMePath">The read me path.</param>
        /// <param name="commands">The commands.</param>
        /// <param name="resumeReSharper">if set to <c>true</c> [resume re sharper].</param>
        public void  Execute(
            string readMePath,
            string commands,
            bool resumeReSharper)
        {
            TraceService.WriteLine("NugetService::Execute");
            this.ResumeReSharper = resumeReSharper;

            this.SetupEventHandlers();

            commands += Environment.NewLine + "$DTE.ItemOperations.OpenFile('" + readMePath + "')";

            this.visualStudioService.DTEService.ExecuteNugetCommand(commands);
        }

        /// <summary>
        /// Setups the event handlers.
        /// </summary>
        internal void SetupEventHandlers()
        {
            TraceService.WriteLine("NugetService::SetupEventHandlers");

            this.documentEvents = this.visualStudioService.DTEService.GetDocumentEvents();
            
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

            this.ExecutePostNugetCommands();
            this.ExecutePostNugetFileOperations();

            this.visualStudioService.DTEService.CollapseSolution();
            
            if (this.ResumeReSharper)
            {
                try
                {
                    //// this could fail - so catch exception.
                    this.visualStudioService.DTEService.ExecuteCommand(Settings.ResumeReSharperCommand);
                }
                catch (Exception exception)
                {
                    TraceService.WriteError("Error Resuming ReSharper exception=" + exception.Message);
                }
            }
        }

 
        /// <summary>
        /// Replaces the project item text.
        /// </summary>
        /// <param name="projectService">The project service.</param>
        /// <param name="projectItem">The project item.</param>
        /// <param name="text">The text.</param>
        /// <param name="replacementText">The replacement text.</param>
        internal void ReplaceProjectItemText(
            IProjectService projectService,
            string projectItem,
            string text,
            string replacementText)
        {
            TraceService.WriteLine("NugetService::ReplaceProjectItemText");

            if (projectService != null)
            {
                IProjectItemService projectItemService = projectService.GetProjectItem(projectItem);

                if (projectItemService != null)
                {
                    TraceService.WriteLine("Replacing " + text + " with " + replacementText);

                    projectItemService.ReplaceText(text, replacementText);
                }
            }
        }

        /// <summary>
        /// Executes the post nuget commands.
        /// </summary>
        internal void ExecutePostNugetCommands()
        {
            TraceService.WriteLine("NugetService::ExecutePostNugetCommands");

            IEnumerable<StudioCommand> postNugetCommands = this.cachingService.PostNugetCommands;

            if (postNugetCommands != null)
            {
                foreach (StudioCommand postNugetCommand in postNugetCommands)
                {
                    TraceService.WriteLine("Platform=" + postNugetCommand.PlatForm);
                    TraceService.WriteLine("CommandType=" + postNugetCommand.CommandType);
                    TraceService.WriteLine("Platform=" + postNugetCommand.PlatForm);
                    TraceService.WriteLine("Name=" + postNugetCommand.Name);

                    IProjectService projectService = this.visualStudioService.GetProjectServiceBySuffix(postNugetCommand.PlatForm);

                    if (projectService != null)
                    {
                        switch (postNugetCommand.CommandType)
                        {
                            case "RemoveFolder":
                                projectService.RemoveFolder(postNugetCommand.Name);
                                break;
                                
                            case "RemoveFile":
                                projectService.RemoveFolderItem(postNugetCommand.Name);
                                break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Executes the post nuget file operations.
        /// </summary>
        internal void ExecutePostNugetFileOperations()
        {
            TraceService.WriteLine("NugetService::ExecutePostNugetFileOperations");

            IEnumerable<FileOperation> postNugetFileOperations = this.cachingService.PostNugetFileOperations;

            if (postNugetFileOperations != null)
            {
                foreach (FileOperation postNugetFileOperation in postNugetFileOperations)
                {
                    this.fileOperationService.ProcessCommand(postNugetFileOperation);
                }
            }
        }
    }
}
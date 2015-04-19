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
        /// The document events.
        /// </summary>
        private DocumentEvents documentEvents;

        /// <summary>
        /// Initializes a new instance of the <see cref="NugetService" /> class.
        /// </summary>
        /// <param name="visualStudioService">The visual studio service.</param>
        public NugetService(IVisualStudioService visualStudioService)
        {
            TraceService.WriteLine("NugetService::Constructor");

            this.visualStudioService = visualStudioService;
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
        public void Execute(
            string readMePath,
            IEnumerable<string> commands)
        {
            TraceService.WriteLine("NugetService::Execute");

            string nugetCommandsString = string.Join(Environment.NewLine, commands);

            this.Execute(
                readMePath,
                nugetCommandsString);
        }

        /// <summary>
        /// Executes the specified visual studio service.
        /// </summary>
        /// <param name="readMePath">The read me path.</param>
        /// <param name="commands">The commands.</param>
        public void  Execute(
            string readMePath,
            string commands)
        {
            TraceService.WriteLine("NugetService::Execute");
            
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

            this.FixUpXamarinFormHelpers();
            this.FixUpMvvmCrossXamarinForms();

            this.visualStudioService.DTEService.CollapseSolution();
        }

        /// <summary>
        /// Fixes up xamarin form helpers.
        /// </summary>
        internal void FixUpXamarinFormHelpers()
        {
            TraceService.WriteLine("NugetService::FixUpXamarinFormHelpers");

            IProjectService formsProjectService = this.visualStudioService.XamarinFormsProjectService;

            if (formsProjectService != null)
            {
                IProjectService droidProjectService = this.visualStudioService.DroidProjectService;

                if (droidProjectService != null)
                {
                    IProjectItemService projectItemService = droidProjectService.GetProjectItem("MainActivity.cs");

                    if (projectItemService != null)
                    {
                        projectItemService.ReplaceText("Droid.Forms", "Forms");
                    }
                }

                IProjectService iosProjectService = this.visualStudioService.iOSProjectService;

                if (iosProjectService != null)
                {
                    IProjectItemService projectItemService = iosProjectService.GetProjectItem("AppDelegate.cs");

                    if (projectItemService != null)
                    {
                        projectItemService.ReplaceText("iOS.Forms", "Forms");
                    }
                }

                IProjectService windowsPhoneProjectService = this.visualStudioService.WindowsPhoneProjectService;

                if (windowsPhoneProjectService != null)
                {
                    IProjectItemService projectItemService = windowsPhoneProjectService.GetProjectItem("MainPage.xaml.cs");

                    if (projectItemService != null)
                    {
                        projectItemService.ReplaceText("FormsProject", formsProjectService.Name);
                    }
                }

                IProjectService wpfProjectService = this.visualStudioService.WpfProjectService;

                if (wpfProjectService != null)
                {
                    IProjectItemService projectItemService = wpfProjectService.GetProjectItem("App.xaml.cs");

                    if (projectItemService != null)
                    {
                        projectItemService.ReplaceText("Wpf.Forms", "Forms");
                    }
                }
            }
        }

        /// <summary>
        /// Fixes up MVVM cross xamarin forms.
        /// </summary>
        internal void FixUpMvvmCrossXamarinForms()
        {
            TraceService.WriteLine("NugetService::FixUpMvvmCrossXamarinForms");

            IProjectService coreProjectService = this.visualStudioService.CoreProjectService;
            IProjectService formsProjectService = this.visualStudioService.XamarinFormsProjectService;

            if (coreProjectService != null &&
                formsProjectService != null)
            { 
                string coreProjectName = coreProjectService.Name;
                string formsProjectName = formsProjectService.Name;

                //// Droid 
                
                this.ReplaceProjectItemText(
                    this.visualStudioService.DroidProjectService,
                    "Setup.cs",
                    "CoreProject",
                    coreProjectName);

                this.ReplaceProjectItemText(
                    this.visualStudioService.DroidProjectService,
                    "Setup.cs",
                    "FormsProject",
                    formsProjectName);

                this.ReplaceProjectItemText(
                    this.visualStudioService.DroidProjectService,
                    "MvxFormsAndroidViewPresenter.cs",
                    "CoreProject",
                    coreProjectName);

                this.ReplaceProjectItemText(
                    this.visualStudioService.DroidProjectService,
                    "MvxFormsAndroidViewPresenter.cs",
                    "FormsProject",
                    formsProjectName);
                
                this.ReplaceProjectItemText(
                    this.visualStudioService.DroidProjectService,
                    "MvxFormsAndroidNavigationActivity.cs",
                    "FormsProject",
                    formsProjectName);

                //// iOS
                
                this.ReplaceProjectItemText(
                    this.visualStudioService.iOSProjectService,
                    "Setup.cs",
                    "CoreProject",
                    coreProjectName);

                this.ReplaceProjectItemText(
                    this.visualStudioService.iOSProjectService,
                    "Setup.cs",
                    "FormsProject",
                    formsProjectName);

                this.ReplaceProjectItemText(
                   this.visualStudioService.iOSProjectService,
                   "MvxFormsTouchViewPresenter.cs",
                   "CoreProject",
                   coreProjectName);

                this.ReplaceProjectItemText(
                    this.visualStudioService.iOSProjectService,
                    "MvxFormsTouchViewPresenter.cs",
                    "FormsProject",
                    formsProjectName);

                //// Windows Phone
                
                this.ReplaceProjectItemText(
                    this.visualStudioService.WindowsPhoneProjectService,
                    "Setup.cs",
                    "CoreProject",
                    coreProjectName);

                this.ReplaceProjectItemText(
                    this.visualStudioService.WindowsPhoneProjectService,
                    "Setup.cs",
                    "FormsProject",
                    formsProjectName);

                this.ReplaceProjectItemText(
                    this.visualStudioService.WindowsPhoneProjectService,
                    "MvxFormsWindowsPhoneViewPresenter.cs",
                    "CoreProject",
                    coreProjectName);

                this.ReplaceProjectItemText(
                    this.visualStudioService.WindowsPhoneProjectService,
                    "MvxFormsWindowsPhoneViewPresenter.cs",
                    "FormsProject",
                    formsProjectName);
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
            if (projectService != null)
            {
                IProjectItemService projectItemService = projectService.GetProjectItem(projectItem);

                if (projectItemService != null)
                {
                    projectItemService.ReplaceText(text, replacementText);
                }
            }
        }
    }
}

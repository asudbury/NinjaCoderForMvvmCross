// --------------------------------------------------------------------------------------------------------------------
// <summary>
//  Defines the FileOperationService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Services
{
    using Interfaces;
    using Scorchio.VisualStudio.Entities;
    using Scorchio.VisualStudio.Services;
    using Scorchio.VisualStudio.Services.Interfaces;
    using System.Collections.Generic;

    /// <summary>
    ///  Defines the FileOperationService type.
    /// </summary>
    public class FileOperationService : IFileOperationService
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
        /// Initializes a new instance of the <see cref="FileOperationService" /> class.
        /// </summary>
        /// <param name="visualStudioService">The visual studio service.</param>
        /// <param name="settingsService">The settings service.</param>
        public FileOperationService(
            IVisualStudioService visualStudioService,
            ISettingsService settingsService)
        {
            this.visualStudioService = visualStudioService;
            this.settingsService = settingsService;
        }

        /// <summary>
        /// Processes the command.
        /// </summary>
        /// <param name="fileOperation">The file operation.</param>
        public void ProcessCommand(FileOperation fileOperation)
        {
            TraceService.WriteLine("FileOperationService::ProcessCommand");

            TraceService.WriteDebugLine("Platform=" + fileOperation.PlatForm);
            TraceService.WriteDebugLine("CommandType=" + fileOperation.CommandType);
            TraceService.WriteDebugLine("Directory=" + fileOperation.Directory);
            TraceService.WriteDebugLine("File=" + fileOperation.File);
            TraceService.WriteDebugLine("From=" + fileOperation.From);
            TraceService.WriteDebugLine("To=" + fileOperation.To);

            IProjectService projectService = this.visualStudioService.GetProjectServiceBySuffix(fileOperation.PlatForm);

            if (projectService != null)
            {
                IEnumerable<IProjectItemService> fileItemServices = this.GetFileItems(fileOperation, projectService);

                foreach (IProjectItemService projectItemService in fileItemServices)
                {
                    if (fileOperation.CommandType == "ReplaceText")
                    {
                        this.ReplaceText(fileOperation, projectService, projectItemService);
                    }
                    else if (fileOperation.CommandType == "Properties")
                    {
                        this.UpdateProperty(fileOperation, projectItemService);
                    }
                }
            }
            else
            {
                TraceService.WriteLine("Platform " + fileOperation.PlatForm + " not found");
            }
        }

        /// <summary>
        /// Gets the file items.
        /// </summary>
        /// <param name="fileOperation">The file operation.</param>
        /// <param name="projectService">The project service.</param>
        /// <returns>The files to have operations on.</returns>
        private IEnumerable<IProjectItemService> GetFileItems(
            FileOperation fileOperation, 
            IProjectService projectService)
        {
            TraceService.WriteLine("FileOperationService::GetFileItems");

            List<IProjectItemService> fileItemServices = new List<IProjectItemService>();

            if (string.IsNullOrEmpty(fileOperation.Directory) == false)
            {
                IProjectItemService projectItemService = projectService.GetFolder(fileOperation.Directory);

                if (projectItemService != null)
                {
                    if (string.IsNullOrEmpty(fileOperation.File) == false)
                    {
                        fileItemServices.Add(projectItemService.GetProjectItem(fileOperation.File));
                    }

                    else
                    {
                        IEnumerable<IProjectItemService> projectItemServices = projectItemService.GetCSharpProjectItems();

                        foreach (IProjectItemService childProjectItemService in projectItemServices)
                        {
                            TraceService.WriteDebugLine("FileOperationService::GetFileItems File=" + childProjectItemService.Name);
                            fileItemServices.Add(childProjectItemService);
                        }
                    }
                }
                else
                {
                    TraceService.WriteDebugLine("Directory " + fileOperation.Directory + " not found");
                }
            }
            else
            {
                fileItemServices.Add(projectService.GetProjectItem(fileOperation.File));
            }

            TraceService.WriteDebugLine("FileOperationService::GetFileItems fileItemServicesCount=" + fileItemServices.Count);

            return fileItemServices;
        }

        /// <summary>
        /// Replaces the text.
        /// </summary>
        /// <param name="fileOperation">The file operation.</param>
        /// <param name="projectService">The project service.</param>
        /// <param name="projectItemService">The project item service.</param>
        private void ReplaceText(
            FileOperation fileOperation,
            IProjectService projectService,
            IProjectItemService projectItemService)
        {
            if (projectService == null)
            {
                return;
            }

            if (projectItemService == null)
            {
                return;
            }

            string to = fileOperation.To.Replace("$rootnamespace$", projectService.Name);

            to = to.Replace("$CoreProject$", this.settingsService.CoreProjectSuffix.Substring(1));
            to = to.Replace("$FormsProject$", this.settingsService.XamarinFormsProjectSuffix.Substring(1));
            to = to.Replace("$iOSProject$", this.settingsService.iOSProjectSuffix.Substring(1));
            to = to.Replace("$DroidProject$", this.settingsService.DroidProjectSuffix.Substring(1));
            to = to.Replace("$WindosPhonedProject$", this.settingsService.WindowsPhoneProjectSuffix.Substring(1));
            to = to.Replace("$WindosUniversalProject$", this.settingsService.WindowsUniversalProjectSuffix.Substring(1));
            to = to.Replace("$WpfProject$", this.settingsService.WpfProjectSuffix.Substring(1));

            string from = fileOperation.From;

            TraceService.WriteDebugLine("from=" + @from + " to" + to);

            if (@from != to)
            {
                projectItemService.ReplaceText(fileOperation.From, to);
                TraceService.WriteDebugLine("**Replaced**");
            }
            else
            {
                TraceService.WriteDebugLine("No need to replace!");
            }
        }

        /// <summary>
        /// Updates the property.
        /// </summary>
        /// <param name="fileOperation">The file operation.</param>
        /// <param name="projectItemService">The project item service.</param>
        private void UpdateProperty(
            FileOperation fileOperation, 
            IProjectItemService projectItemService)
        {
            if (projectItemService.ProjectItem != null)
            {
                projectItemService.ProjectItem.Properties.Item(fileOperation.From).Value = fileOperation.To;
                TraceService.WriteDebugLine("**Properties Updates**");
            }
        }
    }
}

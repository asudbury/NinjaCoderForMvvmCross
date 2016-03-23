// --------------------------------------------------------------------------------------------------------------------
// <summary>
//  Defines the FileOperationService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Services
{
    using NinjaCoder.MvvmCross.Services.Interfaces;
    using Scorchio.VisualStudio.Entities;
    using Scorchio.VisualStudio.Services;
    using Scorchio.VisualStudio.Services.Interfaces;

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
        /// Initializes a new instance of the <see cref="FileOperationService"/> class.
        /// </summary>
        /// <param name="visualStudioService">The visual studio service.</param>
        public FileOperationService(IVisualStudioService visualStudioService)
        {
            this.visualStudioService = visualStudioService;
        }

        /// <summary>
        /// Processes the command.
        /// </summary>
        /// <param name="fileOperation">The file operation.</param>
        public void ProcessCommand(FileOperation fileOperation)
        {
            TraceService.WriteLine("FileOperationService::ProcessCommand");

            TraceService.WriteLine("Platform=" + fileOperation.PlatForm);
            TraceService.WriteLine("CommandType=" + fileOperation.CommandType);
            TraceService.WriteLine("Directory=" + fileOperation.Directory);
            TraceService.WriteLine("File=" + fileOperation.File);
            TraceService.WriteLine("From=" + fileOperation.From);
            TraceService.WriteLine("To=" + fileOperation.To);

            IProjectItemService fileItemService = null;

            IProjectService projectService = this.visualStudioService.GetProjectServiceBySuffix(fileOperation.PlatForm);

            if (projectService != null)
            {
                if (string.IsNullOrEmpty(fileOperation.Directory) == false)
                {
                    IProjectItemService projectItemService = projectService.GetFolder(fileOperation.Directory);

                    if (projectItemService != null)
                    {
                        fileItemService = projectItemService.GetProjectItem(fileOperation.File);
                    }
                    else
                    {
                        TraceService.WriteLine("Directory " + fileOperation.Directory + " not found");
                    }
                }
                else
                {
                    fileItemService = projectService.GetProjectItem(fileOperation.File);
                }

                if (fileItemService != null)
                {
                    switch (fileOperation.CommandType)
                    {
                        case "ReplaceText":

                            string to = fileOperation.To.Replace("$rootnamespace$", projectService.Name);
                            fileItemService.ReplaceText(fileOperation.From, to);
                            TraceService.WriteLine("**Replaced**");
                            break;

                        case "Properties":
                            if (fileItemService.ProjectItem != null)
                            {
                                fileItemService.ProjectItem.Properties.Item(fileOperation.From).Value = fileOperation.To;
                                TraceService.WriteLine("**Properties Updates**");
                            }

                            break;
                    }
                }
                else
                {
                    TraceService.WriteLine("File " + fileOperation.File + " not found");
                }
            }
            else
            {
                TraceService.WriteLine("Platform " + fileOperation.PlatForm + " not found");
            }
        }
    }
}

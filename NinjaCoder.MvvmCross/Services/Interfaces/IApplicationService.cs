// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the IApplicationService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Services.Interfaces
{
    using Microsoft.VisualStudio.TextTemplating;

    using NinjaCoder.MvvmCross.Entities;
    using Scorchio.VisualStudio.Entities;

    /// <summary>
    /// Defines the IApplicationService type.
    /// </summary>
    public interface IApplicationService
    {
        /// <summary>
        /// Views the log file.
        /// </summary>
        void ViewLogFile();

        /// <summary>
        /// Clears the log file.
        /// </summary>
        void ClearLogFile();
        
        /// <summary>
        /// Gets the application framework.
        /// </summary>
        /// <returns></returns>
        FrameworkType GetApplicationFramework();

        /// <summary>
        /// Suspends the resharper if requested.
        /// </summary>
        void SuspendResharperIfRequested();

        /// <summary>
        /// Fixes the information p list.
        /// </summary>
        /// <param name="projectTemplateInfo">The project template information.</param>
        void FixInfoPList(ProjectTemplateInfo projectTemplateInfo);

        /// <summary>
        /// Gets the commands list.
        /// </summary>
        /// <returns></returns>
        CommandsList GetCommandsList();

        /// <summary>
        /// Sets the working directory.
        /// </summary>
        /// <param name="path">The path.</param>
        void SetWorkingDirectory(string path);

        /// <summary>
        /// Sets the text templating engine host.
        /// </summary>
        /// <param name="useSimpleTextTemplatingEngine">if set to <c>true</c> [use simple text templating engine].</param>
        void UseSimpleTextTemplatingEngine(bool useSimpleTextTemplatingEngine);
    }
}

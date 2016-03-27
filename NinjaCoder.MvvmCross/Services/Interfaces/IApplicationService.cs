// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the IApplicationService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Services.Interfaces
{
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
        /// Views the error log file.
        /// </summary>
        void ViewErrorLogFile();

        /// <summary>
        /// Gets the application framework.
        /// </summary>
        /// <returns>The Framework Type.</returns>
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
        /// <returns>A CommandsList.</returns>
        CommandsList GetCommandsList();

        /// <summary>
        /// Sets the working directory.
        /// </summary>
        /// <param name="path">The path.</param>
        void SetWorkingDirectory(string path);

        /// <summary>
        /// Clears the error log file.
        /// </summary>
        void ClearErrorLogFile();

        /// <summary>
        /// Shows the MVVM cross home page.
        /// </summary>
        void ShowMvvmCrossHomePage();

        /// <summary>
        /// Shows the xamarin forms home page.
        /// </summary>
        void ShowXamarinFormsHomePage();
    }
}

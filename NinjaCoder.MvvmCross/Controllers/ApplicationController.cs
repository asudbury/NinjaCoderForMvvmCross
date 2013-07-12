// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ApplicationController type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Controllers
{
    using System.Collections.Generic;
    using EnvDTE;
    using EnvDTE80;
    using Scorchio.VisualStudio.Extensions;
    using Scorchio.VisualStudio.Services;

    using Services;
    using Views;

    /// <summary>
    /// Defines the ApplicationController type.
    /// </summary>
    public class ApplicationController : BaseController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationController"/> class.
        /// </summary>
        public ApplicationController()
            : base(new VisualStudioService(), new ReadMeService(), new SettingsService())
        {
        }

        /// <summary>
        /// Shows the options.
        /// </summary>
        public void ShowOptions()
        {
            OptionsForm form = new OptionsForm(this.SettingsService.DisplayLogo);

            form.ShowDialog();

            //// in case any of the setting have changed to do with logging reset them!
            TraceService.Initialize(
                this.SettingsService.LogToTrace, 
                this.SettingsService.LogToFile, 
                this.SettingsService.LogFilePath, 
                this.SettingsService.DisplayErrors);
        }

        /// <summary>
        /// Gets the projects.
        /// </summary>
        /// <returns>The projects.</returns>
        public IEnumerable<Project> GetProjects()
        {
            Solution2 solution = this.VisualStudioService.DTE2.GetSolution() as Solution2;

            return solution.GetProjects();
        }
    }
}

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

    using Services;
    using Views;

    using Scorchio.VisualStudio.Extensions;

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
            OptionsForm form = new OptionsForm();

            form.ShowDialog();
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

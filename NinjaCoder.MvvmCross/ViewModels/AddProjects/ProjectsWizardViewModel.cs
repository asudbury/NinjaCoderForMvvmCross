// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ProjectsWizardViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.ViewModels.AddProjects
{
    using NinjaCoder.MvvmCross.Factories.Interfaces;
    using Scorchio.Infrastructure.Wpf.ViewModels.Wizard;
    using System;

    /// <summary>
    ///  Defines the ProjectsWizardViewModel type.
    /// </summary>
    public class ProjectsWizardViewModel : WizardViewModel
    {
        /// <summary>
        /// Occurs when [on cancel].
        /// </summary>
        public event EventHandler OnCancel;

        /// <summary>
        /// Occurs when [on finish].
        /// </summary>
        public event EventHandler OnFinish;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectsWizardViewModel"/> class.
        /// </summary>
        /// <param name="projectFactory">The project factory.</param>
        public ProjectsWizardViewModel(IProjectFactory projectFactory)
        {
            this.Steps = projectFactory.GetWizardsSteps();
        }

        /// <summary>
        /// Cancels this instance.
        /// </summary>
        public override void Cancel()
        {
            EventHandler handler = this.OnCancel;

            if (handler != null)
            {
                handler(this, null);
            }
        }

        protected override void Finish()
        {
            EventHandler handler = this.OnFinish;

            if (handler != null)
            {
                handler(this, null);
            }
        }
    }
}

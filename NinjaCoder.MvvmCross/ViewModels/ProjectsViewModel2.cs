 // --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ProjectsViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.ViewModels
{
    using System.Linq;

    using AddProjects;
    using Factories.Interfaces;

    using NinjaCoder.MvvmCross.Services.Interfaces;

    using Scorchio.Infrastructure.Wpf.ViewModels.Wizard;
    using Scorchio.VisualStudio.Services;
    using System;

    /// <summary>
    ///  Defines the ProjectsViewModel type.
    /// </summary>
    internal class ProjectsViewModel2 : NinjaBaseViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectsViewModel" /> class.
        /// </summary>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="projectFactory">The project factory.</param>
        public ProjectsViewModel2(
            ISettingsService settingsService,
            IProjectFactory projectFactory)
            : base(settingsService)
        {
            TraceService.WriteLine("ProjectsViewModel::Constructor Start");

            this.ProjectsWizardViewModel = new ProjectsWizardViewModel(projectFactory);

            this.ProjectsWizardViewModel.OnCancel += ProjectsWizardViewModelOnOnCancel;
            this.ProjectsWizardViewModel.OnFinish += ProjectsWizardViewModelOnFinish;

            TraceService.WriteLine("ProjectsViewModel::Constructor End");
        }

        /// <summary>
        /// Gets or sets the wizard view model.
        /// </summary>
        public ProjectsWizardViewModel ProjectsWizardViewModel { get; set; }

        /// <summary>
        /// Gets the wizard step view model.
        /// </summary>
        /// <param name="name">The name.</param>
        public WizardStepViewModel GetWizardStepViewModel(string name)
        {
            return this.ProjectsWizardViewModel.Steps
                    .FirstOrDefault(x => x.ViewModel.ToString().Contains(name));
        }

        /// <summary>
        /// Projectses the wizard view model on on cancel.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ProjectsWizardViewModelOnOnCancel(object sender, EventArgs eventArgs)
        {
            this.OnCancel();
        }

        /// <summary>
        /// Projectses the wizard view model on finish.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ProjectsWizardViewModelOnFinish(object sender, EventArgs e)
        {
            this.OnOk();
        }
    }
}

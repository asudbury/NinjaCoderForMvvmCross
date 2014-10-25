// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ProjectFactory type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Factories
{
    using Interfaces;
    using NinjaCoder.MvvmCross.Entities;
    using NinjaCoder.MvvmCross.UserControls.AddViews;
    using Scorchio.Infrastructure.Wpf.ViewModels.Wizard;
    using Scorchio.VisualStudio.Entities;
    using Services.Interfaces;
    using System.Collections.Generic;
    using UserControls.AddProjects;
    using ViewModels.AddProjects;

    /// <summary>
    ///  Defines the ProjectFactory type.
    /// </summary>
    public class ProjectFactory : IProjectFactory
    {
        /// <summary>
        /// The resolver service.
        /// </summary>
        private readonly IResolverService resolverService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectFactory" /> class.
        /// </summary>
        /// <param name="resolverService">The resolver service.</param>
        public ProjectFactory(IResolverService resolverService)
        {
            this.resolverService = resolverService;
        }

        /// <summary>
        /// Gets the allowed projects.
        /// </summary>
        /// <param name="frameworkType">Type of the framework.</param>
        /// <returns>
        /// The allowed projects.
        /// </returns>
        public IEnumerable<ProjectTemplateInfo> GetAllowedProjects(FrameworkType frameworkType)
        {
            switch (frameworkType)
            {
                case FrameworkType.MvvmCross:
                    return this.resolverService.Resolve<IMvvmCrossProjectFactory>().GetAllowedProjects();

                case FrameworkType.XamarinForms:
                    return this.resolverService.Resolve<XamarinFormsProjectFactory>().GetAllowedProjects();

                default:
                    return this.resolverService.Resolve<MvvmCrossAndXamarinFormsProjectFactory>().GetAllowedProjects();
            }
        }

        /// <summary>
        /// Gets the wizards steps.
        /// </summary>
        /// <returns>
        /// The wizard steps.
        /// </returns>
        public List<WizardStepViewModel> GetWizardsSteps()
        {
            List<WizardStepViewModel> wizardSteps = new List<WizardStepViewModel>
            {
                new WizardStepViewModel
                {
                    ViewModel = this.resolverService.Resolve<FrameworkSelectorViewModel>(),
                    ViewType = typeof (FrameworkSelectorControl)
                },
                new WizardStepViewModel
                {
                    ViewModel = this.resolverService.Resolve<ProjectsViewModel>(),
                    ViewType = typeof (ProjectsControl)
                },
                new WizardStepViewModel
                {
                    ViewModel = this.resolverService.Resolve<ViewsViewModel>(),
                    ViewType = typeof (ViewsControl)
                },

                new WizardStepViewModel
                {
                    ViewModel = this.resolverService.Resolve<FinishedViewModel>(),
                    ViewType = typeof (FinishedControl)
                }
            };

            return wizardSteps;
        }
    }
}

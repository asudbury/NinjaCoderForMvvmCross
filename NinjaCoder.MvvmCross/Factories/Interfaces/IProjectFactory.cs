// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the IProjectFactory type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NinjaCoder.MvvmCross.Factories.Interfaces
{
    using Entities;
    using Scorchio.Infrastructure.Wpf.ViewModels.Wizard;
    using Scorchio.VisualStudio.Entities;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the IProjectFactory type.
    /// </summary>
    public interface IProjectFactory
    {
        /// <summary>
        /// Gets the allowed projects.
        /// </summary>
        /// <param name="frameworkType">Type of the framework.</param>
        /// <returns>
        /// The allowed projects.
        /// </returns>
        IEnumerable<ProjectTemplateInfo> GetAllowedProjects(FrameworkType frameworkType);

        /// <summary>
        /// Gets the wizards steps.
        /// </summary>
        /// <returns>
        /// The wizard steps.
        /// </returns>
        List<WizardStepViewModel> GetWizardsSteps();

        /// <summary>
        /// Registers the wizard data.
        /// </summary>
        void RegisterWizardData();

        /// <summary>
        /// Gets the route modifier.
        /// </summary>
        /// <param name="frameworkType">Type of the framework.</param>
        /// <returns>The RouteModifier.</returns>
        RouteModifier GetRouteModifier(FrameworkType frameworkType);
    }
}
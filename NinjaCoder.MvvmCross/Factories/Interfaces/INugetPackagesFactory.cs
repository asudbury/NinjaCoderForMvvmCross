// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the INugetPackagesFactory type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Factories.Interfaces
{
    using Scorchio.Infrastructure.Wpf.ViewModels.Wizard;
    using System.Collections.Generic;

    /// <summary>
    ///  Defines the INugetPackagesFactory type.
    /// </summary>
    public interface INugetPackagesFactory
    {
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
    }
}
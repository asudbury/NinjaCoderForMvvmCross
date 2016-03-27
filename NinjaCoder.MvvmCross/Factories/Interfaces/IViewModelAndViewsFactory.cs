// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the IViewModelAndViewsFactory type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Factories.Interfaces
{
    using Entities;

    using Scorchio.Infrastructure.Wpf.ViewModels.Wizard;
    using Scorchio.VisualStudio.Entities;
    using System.Collections.Generic;

    /// <summary>
    ///  Defines the IViewModelAndViewsFactory type.
    /// </summary>
    public interface IViewModelAndViewsFactory
    {
        /// <summary>
        /// Gets the allowed ui views.
        /// </summary>
        IEnumerable<ItemTemplateInfo> AllowedUIViews { get; }

        /// <summary>
        /// Gets the available view types.
        /// </summary>
        /// <returns>The available view types.</returns>
        IEnumerable<string> GetAvailableViewTypes();

        /// <summary>
        /// Gets the available MVVM cross ios view types.
        /// </summary>
        /// <returns>The view types.</returns>
        IEnumerable<string> GetAvailableMvvmCrossiOSViewTypes();

            /// <summary>
        /// Gets the required text templates.
        /// </summary>
        /// <param name="view">The view.</param>
        /// <param name="viewModelName">Name of the view model.</param>
        /// <param name="requiredUIViews">The required UI views.</param>
        /// <param name="unitTestsRequired">if set to <c>true</c> [unit tests required].</param>
        /// <returns>A list of TextTemplateInfos.</returns>
        IEnumerable<TextTemplateInfo> GetRequiredTextTemplates(
            View view,
            string viewModelName,
            IEnumerable<ItemTemplateInfo> requiredUIViews,
            bool unitTestsRequired);

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
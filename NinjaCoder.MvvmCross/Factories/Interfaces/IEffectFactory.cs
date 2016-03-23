// --------------------------------------------------------------------------------------------------------------------
// <summary>
//  Defines the IEffectFactory type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Factories.Interfaces
{
    using Scorchio.Infrastructure.Wpf.ViewModels.Wizard;
    using Scorchio.VisualStudio.Entities;
    using System.Collections.Generic;

    /// <summary>
    ///  Defines the IEffectFactory type.
    /// </summary>
    public interface IEffectFactory
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

        /// <summary>
        /// Gets the text templates.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="directory">The directory.</param>
        /// <returns></returns>
        IEnumerable<TextTemplateInfo> GetTextTemplates(
            string name,
            string directory);
    }
}

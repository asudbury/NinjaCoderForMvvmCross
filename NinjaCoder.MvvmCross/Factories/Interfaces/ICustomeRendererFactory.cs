// --------------------------------------------------------------------------------------------------------------------
// <summary>
// 	Defines the ICustomRendererFactory type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Factories.Interfaces
{
    using Scorchio.Infrastructure.Wpf.ViewModels.Wizard;
    using Scorchio.VisualStudio.Entities;
    using System.Collections.Generic;

    /// <summary>
    ///  Defines the ICustomRendererFactory type.
    /// </summary>
    public interface ICustomRendererFactory
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
        /// <param name="renderer">The renderer.</param>
        /// <returns></returns>
        IEnumerable<TextTemplateInfo> GetTextTemplates(
            string name,
            string directory,
            string renderer);
    }
}

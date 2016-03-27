// --------------------------------------------------------------------------------------------------------------------
// <summary>
//  Defines the IDependencyServicesFactory type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Factories.Interfaces
{
    using Scorchio.Infrastructure.Wpf.ViewModels.Wizard;
    using Scorchio.VisualStudio.Entities;
    using System.Collections.Generic;

    /// <summary>
    ///  Defines the IDependencyServicesFactory type.
    /// </summary>
    public interface IDependencyServicesFactory
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
        /// <param name="methodComment">The method comment.</param>
        /// <param name="methodReturnType">Type of the method return.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="directory">The directory.</param>
        /// <returns>A list of TextTemplateInfos.</returns>
        IEnumerable<TextTemplateInfo> GetTextTemplates(
            string name,
            string methodComment,
            string methodReturnType,
            string methodName,
            string directory);
    }
}

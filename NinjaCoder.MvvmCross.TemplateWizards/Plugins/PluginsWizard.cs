 // --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the PluginsWizard type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.TemplateWizards.Plugins
{
    using NinjaCoder.MvvmCross.TemplateWizards.Services;

    /// <summary>
    ///  Defines the PluginsWizard type.
    /// </summary>
    public class PluginsWizard : BaseWizard
    {
        /// <summary>
        /// Runs custom wizard logic at the beginning of a template wizard run.
        /// </summary>
        public override void OnRunStarted()
        {
            TraceService.WriteLine("PluginsWizard::OnRunStarted");
        }

        /// <summary>
        /// Called when [should add project item].
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns>True or false.</returns>
        public override bool OnShouldAddProjectItem(string filePath)
        {
            TraceService.WriteLine("PluginsWizard::OnShouldAddProjectItem path=" + filePath);

            return true;
        }
    }
}
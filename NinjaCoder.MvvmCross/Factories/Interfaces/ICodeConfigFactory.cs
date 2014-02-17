// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ICodeConfigFactory type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Factories.Interfaces
{
    using NinjaCoder.MvvmCross.Entities;
    using NinjaCoder.MvvmCross.Services.Interfaces;

    using Scorchio.VisualStudio.Entities;

    /// <summary>
    /// Defines the ICodeConfigFactory type.
    /// </summary>
    public interface ICodeConfigFactory
    {
        /// <summary>
        /// Gets the code config service.
        /// </summary>
        /// <returns>The code config service.</returns>
        ICodeConfigService GetCodeConfigService();

        /// <summary>
        /// Gets the plugin config.
        /// </summary>
        /// <param name="plugin">The plugin.</param>
        /// <returns>The code config.</returns>
        CodeConfig GetPluginConfig(Plugin plugin);

        /// <summary>
        /// Gets the service config.
        /// </summary>
        /// <param name="friendlyName">Name of the friendly.</param>
        /// <returns>The code config.</returns>
        CodeConfig GetServiceConfig(string friendlyName);
    }
}

// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the IPluginFactory type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Factories.Interfaces
{
    using NinjaCoder.MvvmCross.Entities;
    using NinjaCoder.MvvmCross.Services.Interfaces;

    /// <summary>
    ///  Defines the IPluginFactory type.
    /// </summary>
    public interface IPluginFactory
    {
        /// <summary>
        /// Gets the plugins service.
        /// </summary>
        /// <returns>The plugin service.</returns>
        IPluginsService GetPluginsService();

        /// <summary>
        /// Gets the plugins.
        /// </summary>
        /// <returns>The plugins.</returns>
        Plugins GetPlugins();

        /// <summary>
        /// Gets the name of the plugin by name.
        /// </summary>
        /// <param name="friendlyName">Name of the friendly.</param>
        /// <returns>The plugin.</returns>
        Plugin GetPluginByName(string friendlyName);
    }
}
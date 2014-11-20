// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the IPluginFactory type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Factories.Interfaces
{
    using Entities;
    using Services.Interfaces;

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
        /// <param name="uri">The URI.</param>
        Plugins GetPlugins(string uri);
    }
}
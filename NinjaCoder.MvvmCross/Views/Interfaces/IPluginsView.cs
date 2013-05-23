// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the IPluginsView type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NinjaCoder.MvvmCross.Views.Interfaces
{
    using System.Collections.Generic;

    using NinjaCoder.MvvmCross.Entities;

    /// <summary>
    /// Defines the IPluginsView type.
    /// </summary>
    public interface IPluginsView
    {
        /// <summary>
        /// Gets the implement in view model.
        /// </summary>
        string ImplementInViewModel { get;  }

        /// <summary>
        /// Gets the required templates.
        /// </summary>
        List<Plugin> RequiredPlugins { get; }

        /// <summary>
        /// Adds the plugin.
        /// </summary>
        /// <param name="plugin">The plugin.</param>
        void AddPlugin(Plugin plugin);

        /// <summary>
        /// Adds the plugin.
        /// </summary>
        /// <param name="viewModelName">Name of the view model.</param>
        void AddViewModel(string viewModelName);
    }
}

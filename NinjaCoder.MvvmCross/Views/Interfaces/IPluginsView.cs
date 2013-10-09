// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the IPluginsView type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Views.Interfaces
{
    using System.Collections.Generic;

    using Entities;

    /// <summary>
    /// Defines the IPluginsView type.
    /// </summary>
    public interface IPluginsView
    {
        /// <summary>
        /// Sets a value indicating whether [display logo].
        /// </summary>
        bool DisplayLogo { set; }

        /// <summary>
        /// Gets or sets a value indicating whether [use nuget].
        /// </summary>
        bool UseNuget { get; set; }

        /// <summary>
        /// Gets the implement in view model.
        /// </summary>
        string ImplementInViewModel { get;  }

        /// <summary>
        /// Gets the required templates.
        /// </summary>
        List<Plugin> RequiredPlugins { get; }

        /// <summary>
        /// Gets a value indicating whether [include unit tests].
        /// </summary>
        bool IncludeUnitTests { get; }

        /// <summary>
        /// Adds the plugin.
        /// </summary>
        /// <param name="plugin">The plugin.</param>
        void AddPlugin(Plugin plugin);

        /// <summary>
        /// Adds the viewModel.
        /// </summary>
        /// <param name="viewModelName">Name of the view model.</param>
        void AddViewModel(string viewModelName);
    }
}

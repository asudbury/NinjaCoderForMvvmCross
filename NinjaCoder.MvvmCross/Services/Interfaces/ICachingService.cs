// --------------------------------------------------------------------------------------------------------------------
// <summary>
// 	Defines the ICachingService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Services.Interfaces
{
    using NinjaCoder.MvvmCross.Entities;
    using System.Collections.Generic;

    using Scorchio.VisualStudio.Entities;

    /// <summary>
    ///  Defines the ICachingService type.
    /// </summary>
    public interface ICachingService
    {
        /// <summary>
        /// Gets or sets the application sample plug ins.
        /// </summary>
        IEnumerable<Plugin> ApplicationSamplePlugIns { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [xamarin forms labs nuget package requested].
        /// </summary>
        bool XamarinFormsLabsNugetPackageRequested { get; set; }

        /// <summary>
        /// Gets or sets the post nuget commands.
        /// </summary>
        IEnumerable<StudioCommand> PostNugetCommands { get; set; }

        /// <summary>
        /// Gets or sets the post nuget file operations.
        /// </summary>
        IEnumerable<FileOperation> PostNugetFileOperations { get; set; }

        /// <summary>
        /// Gets or sets the plugins.
        /// </summary>
        Dictionary<string, Plugins> Plugins { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has ninja nuget packages.
        /// </summary>
        bool HasNinjaNugetPackages { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has ninja community nuget packages.
        /// </summary>
        bool HasNinjaCommunityNugetPackages { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has local nuget packages.
        /// </summary>
        bool HasLocalNugetPackages { get; set; }

        /// <summary>
        /// Gets or sets the application commands list.
        /// </summary>
        CommandsList ApplicationCommandsList { get; set; }

        /// <summary>
        /// Gets the messages.
        /// </summary>
        IDictionary<string, string> Messages { get; } 
    }
}
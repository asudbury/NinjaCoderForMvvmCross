// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the CachingService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Services
{
    using Entities;
    using Interfaces;
    using Scorchio.VisualStudio.Entities;
    using System.Collections.Generic;

    /// <summary>
    ///  Defines the CachingService type.
    /// </summary>
    public class CachingService : ICachingService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CachingService"/> class.
        /// </summary>
        public CachingService()
        {
            this.Plugins = new Dictionary<string, Plugins>();
            this.Messages = new Dictionary<string, string>();
        }

        /// <summary>
        /// Gets or sets the application sample plug ins.
        /// </summary>
        public IEnumerable<Plugin> ApplicationSamplePlugIns { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [xamarin forms labs nuget package requested].
        /// </summary>
        public bool XamarinFormsLabsNugetPackageRequested { get; set; }

        /// <summary>
        /// Gets or sets the post nuget commands.
        /// </summary>
        public IEnumerable<StudioCommand> PostNugetCommands { get; set; }

        /// <summary>
        /// Gets or sets the post nuget file operations.
        /// </summary>
        public IEnumerable<FileOperation> PostNugetFileOperations { get; set; }

        /// <summary>
        /// Gets or sets the plugins.
        /// </summary>
        public Dictionary<string, Plugins> Plugins { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has ninja nuget packages.
        /// </summary>
        public bool HasNinjaNugetPackages { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has ninja community nuget packages.
        /// </summary>
        public bool HasNinjaCommunityNugetPackages { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has local nuget packages.
        /// </summary>
        public bool HasLocalNugetPackages { get; set; }

        /// <summary>
        /// Gets or sets the application commands list.
        /// </summary>
        public CommandsList ApplicationCommandsList { get; set; }
        
        /// <summary>
        /// Gets the messages.
        /// </summary>
        public IDictionary<string, string> Messages { get; private set; }

        /// <summary>
        /// Gets or sets the projects.
        /// </summary>
        public IEnumerable<ProjectTemplateInfo> Projects { get; set; }
    }
}
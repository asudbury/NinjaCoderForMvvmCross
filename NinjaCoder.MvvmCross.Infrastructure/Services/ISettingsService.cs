// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ISettingsService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Infrastructure.Services
{
    /// <summary>
    /// Defines the ISettingsService type.
    /// </summary>
    public interface ISettingsService
    {
        /// <summary>
        /// Gets a value indicating whether [display logo].
        /// </summary>
        bool DisplayLogo { get; }

        /// <summary>
        /// Gets or sets a value indicating whether [log to trace].
        /// </summary>
        bool LogToTrace { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [log to file].
        /// </summary>
        bool LogToFile { get; set; }

        /// <summary>
        /// Gets or sets the log file path.
        /// </summary>
        string LogFilePath { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [include lib folder in projects].
        /// </summary>
        bool IncludeLibFolderInProjects { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [use nuget for project templates].
        /// </summary>
        bool UseNugetForProjectTemplates { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [use nuget for plugins].
        /// </summary>
        bool UseNugetForPlugins { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [use nuget for services].
        /// </summary>
        bool UseNugetForServices { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [suspend re sharper during build].
        /// </summary>
        bool SuspendReSharperDuringBuild { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [display errors].
        /// </summary>
        bool DisplayErrors { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [remove default file headers].
        /// </summary>
        bool RemoveDefaultFileHeaders { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [remove default comments].
        /// </summary>
        bool RemoveDefaultComments { get; set; }

        /// <summary>
        /// Gets the converters templates path.
        /// </summary>
        string ConvertersTemplatesPath { get; }

        /// <summary>
        /// Gets the services templates path.
        /// </summary>
        string ServicesTemplatesPath { get; }

        /// <summary>
        /// Gets the code snippets path.
        /// </summary>
        string CodeSnippetsPath { get; }

        /// <summary>
        /// Gets the MVVM cross assemblies path.
        /// </summary>
        string MvvmCrossAssembliesPath { get; }

        /// <summary>
        /// Gets the config path.
        /// </summary>
        string ConfigPath { get; }

        /// <summary>
        /// Gets the application version.
        /// </summary>
        string ApplicationVersion { get; }

        /// <summary>
        /// Gets the MvvmCross version.
        /// </summary>
        string MvvmCrossVersion { get; }

        /// <summary>
        /// Gets the download nuget page.
        /// </summary>
        string DownloadNugetPage { get; }

        /// <summary>
        /// Gets the unit testing assemblies.
        /// </summary>
        string UnitTestingAssemblies { get; }

        /// <summary>
        /// Gets the unit testing init method.
        /// </summary>
        string UnitTestingInitMethod { get; }

        /// <summary>
        /// Gets the name of the base view model.
        /// </summary>
        string BaseViewModelName { get; }

        /// <summary>
        /// Gets the view model navigation snippet file.
        /// </summary>
        string ViewModelNavigationSnippetFile { get; }

        /// <summary>
        /// Gets the snippets override directory.
        /// </summary>
        string SnippetsOverrideDirectory { get; }

        /// <summary>
        /// Gets the configs override directory.
        /// </summary>
        string ConfigsOverrideDirectory { get; }

        /// <summary>
        /// Gets or sets a value indicating whether [format function parameters].
        /// </summary>
        bool FormatFunctionParameters { get; set; }

        /// <summary>
        /// Gets a value indicating whether [replace variables in snippets].
        /// </summary>
        bool ReplaceVariablesInSnippets { get; }

        /// <summary>
        /// Gets or sets the MVVM cross plugins wiki page.
        /// </summary>
        string MvvmCrossPluginsWikiPage { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [verbose nuget output].
        /// </summary>
        bool VerboseNugetOutput { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [debug nuget].
        /// </summary>
        bool DebugNuget { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [remove assemblies if nuget used].
        /// </summary>
        bool RemoveAssembliesIfNugetUsed { get; set; }

        /// <summary>
        /// Gets the installed directory.
        /// </summary>
        string InstalledDirectory { get; }

        /// <summary>
        /// Gets the build date time.
        /// </summary>
        string BuildDateTime { get; }

        /// <summary>
        /// Gets or sets a value indicating whether [copy assemblies to lib folder].
        /// </summary>
        bool CopyAssembliesToLibFolder { get; set; }

        /// <summary>
        /// Gets a value indicating whether [process template wizards].
        /// </summary>
        bool ProcessTemplateWizards { get; }

        /// <summary>
        /// Gets or sets the active project.
        /// </summary>
        string ActiveProject { get; set; }

        /// <summary>
        /// Gets or sets the plugins to add.
        /// </summary>
        string PluginsToAdd { get; set; }

        /// <summary>
        /// Gets a value indicating whether [process nuget commands].
        /// </summary>
        bool ProcessNugetCommands { get; }

        /// <summary>
        /// Gets or sets the MVVM cross assemblies override directory.
        /// </summary>
        string MvvmCrossAssembliesOverrideDirectory { get; set; }
     }
}

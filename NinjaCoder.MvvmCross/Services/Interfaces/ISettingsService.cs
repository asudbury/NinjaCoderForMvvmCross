// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ISettingsService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Services.Interfaces
{
    using NinjaCoder.MvvmCross.Entities;

    /// <summary>
    /// Defines the ISettingsService type.
    /// </summary>
    public interface ISettingsService
    {
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
        /// Gets the services templates path.
        /// </summary>
        string ServicesTemplatesPath { get; }

        /// <summary>
        /// Gets the code snippets path.
        /// </summary>
        string CodeSnippetsPath { get; }

        /// <summary>
        /// Gets the plugins code snippets path.
        /// </summary>
        string PluginsCodeSnippetsPath { get; }

        /// <summary>
        /// Gets the services code snippets path.
        /// </summary>
        string ServicesCodeSnippetsPath { get; }

        /// <summary>
        /// Gets the plugins config path.
        /// </summary>
        string PluginsConfigPath { get; }

        /// <summary>
        /// Gets the services config path.
        /// </summary>
        string ServicesConfigPath { get; }

        /// <summary>
        /// Gets the config path.
        /// </summary>
        string ConfigPath { get; }

        /// <summary>
        /// Gets the application version.
        /// </summary>
        string ApplicationVersion { get; }

        /// <summary>
        /// Gets the unit testing assemblies.
        /// </summary>
        string UnitTestingAssemblies { get; }

        /// <summary>
        /// Gets the unit testing init method.
        /// </summary>
        string UnitTestingInitMethod { get; }

        /// <summary>
        /// Gets the view model navigation snippet file.
        /// </summary>
        string ViewModelNavigationSnippetFile { get; }

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
        /// Gets the installed directory.
        /// </summary>
        string InstalledDirectory { get; }

        /// <summary>
        /// Gets the build date time.
        /// </summary>
        string BuildDateTime { get; }

        /// <summary>
        /// Gets a value indicating whether [process view model and views].
        /// </summary>
        bool ProcessViewModelAndViews { get; }

        /// <summary>
        /// Gets a value indicating whether [process wizard].
        /// </summary>
        bool ProcessWizard { get; }

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
        /// Gets a value indicating whether [beta testing].
        /// </summary>
        bool BetaTesting { get; }

        /// <summary>
        /// Gets or sets the default projects path.
        /// </summary>
        string DefaultProjectsPath { get; set; }

        /// <summary>
        /// Gets or sets the theme.
        /// </summary>
        string Theme { get; set; }

        /// <summary>
        /// Gets or sets the color of the theme.
        /// </summary>
        string ThemeColor { get; set; }

        /// <summary>
        /// Gets or sets the testing framework.
        /// </summary>
        string TestingFramework { get; set; }

        /// <summary>
        /// Gets or sets the mocking framework.
        /// </summary>
        string MockingFramework { get; set; }

        /// <summary>
        /// Gets or sets the default users paths set.
        /// </summary>
        bool DefaultUsersPathsSet { get; set; }

        /// <summary>
        /// Gets or sets the default user plugins path.
        /// </summary>
        string DefaultUserPluginsPath { get; set; }

        /// <summary>
        /// Gets or sets the default user services path.
        /// </summary>
        string DefaultUserServicesPath { get; set; }

        /// <summary>
        /// Gets or sets the default user code snippets plugins path.
        /// </summary>
        string DefaultUserCodeSnippetsPluginsPath { get; set; }

        /// <summary>
        /// Gets or sets the default user code snippets services path.
        /// </summary>
        string DefaultUserCodeSnippetsServicesPath { get; set; }

        /// <summary>
        /// Gets or sets the default user code config plugins path.
        /// </summary>
        string DefaultUserCodeConfigPluginsPath { get; set; }

        /// <summary>
        /// Gets or sets the default user code config services path.
        /// </summary>
        string DefaultUserCodeConfigServicesPath { get; set; }

        /// <summary>
        /// Gets or sets the user plugins path.
        /// </summary>
        string UserPluginsPath { get; set; }

        /// <summary>
        /// Gets or sets the user services path.
        /// </summary>
        string UserServicesPath { get; set; }

        /// <summary>
        /// Gets or sets the user code snippets plugins path.
        /// </summary>
        string UserCodeSnippetsPluginsPath { get; set; }

        /// <summary>
        /// Gets or sets the user code snippets services path.
        /// </summary>
        string UserCodeSnippetsServicesPath { get; set; }

        /// <summary>
        /// Gets or sets the user code config plugins path.
        /// </summary>
        string UserCodeConfigPluginsPath { get; set; }

        /// <summary>
        /// Gets or sets the user code config services path.
        /// </summary>
        string UserCodeConfigServicesPath { get; set; }

        /// <summary>
        /// Gets or sets the type of the selected view.
        /// </summary>
        string SelectedViewType { get; set; }

        /// <summary>
        /// Gets or sets the selected view prefix.
        /// </summary>
        string SelectedViewPrefix { get; set; }

        /// <summary>
        /// Gets or sets the PCL profile.
        /// </summary>
        string PCLProfile { get; set; }

        /// <summary>
        /// Gets or sets the windows phone version.
        /// </summary>
        string WindowsPhoneBuildVersion { get; set; }

        /// <summary>
        /// Gets or sets the ios version.
        /// </summary>
        string iOSBuildVersion { get; set; }

        /// <summary>
        /// Gets or sets the langugage override.
        /// </summary>
        string LanguageOverride { get; set; }

        /// <summary>
        /// Gets or sets the visual studio version.
        /// </summary>
        string VisualStudioVersion { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [show view log file on visual studio menu].
        /// </summary>
        bool ShowViewLogFileOnVisualStudioMenu { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [show clear log file on visual studio menu].
        /// </summary>
        bool ShowClearLogFileOnVisualStudioMenu { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [check for updates].
        /// </summary>
        bool CheckForUpdates { get; set; }

        /// <summary>
        /// Gets or sets the last checked for update date time.
        /// </summary>
        string LastCheckedForUpdateDateTime { get; set; }

        /// <summary>
        /// Gets or sets the latest version on gallery.
        /// </summary>
        string LatestVersionOnGallery { get; set; }

        /// <summary>
        /// Gets the gallery id.
        /// </summary>
        string GalleryId { get; }

        /// <summary>
        /// Gets the ninja coder download URL.
        /// </summary>
        string NinjaCoderDownloadUrl { get; }

        /// <summary>
        /// Gets the update checker path.
        /// </summary>
        string UpdateCheckerPath { get; }

        /// <summary>
        /// Gets or sets the type of the framework.
        /// </summary>
        FrameworkType FrameworkType { get; set;  }

        /// <summary>
        /// Gets a value indicating whether [fix information plist].
        /// </summary>
        bool FixInfoPlist { get; }

        /// <summary>
        /// Gets a value indicating whether [enable xamarin forms].
        /// </summary>

        bool EnableXamarinForms { get;  }

        /// <summary>
        /// Gets a value indicating whether [enable MVVM cross and xamarin forms].
        /// </summary>
        bool EnableMvvmCrossAndXamarinForms { get;  }
    }
}

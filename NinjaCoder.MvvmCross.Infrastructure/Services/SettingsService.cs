// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the SettingsService type.
// </summary>
// ------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Infrastructure.Services
{
    using System;
    using System.IO;

    using Microsoft.Win32;

    /// <summary>
    ///  Defines the SettingsService type.
    /// </summary>
    public class SettingsService : ISettingsService
    {
        /// <summary>
        /// Gets or sets a value indicating whether [log to trace].
        /// </summary>
        public bool LogToTrace
        {
            get { return this.GetRegistryValue("Tracing", "LogToTrace", "N") == "Y"; }
            set { this.SetRegistryValue("Tracing", "LogToTrace", value ? "Y" : "N"); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [log to file].
        /// </summary>
        public bool LogToFile
        {
            get { return this.GetRegistryValue("Tracing", "LogToFile", "N") == "Y"; }
            set { this.SetRegistryValue("Tracing", "LogToFile", value ? "Y" : "N"); }
        }

        /// <summary>
        /// Gets or sets the log file path.
        /// </summary>
        public string LogFilePath 
        {
            get { return this.GetRegistryValue("Tracing", "LogFilePath", Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\ninja-coder-for-mvvmcross.log"); }
            set { this.SetRegistryValue("Tracing", "LogFilePath", value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [include lib folder in projects].
        /// </summary>
        public bool IncludeLibFolderInProjects
        {
            get { return this.GetRegistryValue("Build", "IncludeLibFolderInProjects", "N") == "Y"; }
            set { this.SetRegistryValue("Build", "IncludeLibFolderInProjects", value ? "Y" : "N"); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [use nuget for project templates].
        /// </summary>
        public bool UseNugetForProjectTemplates
        {
            get { return this.GetRegistryValue("Nuget", "UseNugetForProjectTemplates", "N") == "Y"; }
            set { this.SetRegistryValue("Nuget", "UseNugetForProjectTemplates", value ? "Y" : "N"); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [use nuget for plugins].
        /// </summary>
        public bool UseNugetForPlugins
        {
            get { return this.GetRegistryValue("Nuget", "UseNugetForPlugins", "N") == "Y"; }
            set { this.SetRegistryValue("Nuget", "UseNugetForPlugins", value ? "Y" : "N"); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [use nuget for services].
        /// </summary>
        public bool UseNugetForServices
        {
            get { return this.GetRegistryValue("Nuget", "UseNugetForServices", "N") == "Y"; }
            set { this.SetRegistryValue("Nuget", "UseNugetForServices", value ? "Y" : "N"); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [suspend re sharper during build].
        /// </summary>
        public bool SuspendReSharperDuringBuild
        {
            get { return this.GetRegistryValue("Build", "SuspendReSharperDuringBuild", "N") == "Y"; }
            set { this.SetRegistryValue("Build", "SuspendReSharperDuringBuild", value ? "Y" : "N"); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [display errors].
        /// </summary>
        public bool DisplayErrors
        {
            get { return this.GetRegistryValue("Tracing", "DisplayErrors", "N") == "Y"; }
            set { this.SetRegistryValue("Tracing", "DisplayErrors", value ? "Y" : "N"); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [remove default file headers].
        /// </summary>
        public bool RemoveDefaultFileHeaders
        {
            get { return this.GetRegistryValue("Coding Style", "RemoveDefaultFileHeaders", "N") == "Y"; }
            set { this.SetRegistryValue("Coding Style", "RemoveDefaultFileHeaders", value ? "Y" : "N"); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [remove default comments].
        /// </summary>
        public bool RemoveDefaultComments
        {
            get { return this.GetRegistryValue("Coding Style", "RemoveDefaultComments", "N") == "Y"; }
            set { this.SetRegistryValue("Coding Style", "RemoveDefaultComments", value ? "Y" : "N"); }
        }

        /// <summary>
        /// Gets the converters templates path.
        /// </summary>
        public string ConvertersTemplatesPath
        {
            get { return this.GetItemTemplatesPath() + @"\Converters"; }
        }
        
        /// <summary>
        /// Gets the services templates path.
        /// </summary>
        public string ServicesTemplatesPath
        {
            get { return this.GetItemTemplatesPath() + @"\Services"; }
        }

        /// <summary>
        /// Gets the code snippets path.
        /// </summary>
        public string CodeSnippetsPath
        {
            get { return this.InstalledDirectory + @"CodeSnippets\"; }
        }

        /// <summary>
        /// Gets the plugins code snippets path.
        /// </summary>
        public string PluginsCodeSnippetsPath
        {
            get { return this.InstalledDirectory + @"CodeSnippets\Plugins\"; }
        }

        /// <summary>
        /// Gets the services code snippets path.
        /// </summary>
        public string ServicesCodeSnippetsPath
        {
            get { return this.InstalledDirectory + @"CodeSnippets\Services\"; }
        }

        /// <summary>
        /// Gets the plugins config path.
        /// </summary>
        public string PluginsConfigPath
        {
            get { return this.InstalledDirectory + @"Config\Plugins\"; }
        }

        /// <summary>
        /// Gets the services config path.
        /// </summary>
        public string ServicesConfigPath
        {
            get { return this.InstalledDirectory + @"Config\Services\"; }
        }

        /// <summary>
        /// Gets the MVVM cross assemblies path.
        /// </summary>
        public string MvvmCrossAssembliesPath
        {
            get { return this.InstalledDirectory + @"MvvmCross\Assemblies\"; }
        }

        /// <summary>
        /// Gets the config path.
        /// </summary>
        public string ConfigPath
        {
            get { return this.InstalledDirectory + @"Config\"; }
        }

        /// <summary>
        /// Gets the application version.
        /// </summary>
        public string ApplicationVersion 
        { 
            get { return this.GetRegistryValue(string.Empty, "Version", "Unknown"); }
        }

        /// <summary>
        /// Gets the MvvmCross version.
        /// </summary>
        public string MvvmCrossVersion 
        {
            get { return this.GetRegistryValue(string.Empty, "MvvmCross Version", "Unknown"); } 
        }

        /// <summary>
        /// Gets the unit testing assemblies.
        /// </summary>
        public string UnitTestingAssemblies
        {
            get { return this.GetRegistryValue(string.Empty, "UnitTestingAssemblies", "Cirrious.CrossCore"); } 
        }

        /// <summary>
        /// Gets the unit testing init method.
        /// </summary>
        public string UnitTestingInitMethod
        {
            get { return this.GetRegistryValue(string.Empty, "UnitTestingInitMethod", "CreateTestableObject"); } 
        }

        /// <summary>
        /// Gets the view model navigation snippet file.
        /// </summary>
        public string ViewModelNavigationSnippetFile
        {
            get { return this.GetRegistryValue(string.Empty, "ViewModelNavigationSnippetFile", this.CodeSnippetsPath + @"\ViewModelNavigation.xml"); } 
        }

        /// <summary>
        /// Gets or sets a value indicating whether [format function parameters].
        /// </summary>
        public bool FormatFunctionParameters
        {
            get { return this.GetRegistryValue("Coding Style", "FormatFunctionParameters", "N") == "Y"; }
            set { this.SetRegistryValue("Coding Style", "FormatFunctionParameters", value ? "Y" : "N"); }
        }

        /// <summary>
        /// Gets a value indicating whether [replace variables in snippets].
        /// </summary>
        public bool ReplaceVariablesInSnippets
        {
            get { return this.GetRegistryValue(string.Empty, "ReplaceVariablesInSnippets", "Y") == "Y"; }
        }

        /// <summary>
        /// Gets or sets the MVVM cross plugins wiki page.
        /// </summary>
        public string MvvmCrossPluginsWikiPage
        {
            get { return this.GetRegistryValue(string.Empty, "MvvmCrossPluginsWikiPage", "http://github.com/MvvmCross/MvvmCross/wiki/MvvmCross-plugins#existing-mvvmcross-plugins"); }
            set { this.SetRegistryValue(string.Empty, "MvvmCrossPluginsWikiPage", value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [verbose nuget output].
        /// </summary>
        public bool VerboseNugetOutput
        {
            get { return this.GetRegistryValue(string.Empty, "VerboseNugetOutput", "N") == "Y"; }
            set { this.SetRegistryValue(string.Empty, "VerboseNugetOutput", value ? "Y" : "N"); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [debug nuget].
        /// </summary>
        public bool DebugNuget
        {
            get { return this.GetRegistryValue(string.Empty, "DebugNuget", "N") == "Y"; }
            set { this.SetRegistryValue(string.Empty, "DebugNuget", value ? "Y" : "N"); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [remove assemblies if nuget used].
        /// </summary>
        public bool RemoveAssembliesIfNugetUsed
        {
            get { return this.GetRegistryValue(string.Empty, "RemoveAssembliesIfNugetUsed", "Y") == "Y"; }
            set { this.SetRegistryValue(string.Empty, "RemoveAssembliesIfNugetUsed", value ? "Y" : "N"); }
        }

        /// <summary>
        /// Gets the installed directory.
        /// </summary>
        public string InstalledDirectory
        {
            get { return this.GetRegistryValue(string.Empty, "InstalledDirectory", string.Empty); }
        }

        /// <summary>
        /// Gets the build date time.
        /// </summary>
        public string BuildDateTime
        {
            get
            {
                string path = this.InstalledDirectory + @"NinjaCoder.MvvmCross.dll";

                if (File.Exists(path))
                {
                    FileInfo fileInfo = new FileInfo(path);

                    return fileInfo.CreationTime.ToString("dd-MMM-yyyy HH:mm");
                }

                return string.Empty;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [copy assemblies to lib folder].
        /// </summary>
        public bool CopyAssembliesToLibFolder
        {
            get { return this.GetRegistryValue("Build", "CopyAssembliesToLibFolder", "N") == "Y"; }
            set { this.SetRegistryValue("Build", "CopyAssembliesToLibFolder", value ? "Y" : "N"); }
        }

        /// <summary>
        /// Gets a value indicating whether [process template wizards].
        /// </summary>
        public bool ProcessTemplateWizards
        {
            get { return this.GetRegistryValue("Internals", "ProcessTemplateWizards", "Y") == "Y"; }
        }

        /// <summary>
        /// Gets a value indicating whether [process nuget commands].
        /// </summary>
        public bool ProcessNugetCommands
        {
            get { return this.GetRegistryValue("Internals", "ProcessNugetCommands", "Y") == "Y"; }
        }

        /// <summary>
        /// Gets a value indicating whether [beta testing].
        /// </summary>
        public bool BetaTesting
        {
            get { return this.GetRegistryValue("Internals", "BetaTesting", "N") == "Y"; }
        }

        /// <summary>
        /// Gets or sets the default projects path.
        /// </summary>
        public string DefaultProjectsPath
        {
            get { return this.GetRegistryValue(string.Empty, "DefaultProjectsPath", string.Empty); }
            set { this.SetRegistryValue(string.Empty, "DefaultProjectsPath", value); }
        }

        /// <summary>
        /// Gets or sets the theme.
        /// </summary>
        public string Theme
        {
            get { return this.GetRegistryValue("Visual", "Theme", "Light"); }
            set { this.SetRegistryValue("Visual", "Theme", value); }
        }

        /// <summary>
        /// Gets or sets the color of the theme.
        /// </summary>
        public string ThemeColor
        {
            get { return this.GetRegistryValue("Visual", "ThemeColor", "Blue"); }
            set { this.SetRegistryValue("Visual", "ThemeColor", value); }
        }

        /// <summary>
        /// Gets or sets the testing framework.
        /// </summary>
        public string TestingFramework
        {
            get { return this.GetRegistryValue("Projects", "TestingFramework", "NUnit"); }
            set { this.SetRegistryValue("Projects", "TestingFramework", value); }
        }

        public string MockingFramework
        {
            get { return this.GetRegistryValue("Projects", "MockingFramework", "Moq"); }
            set { this.SetRegistryValue("Projects", "MockingFramework", value); }
        }

        /// <summary>
        /// Gets or sets the default users paths set.
        /// </summary>
        public bool DefaultUsersPathsSet
        {
            get { return this.GetRegistryValue(string.Empty, "DefaultUsersPathsSet", "N") == "Y"; }
            set { this.SetRegistryValue(string.Empty, "DefaultUsersPathsSet", value ? "Y" : "N"); }
        }

        /// <summary>
        /// Gets or sets the default user plugins path.
        /// </summary>
        public string DefaultUserPluginsPath
        {
            get { return this.GetRegistryValue("Plugins", "DefaultUserPluginsPath", string.Empty); }
            set { this.SetRegistryValue("Plugins", "DefaultUserPluginsPath", value); }
        }

        /// <summary>
        /// Gets or sets the default user services path.
        /// </summary>
        public string DefaultUserServicesPath
        {
            get { return this.GetRegistryValue("Services", "DefaultUserServicesPath", string.Empty); }
            set { this.SetRegistryValue("Services", "DefaultUserServicesPath", value); }
        }

        /// <summary>
        /// Gets or sets the default user code snippets plugins path.
        /// </summary>
        public string DefaultUserCodeSnippetsPluginsPath
        {
            get { return this.GetRegistryValue("Plugins", "DefaultUserCodeSnippetsPluginsPath", string.Empty); }
            set { this.SetRegistryValue("Plugins", "DefaultUserCodeSnippetsPluginsPath", value); }
        }

        /// <summary>
        /// Gets or sets the default user code snippets services path.
        /// </summary>
        public string DefaultUserCodeSnippetsServicesPath
        {
            get { return this.GetRegistryValue("Services", "DefaultUserCodeSnippetsServicesPath", string.Empty); }
            set { this.SetRegistryValue("Services", "DefaultUserCodeSnippetsServicesPath", value); }
        }

        /// <summary>
        /// Gets or sets the default user code config plugins path.
        /// </summary>
        public string DefaultUserCodeConfigPluginsPath
        {
            get { return this.GetRegistryValue("Plugins", "DefaultUserCodeConfigPluginsPath", string.Empty); }
            set { this.SetRegistryValue("Plugins", "DefaultUserCodeConfigPluginsPath", value); }
        }

        /// <summary>
        /// Gets or sets the default user code config services path.
        /// </summary>
        public string DefaultUserCodeConfigServicesPath
        {
            get { return this.GetRegistryValue("Services", "DefaultUserCodeConfigServicesPath", string.Empty); }
            set { this.SetRegistryValue("Services", "DefaultUserCodeConfigServicesPath", value); }
        }

        /// <summary>
        /// Gets or sets the user plugins path.
        /// </summary>
        public string UserPluginsPath
        {
            get { return this.GetRegistryValue("Plugins", "UserPluginsPath", this.DefaultUserPluginsPath); }
            set { this.SetRegistryValue("Plugins", "UserPluginsPath", value); }
        }

        /// <summary>
        /// Gets or sets the user services path.
        /// </summary>
        public string UserServicesPath
        {
            get { return this.GetRegistryValue("Services", "UserServicesPath", this.DefaultUserServicesPath); }
            set { this.SetRegistryValue("Services", "UserServicesPath", value); }
        }

        /// <summary>
        /// Gets or sets the user code snippets plugins path.
        /// </summary>
        public string UserCodeSnippetsPluginsPath
        {
            get { return this.GetRegistryValue("Plugins", "UserCodeSnippetsPluginsPath", this.DefaultUserCodeSnippetsPluginsPath); }
            set { this.SetRegistryValue("Plugins", "UserCodeSnippetsPluginsPath", value); }
        }

        /// <summary>
        /// Gets or sets the user code snippets services path.
        /// </summary>
        public string UserCodeSnippetsServicesPath
        {
            get { return this.GetRegistryValue("Services", "UserCodeSnippetsServicesPath", this.DefaultUserCodeSnippetsServicesPath); }
            set { this.SetRegistryValue("Services", "UserCodeSnippetsServicesPath", value); }
        }

        /// <summary>
        /// Gets or sets the user code config plugins path.
        /// </summary>
        public string UserCodeConfigPluginsPath
        {
            get { return this.GetRegistryValue("Plugins", "UserCodeConfigPluginsPath", this.DefaultUserCodeConfigPluginsPath); }
            set { this.SetRegistryValue("Plugins", "UserCodeConfigPluginsPath", value); }
        }

        /// <summary>
        /// Gets or sets the user code config services path.
        /// </summary>
        public string UserCodeConfigServicesPath
        {
            get { return this.GetRegistryValue("Services", "UserCodeConfigServicesPath", this.DefaultUserCodeConfigServicesPath); }
            set { this.SetRegistryValue("Services", "UserCodeConfigServicesPath", value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [use pre release nuget packages].
        /// </summary>
        public bool UsePreReleaseNugetPackages 
        {
            get { return this.GetRegistryValue("Nuget", "UsePreReleaseNugetPackages", "N") == "Y"; }
            set { this.SetRegistryValue("Nuget", "UsePreReleaseNugetPackages", value ? "Y" : "N"); }
        }

        /// <summary>
        /// Gets or sets the type of the selected view.
        /// </summary>
        public string SelectedViewType
        {
            get { return this.GetRegistryValue("Internals", "SelectedViewType", "SampleData"); }
            set { this.SetRegistryValue("Internals", "SelectedViewType", value); }
        }

        /// <summary>
        /// Gets or sets the selected view prefix.
        /// </summary>
        public string SelectedViewPrefix
        {
            get { return this.GetRegistryValue("Internals", "SelectedViewPrefix", string.Empty); }
            set { this.SetRegistryValue("Internals", "SelectedViewPrefix", value); }
        }

        /// <summary>
        /// Gets or sets the PCL profile.
        /// </summary>
        public string PCLProfile
        {
            get { return this.GetRegistryValue("Projects", "PCLProfile", "158"); }
            set { this.SetRegistryValue("Projects", "PCLProfile", value); }
        }

        /// <summary>
        /// Gets or sets the windows phone version.
        /// </summary>
        public string WindowsPhoneBuildVersion
        {
            get { return this.GetRegistryValue("Projects", "WindowsPhoneBuildVersion", "8"); }
            set { this.SetRegistryValue("Projects", "WindowsPhoneBuildVersion", value); }
        }

        /// <summary>
        /// Gets or sets the ios version.
        /// </summary>
        public string iOSBuildVersion
        {
            get { return this.GetRegistryValue("Projects", "WindowsPhoneBuildVersion", "7"); }
            set { this.SetRegistryValue("Projects", "WindowsPhoneBuildVersion", value); }
        }

        /// <summary>
        /// Gets or sets the Language override.
        /// </summary>
        public string LanguageOverride
        {
            get { return this.GetRegistryValue("Visual", "LanguageOverride", "Current Culture"); }
            set { this.SetRegistryValue("Visual", "LanguageOverride", value); }
        }

        /// <summary>
        /// Gets or sets the visual studio version.
        /// </summary>
        public string VisualStudioVersion
        {
            get { return this.GetRegistryValue("Internals", "VisualStudioVersion", "11.0"); }
            set { this.SetRegistryValue("Internals", "VisualStudioVersion", value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [show view log file on visual studio menu].
        /// </summary>
        public bool ShowViewLogFileOnVisualStudioMenu
        {
            get { return this.GetRegistryValue("Internals", "ShowViewLogFileOnVisualStudioMenu", "N") == "Y"; }
            set { this.SetRegistryValue("Internals", "ShowViewLogFileOnVisualStudioMenu", value ? "Y" : "N"); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [show clear log file on visual studio menu].
        /// </summary>
        public bool ShowClearLogFileOnVisualStudioMenu
        {
            get { return this.GetRegistryValue("Internals", "ShowClearLogFileOnVisualStudioMenu", "N") == "Y"; }
            set { this.SetRegistryValue("Internals", "ShowClearLogFileOnVisualStudioMenu", value ? "Y" : "N"); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [check for updates].
        /// </summary>
        public bool CheckForUpdates
        {
            get { return this.GetRegistryValue("Internals", "CheckForUpdates", "Y") == "Y"; }
            set { this.SetRegistryValue("Internals", "CheckForUpdates", value ? "Y" : "N"); }
        }

        /// <summary>
        /// Gets or sets the last checked for update date time.
        /// </summary>
        public string LastCheckedForUpdateDateTime
        {
            get { return this.GetRegistryValue("Internals", "LastCheckedForUpdateDateTime", string.Empty); }
            set { this.SetRegistryValue("Internals", "LastCheckedForUpdateDateTime", value); }
        }

        /// <summary>
        /// Gets or sets the latest version on gallery.
        /// </summary>
        public string LatestVersionOnGallery
        {
            get { return this.GetRegistryValue("Internals", "LatestVersionOnGallery", string.Empty); }
            set { this.SetRegistryValue("Internals", "LatestVersionOnGallery", value); }
        }

        /// <summary>
        /// Gets the gallery id.
        /// </summary>
        public string GalleryId
        {
            get { return this.GetRegistryValue("Internals", "GalleryId", "AB2BD8EF-571C-47dc-87D2-6CC966FC1346"); }
        }

        /// <summary>
        /// Gets the ninja coder download URL.
        /// </summary>
        public string NinjaCoderDownloadUrl
        {
            get { return this.GetRegistryValue("Internals", "NinjaCoderDownloadUrl", "http://visualstudiogallery.msdn.microsoft.com/618b51f0-6de8-4f85-95ce-a50c658c7767"); }
        }

        /// <summary>
        /// Gets the update checker path.
        /// </summary>
        public string UpdateCheckerPath
        { 
            get { return this.InstalledDirectory + "NinjaCoder.MvvmCross.UpdateChecker.exe"; }
        }

        /// <summary>
        /// Gets a value indicating whether [add view model and views].
        /// </summary>
        public bool AddViewModelAndViews
        {
            get { return this.GetRegistryValue("Internals", "AddViewModelAndViews", "Y") == "Y"; }
        }

        /// <summary>
        /// Gets or sets the active project.
        /// </summary>
        public string ActiveProject
        {
            get { return this.GetRegistryValue("Internals", "ActiveProject", string.Empty); }
            set { this.SetRegistryValue("Internals", "ActiveProject", value); }
        }
        
        /// <summary>
        /// Gets or sets the plugins to add.
        /// </summary>
        public string PluginsToAdd
        {
            get { return this.GetRegistryValue("Internals", "PluginsToAdd", string.Empty); }
            set { this.SetRegistryValue("Internals", "PluginsToAdd", value); }
        }

        /// <summary>
        /// Gets the registry key.
        /// </summary>
        /// <param name="subKey">The sub key.</param>
        /// <param name="writeable">if set to <c>true</c> [writeable].</param>
        /// <returns>The registry key.</returns>
        internal RegistryKey GetRegistryKey(
            string subKey,
            bool writeable)
        {
            RegistryKey softwareKey = Registry.CurrentUser.OpenSubKey("Software");

            if (softwareKey != null)
            {
                RegistryKey scorchioKey = softwareKey.OpenSubKey("Scorchio Limited");

                if (scorchioKey != null)
                {
                    RegistryKey ninjaKey = scorchioKey.OpenSubKey("Ninja Coder for MvvmCross", writeable);

                    if (ninjaKey != null)
                    {
                        if (string.IsNullOrEmpty(subKey) == false)
                        {
                            RegistryKey registryKey = ninjaKey.OpenSubKey(subKey, writeable);

                            return registryKey ?? ninjaKey.CreateSubKey(subKey);
                        }
                    }

                    return ninjaKey;
                }
            }

            return null;
        }

        /// <summary>
        /// Gets the registry value.
        /// </summary>
        /// <param name="subKey">The sub key.</param>
        /// <param name="name">The name.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns> The value.</returns>
        internal string GetRegistryValue(
            string subKey,
            string name, 
            string defaultValue)
        {
            RegistryKey registryKey = this.GetRegistryKey(subKey, true);

            if (registryKey != null)
            {
                object obj = registryKey.GetValue(name);

                if (obj == null)
                {
                    return defaultValue;
                }

                return (string)obj;
            }

            return defaultValue;
        }

        /// <summary>
        /// Sets the value.
        /// </summary>
        /// <param name="subKey">The sub key.</param>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        internal void SetRegistryValue(
            string subKey,
            string name,
            string value)
        {
             RegistryKey registryKey = this.GetRegistryKey(subKey, true);

             if (registryKey != null)
             {
                 registryKey.SetValue(name, value);
             }
        }

        /// <summary>
        /// Gets the item templates path.
        /// </summary>
        /// <returns>The Item templates path.</returns>
        internal string GetItemTemplatesPath()
        {
            string visualStudioFolder = "Microsoft Visual Studio " + this.VisualStudioVersion;

            return string.Format(
                @"{0}\{1}\Common7\IDE\ItemTemplates\CSharp\MvvmCross", 
                Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), 
                visualStudioFolder);
        }
    }
}

// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the SettingsService type.
// </summary>
// ------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Services
{
    using Microsoft.Win32;

    using NinjaCoder.MvvmCross.Entities;
    using NinjaCoder.MvvmCross.Services.Interfaces;
    using Scorchio.Infrastructure.Extensions;
    using System;
    using System.IO;

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
            ////get { return this.InstalledDirectory + @"CodeSnippets\Plugins\"; }
            get { return "https://raw.githubusercontent.com/asudbury/NinjaCoderForMvvmCross/master/Config/CodeSnippets/Plugins/"; } 
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
            get { return this.GetRegistryValue(string.Empty, "MvvmCrossPluginsWikiPage", "http://github.com/MvvmCross/MvvmCross/wiki/MvvmCross-plugins"); }
        }

        /// <summary>
        /// Gets the nuget website page.
        /// </summary>
        public string NugetWebsitePage 
        {
            get { return this.GetRegistryValue(string.Empty, "NugetWebsitePage", "http://nuget.org"); }
        }

        /// <summary>
        /// Gets the xamarin forms labs nuget packages git hub page.
        /// </summary>
        public string XamarinFormsLabsNugetPackagesGitHubPage
        {
            get { return this.GetRegistryValue(string.Empty, "XamarinFormsLabsNugetPackagesGitHubPage", "https://github.com/XLabs/Xamarin-Forms-Labs/wiki/Nuget-Packages"); }
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
        /// Gets a value indicating whether [process view model and views].
        /// </summary>
        public bool ProcessViewModelAndViews
        {
            get { return this.GetRegistryValue("Internals", "ProcessViewModelAndViews", "Y") == "Y"; }
        }

        /// <summary>
        /// Gets a value indicating whether [process wizard].
        /// </summary>
        public bool ProcessWizard
        {
            get { return this.GetRegistryValue("Internals", "ProcessWizard", "Y") == "Y"; }
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
        /// Gets or sets the PCL profile.
        /// </summary>
        public string PCLProfile
        {
            get { return this.GetRegistryValue("Projects", "PCLProfile", "78"); }
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
        /// Gets or sets the API version.
        /// </summary>
        public string iOSApiVersion
        {
            get { return this.GetRegistryValue("Projects", "iOSApiVersion", "Classic"); }
            set { this.SetRegistryValue("Projects", "iOSApiVersion", value); }
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
        /// Gets or sets the type of the framework.
        /// </summary>
        public FrameworkType FrameworkType
        {
            get
            {
                string value = this.GetRegistryValue("Internals", "FrameworkType", "MvvmCross");
                return FrameworkType.MvvmCross.GetValueFromDescription<FrameworkType>(value);
            }
            set
            {
                this.SetRegistryValue("Internals", "FrameworkType", value.GetDescription());
            }
        }

        /// <summary>
        /// Gets a value indicating whether [fix information plist].
        /// </summary>
        public bool FixInfoPlist
        {
           get { return this.GetRegistryValue("Internals", "FixInfoPlist", "Y") == "Y"; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [use local uris].
        /// </summary>
        public bool UseLocalUris
        {
            get { return this.GetRegistryValue("Build", "UseLocalUris", "N") == "Y"; }
            set { this.SetRegistryValue("Build", "UseLocalUris", value ? "Y" : "N"); }
       }

        /// <summary>
        /// Gets the mvvmcross plugins URI.
        /// </summary>
        public string MvvmCrossPluginsUri
        {
            get
            {
                return this.UseLocalUris ? 
                    this.GetRegistryValue("Internals", "MvvmCrossPluginsUri", this.InstalledDirectory + "MvvmCrossPlugins.xml") :
                    this.GetRegistryValue("Internals", "MvvmCrossPluginsUri", "https://raw.githubusercontent.com/asudbury/NinjaCoderForMvvmCross/master/Config/MvvmCrossPlugins.xml");
            }
        }

        /// <summary>
        /// Gets the nuget pacakges URI.
        /// </summary>
        public string NugetPackagesUri
        {
            get
            {
                return this.UseLocalUris ?
                    this.GetRegistryValue("Internals", "NugetPackagesUri", this.InstalledDirectory + "NugetPackages.xml") : 
                    this.GetRegistryValue("Internals", "NugetPackagesUri", "https://raw.githubusercontent.com/asudbury/NinjaCoderForMvvmCross/master/Config/NugetPackages.xml");
            }
        }

        /// <summary>
        /// Gets the mvvmcross plugins URI.
        /// </summary>
        public string XamarinFormsLabsPluginsUri
        {
            get
            {
                return this.UseLocalUris ?
                    this.GetRegistryValue("Internals", "XamarinFormsLabsPluginsUri", this.InstalledDirectory + "XamarinFormsLabsPlugins.xml") :
                    this.GetRegistryValue("Internals", "XamarinFormsLabsPluginsUri", "https://raw.githubusercontent.com/asudbury/NinjaCoderForMvvmCross/master/Config/XamarinFormsLabsPlugins.xml");
            }
        }

        /// <summary>
        /// Gets or sets the xamarin forms views.
        /// </summary>
        public string XamarinFormsViews 
        {
            get { return this.GetRegistryValue("Internals", "XamarinFormsViews", string.Empty); }
            set { this.SetRegistryValue("Internals", "XamarinFormsViews", value); }
        }
        
        /// <summary>
        /// Gets or sets a value indicating whether [output nuget commands to read me].
        /// </summary>
        public bool OutputNugetCommandsToReadMe
        {
            get { return this.GetRegistryValue("Build", "OutputNugetCommandsToReadMe", "N") == "Y"; }
            set { this.SetRegistryValue("Build", "OutputNugetCommandsToReadMe", value ? "Y" : "N"); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [output errors to read me].
        /// </summary>
        public bool OutputErrorsToReadMe
        {
            get { return this.GetRegistryValue("Build", "OutputErrorsToReadMe", "N") == "Y"; }
            set { this.SetRegistryValue("Build", "OutputErrorsToReadMee", value ? "Y" : "N"); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [use pre release MVVM cross nuget packages].
        /// </summary>
        public bool UsePreReleaseMvvmCrossNugetPackages 
        {
            get { return this.GetRegistryValue("Build", "UsePreReleaseMvvmCrossNugetPackages", "N") == "Y"; }
            set { this.SetRegistryValue("Build", "UsePreReleaseMvvmCrossNugetPackages", value ? "Y" : "N"); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [use pre release xamarin forms nuget packages].
        /// </summary>
        public bool UsePreReleaseXamarinFormsNugetPackages
        {
            get { return this.GetRegistryValue("Build", "UsePreReleaseXamarinFormsNugetPackages", "N") == "Y"; }
            set { this.SetRegistryValue("Build", "UsePreReleaseXamarinFormsNugetPackages", value ? "Y" : "N"); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [bind context in xaml for xamarin forms].
        /// </summary>
        public bool BindContextInXamlForXamarinForms 
        {
            get { return this.GetRegistryValue("Coding Style", "BindContextInXamlForXamarinForms", "Y") == "Y"; }
            set { this.SetRegistryValue("Coding Style", "BindContextInXamlForXamarinForms", value ? "Y" : "N"); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [create test projects for all projects].
        /// </summary>
        public bool CreatePlatformTestProjects
        {
            get { return this.GetRegistryValue("Build", "CreatePlatformTestProjects", "N") == "Y"; }
            set { this.SetRegistryValue("Build", "CreatePlatformTestProjects", value ? "Y" : "N"); }
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

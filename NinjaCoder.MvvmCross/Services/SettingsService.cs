// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the SettingsService type.
// </summary>
// ------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Services
{
    using Microsoft.Win32;
    using Entities;
    using Interfaces;
    using Scorchio.Infrastructure.Extensions;
    using System;
    using System.IO;

    /// <summary>
    ///  Defines the SettingsService type.
    /// </summary>
    public class SettingsService : ISettingsService
    {
        /// <summary>
        /// The working directory.
        /// </summary>
        private string workingDirectory;

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
        /// Gets or sets the error file path.
        /// </summary>
        public string ErrorFilePath
        {
            get { return this.GetRegistryValue("Tracing", "ErrorFilePath", Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\ninja-coder-for-mvvmcross-errors.log"); }
            set { this.SetRegistryValue("Tracing", "ErrorFilePath", value); }
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
        /// Gets or sets a value indicating whether [remove this pointer].
        /// </summary>
        public bool RemoveThisPointer
        {
            get { return this.GetRegistryValue("Coding Style", "RemoveThisPointer", "N") == "Y"; }
            set { this.SetRegistryValue("Coding Style", "RemoveThisPointer", value ? "Y" : "N"); }
        }

        /// <summary>
        /// Gets the config path.
        /// </summary>
        public string ConfigPath
        {
            get { return this.WorkingDirectory + @"Config\"; }
        }

        /// <summary>
        /// Gets the application version.
        /// </summary>
        public string ApplicationVersion 
        { 
            get { return this.GetVersion(); }
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
        /// Gets the MVVM cross home page.
        /// </summary>
        public string MvvmCrossHomePage
        {
            get { return this.GetRegistryValue(string.Empty, "MvvmCrossHomePage", "http://mvvmcross.com"); }
        }
        
        /// <summary>
        /// Gets or sets the xamarin forms home page.
        /// </summary>
        public string XamarinFormsHomePage
        {
            get { return this.GetRegistryValue(string.Empty, "XamarinFormsHomePage", "http://xamarin.com/forms"); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [add core tests project].
        /// </summary>
        public bool AddCoreTestsProject 
        {
            get { return this.GetRegistryValue("Add Projects", "CoreTestsProject", "N") == "Y"; }
            set { this.SetRegistryValue("Add Projects", "CoreTestsProject", value ? "Y" : "N"); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [add android project].
        /// </summary>
        public bool AddAndroidProject
        {
            get { return this.GetRegistryValue("Add Projects", "AndroidProject", "Y") == "Y"; }
            set { this.SetRegistryValue("Add Projects", "AndroidProject", value ? "Y" : "N"); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [addi os project].
        /// </summary>
        public bool AddiOSProject
        {
            get { return this.GetRegistryValue("Add Projects", "iOSProject", "Y") == "Y"; }
            set { this.SetRegistryValue("Add Projects", "iOSProject", value ? "Y" : "N"); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [add windows phone project].
        /// </summary>
        public bool AddWindowsPhoneProject
        {
            get { return this.GetRegistryValue("Add Projects", "WindowsPhoneProject", "Y") == "Y"; }
            set { this.SetRegistryValue("Add Projects", "WindowsPhoneProject", value ? "Y" : "N"); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [add WPF project].
        /// </summary>
        public bool AddWpfProject
        {
            get { return this.GetRegistryValue("Add Projects", "WpfProject", "N") == "Y"; }
            set { this.SetRegistryValue("Add Projects", "WpfProject", value ? "Y" : "N"); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [add xamarin forms tests project].
        /// </summary>
        public bool AddXamarinFormsTestsProject
        {
            get { return this.GetRegistryValue("Add Projects", "XamarinFormsTestsProject", "N") == "Y"; }
            set { this.SetRegistryValue("Add Projects", "XamarinFormsTestsProject", value ? "Y" : "N"); }
        }

        /// <summary>
        /// Gets or sets the type of the selected MVVM cross ios view type
        /// </summary>
        public string SelectedMvvmCrossiOSViewType
        {
            get { return this.GetRegistryValue("Build", "SelectedMvvmCrossiOSViewType", "Xib"); }
            set { this.SetRegistryValue("Build", "SelectedMvvmCrossiOSViewType", value); }
        }

        /// <summary>
        /// Gets the MVVM cross ios sample data web page.
        /// </summary>
        public string MvvmCrossiOSViewWebPage
        {
            get { return this.GetRegistryValue(string.Empty, "MvvmCrossiOSViewWebPage", "http://kerry.lothrop.de/ios-ui-with-mvvmcross"); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [add projects skip application options].
        /// </summary>
        public bool AddProjectsSkipApplicationOptions
        {
            get { return this.GetRegistryValue("Build", "AddProjectsSkipApplicationOptions", "N") == "Y"; }
            set { this.SetRegistryValue("Build", "AddProjectsSkipApplicationOptions", value ? "Y" : "N"); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [add projects skip ninja coder options].
        /// </summary>
        public bool AddProjectsSkipNinjaCoderOptions
        {
            get { return this.GetRegistryValue("Build", "AddProjectsSkipNinjaCoderOptions", "N") == "Y"; }
            set { this.SetRegistryValue("Build", "AddProjectsSkipNinjaCoderOptions", value ? "Y" : "N"); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [add projects skip MVVM cross plugin options].
        /// </summary>
        public bool AddProjectsSkipMvvmCrossPluginOptions
        {
            get { return this.GetRegistryValue("Build", "AddProjectsSkipMvvmCrossPluginOptions", "N") == "Y"; }
            set { this.SetRegistryValue("Build", "AddProjectsSkipMvvmCrossPluginOptions", value ? "Y" : "N"); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [add projects skip nuget package options].
        /// </summary>
        public bool AddProjectsSkipNugetPackageOptions
        {
            get { return this.GetRegistryValue("Build", "AddProjectsSkipNugetPackageOptions", "N") == "Y"; }
            set { this.SetRegistryValue("Build", "AddProjectsSkipNugetPackageOptions", value ? "Y" : "N"); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [add projects skip view options].
        /// </summary>
        public bool AddProjectsSkipViewOptions
        {
            get { return this.GetRegistryValue("Build", "AddProjectsSkipViewOptions", "N") == "Y"; }
            set { this.SetRegistryValue("Build", "AddProjectsSkipViewOptions", value ? "Y" : "N"); }
        }

        /// <summary>
        /// Gets or sets the default type of the view.
        /// </summary>
        public string DefaultViewType
        {
            get { return this.GetRegistryValue("Build", "DefaultViewType", "SampleData"); }
            set { this.SetRegistryValue("Build", "DefaultViewType", value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [output text template content to trace file].
        /// </summary>
        public bool OutputTextTemplateContentToTraceFile
        {
            get { return this.GetRegistryValue("Tracing", "OutputTextTemplateContentToTraceFile", "N") == "Y"; }
            set { this.SetRegistryValue("Tracing", "OutputTextTemplateContentToTraceFile", value ? "Y" : "N"); }
        }

        /// <summary>
        /// Gets or sets the start up project.
        /// </summary>
        public string StartUpProject
        {
            get { return this.GetRegistryValue("Build", "StartUpProject", string.Empty); }
            set { this.SetRegistryValue("Build", "StartUpProject", value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [use xamarin forms xaml compiliation].
        /// </summary>
        public bool UseXamarinFormsXamlCompilation
        {
            get { return this.GetRegistryValue("Build", "UseXamarinFormsXamlCompilation", "Y") == "Y"; }
            set { this.SetRegistryValue("Build", "UseXamarinFormsXamlCompilation", value ? "Y" : "N"); }
        }

        /// <summary>
        /// Gets the MVVM cross plugins wiki page.
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
        /// Gets the build date time.
        /// </summary>
        public string BuildDateTime
        {
            get
            {
                string path = this.WorkingDirectory + @"NinjaCoder.MvvmCross.dll";

                if (File.Exists(path))
                {
                    FileInfo fileInfo = new FileInfo(path);

                    return fileInfo.CreationTime.ToString("dd-MMM-yyyy HH:mm");
                }

                return "Not Known";
            }
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
        }

        /// <summary>
        /// Gets or sets the testing framework.
        /// </summary>
        public string TestingFramework
        {
            get { return this.GetRegistryValue("Projects", "TestingFramework", "NUnit"); }
            set { this.SetRegistryValue("Projects", "TestingFramework", value); }
        }

        /// <summary>
        /// Gets or sets the mocking framework.
        /// </summary>
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
            get { return this.GetRegistryValue("Projects", "PCLProfile", "259"); }
        }

        /// <summary>
        /// Gets the windows phone build version.
        /// </summary>
        public string WindowsPhoneBuildVersion
        {
            get { return this.GetRegistryValue("Projects", "WindowsPhoneBuildVersion", "8"); }
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
            get { return this.GetRegistryValue("Internals", "VisualStudioVersion", "14.0"); }
            set { this.SetRegistryValue("Internals", "VisualStudioVersion", value); }
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
                    this.GetRegistryValue("Internals", "MvvmCrossPluginsUri", this.ConfigPath + "MvvmCrossPlugins.xml") :
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
                    this.GetRegistryValue("Internals", "NugetPackagesUri", this.ConfigPath + "NugetPackages.xml") : 
                    this.GetRegistryValue("Internals", "NugetPackagesUri", "https://raw.githubusercontent.com/asudbury/NinjaCoderForMvvmCross/master/Config/NugetPackages.xml");
            }
        }

        /// <summary>
        /// Gets the xamarin forms nuget pacakges URI.
        /// </summary>
        public string XamarinFormsNugetPackagesUri
        {
            get
            {
                return this.UseLocalUris ?
                    this.GetRegistryValue("Internals", "XamarinFormsNugetPackagesUri", this.ConfigPath + "XamarinFormsNugetPackages.xml") :
                    this.GetRegistryValue("Internals", "XamarinFormsNugetPackagesUri", "https://raw.githubusercontent.com/asudbury/NinjaCoderForMvvmCross/master/Config/XamarinFormsNugetPackages.xml");
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
                    this.GetRegistryValue("Internals", "XamarinFormsLabsPluginsUri", this.ConfigPath + "XamarinFormsLabsPlugins.xml") :
                    this.GetRegistryValue("Internals", "XamarinFormsLabsPluginsUri", "https://raw.githubusercontent.com/asudbury/NinjaCoderForMvvmCross/master/Config/XamarinFormsLabsPlugins.xml");
            }
        }

        /// <summary>
        /// Gets the ninja nuget packages URI.
        /// </summary>
        public string NinjaNugetPackagesUri
        {
            get
            {
                return this.UseLocalUris ?
                    this.GetRegistryValue("Internals", "NinjaNugetPackagesUri", this.ConfigPath + "NinjaNugetPackages.xml") :
                    this.GetRegistryValue("Internals", "NinjaNugetPackagesUri", "https://raw.githubusercontent.com/asudbury/NinjaCoderForMvvmCross/master/Config/NinjaNugetPackages.xml");

            }
        }

        /// <summary>
        /// Gets the ninja community nuget packages URI.
        /// </summary>
        public string NinjaCommunityNugetPackagesUri
        {
            get
            {
                return this.UseLocalUris ?
                    this.GetRegistryValue("Internals", "NinjaCommunityNugetPackagesUri", this.ConfigPath + "NinjaCommunityNugetPackages.xml") :
                    this.GetRegistryValue("Internals", "NinjaCommunityNugetPackagesUri", "https://raw.githubusercontent.com/asudbury/NinjaCoderForMvvmCross/master/Config/NinjaCommunityNugetPackages.xml");
            }
        }

        /// <summary>
        /// Gets the local nuget packages URI.
        /// </summary>
        public string LocalNugetPackagesUri
        {
            get { return this.GetRegistryValue("Internals", "LocalNugetPackagesUri", this.ConfigPath + "LocalNugetPackages.xml"); }
        }

        /// <summary>
        /// Gets the application commands URI.
        /// </summary>
        public string ApplicationCommandsUri
        {
            get
            {
                return this.UseLocalUris ?
                    this.GetRegistryValue("Internals", "ApplicationCommandsUri", this.ConfigPath + "ApplicationCommands.xml") :
                    this.GetRegistryValue("Internals", "ApplicationCommandsUri", "https://raw.githubusercontent.com/asudbury/NinjaCoderForMvvmCross/master/Config/ApplicationCommands.xml");
            }
        }
        
        /// <summary>
        /// Gets or sets a value indicating whether [output nuget commands to read me].
        /// </summary>
        public bool OutputNugetCommandsToReadMe
        {
            get { return this.GetRegistryValue("Build", "OutputNugetCommandsToReadMe", "Y") == "Y"; }
            set { this.SetRegistryValue("Build", "OutputNugetCommandsToReadMe", value ? "Y" : "N"); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [output errors to read me].
        /// </summary>
        public bool OutputErrorsToReadMe
        {
            get { return this.GetRegistryValue("Build", "OutputErrorsToReadMe", "N") == "Y"; }
            set { this.SetRegistryValue("Build", "OutputErrorsToReadMe", value ? "Y" : "N"); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [output read me to log file].
        /// </summary>
        public bool OutputReadMeToLogFile 
        {
            get { return this.GetRegistryValue("Build", "OutputReadMeToLogFile", "Y") == "Y"; }
            set { this.SetRegistryValue("Build", "OutputReadMeToLogFile", value? "Y" : "N"); }
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
        /// Gets or sets a value indicating whether [use pre release ninja nuget packages].
        /// </summary>
        public bool UsePreReleaseNinjaNugetPackages
        {
            get { return this.GetRegistryValue("Build", "UsePreReleaseNinjaNugetPackages", "N") == "Y"; }
            set { this.SetRegistryValue("Build", "UsePreReleaseNinjaNugetPackages", value ? "Y" : "N"); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [bind context in xaml for xamarin forms].
        /// </summary>
        public bool BindContextInXamlForXamarinForms 
        {
            get { return this.GetRegistryValue("Internals", "BindContextInXamlForXamarinForms", "Y") == "Y"; }
            set { this.SetRegistryValue("Internals", "BindContextInXamlForXamarinForms", value ? "Y" : "N"); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [bind xaml for xamarin forms].
        /// </summary>
        public bool BindXamlForXamarinForms
        {
            get { return this.GetRegistryValue("Internals", "BindXamlForXamarinForms", "Y") == "Y"; }
            set { this.SetRegistryValue("Internals", "BindXamlForXamarinForms", value ? "Y" : "N"); }
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
        /// Gets or sets a value indicating whether [use xamarin test cloud].
        /// </summary>
        public bool UseXamarinTestCloud
        {
            get { return this.GetRegistryValue("Build", "UseXamarinTestCloud", "N") == "Y"; }
            set { this.SetRegistryValue("Build", "UseXamarinTestCloud", value ? "Y" : "N"); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [use xamarin insights].
        /// </summary>
        public bool UseXamarinInsights
        {
            get { return this.GetRegistryValue("Build", "UseXamarinInsights", "N") == "Y"; }
            set { this.SetRegistryValue("Build", "UseXamarinInsights", value ? "Y" : "N"); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [use style cop].
        /// </summary>
        public bool UseStyleCop
        {
            get { return this.GetRegistryValue("Build", "UseStyleCop", "N") == "Y"; }
            set { this.SetRegistryValue("Build", "UseStyleCop", value ? "Y" : "N"); }
        }

        /// <summary>
        /// Gets the xamarin pages help.
        /// </summary>
        public string XamarinPagesHelp
        {
            get { return this.GetRegistryValue(string.Empty, "XamarinPagesHelp", "http://developer.xamarin.com/guides/cross-platform/xamarin-forms/controls/pages/"); }
        }

        /// <summary>
        /// Gets the xamarin layouts help.
        /// </summary>
        public string XamarinLayoutsHelp
        {
            get { return this.GetRegistryValue(string.Empty, "XamarinLayoutsHelp", "http://developer.xamarin.com/guides/cross-platform/xamarin-forms/controls/layouts/"); }
        }

        /// <summary>
        /// Gets a value indicating whether [use temporary project name].
        /// </summary>
        public bool UseTempProjectName
        {
            get { return this.GetRegistryValue("Internals", "UseTempProjectName", "N") == "Y"; }
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
        /// Gets a value indicating whether [use local nuget].
        /// </summary>
        public bool UseLocalNuget
        {
            get { return this.GetRegistryValue("Build", "UseLocalNuget", "N") == "Y"; }
        }

        /// <summary>
        /// Gets the name of the local nuget.
        /// </summary>
        public string LocalNugetName
        {
            get { return this.GetRegistryValue("Build", "LocalNugetName", "ProGetLocal"); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [create test projects solution folder].
        /// </summary>
        public bool CreateTestProjectsSolutionFolder
        {
            get { return this.GetRegistryValue("Build", "CreateTestProjectsSolutionFolder", "N") == "Y"; }
            set { this.SetRegistryValue("Build", "CreateTestProjectsSolutionFolder", value ? "Y" : "N"); }
        }

        /// <summary>
        /// Gets or sets the name of the test projects solution folder.
        /// </summary>
        public string TestProjectsSolutionFolderName 
        {
            get { return this.GetRegistryValue("Build", "TestProjectsSolutionFolderName", "Tests"); }
            set { this.SetRegistryValue("Build", "TestProjectsSolutionFolderName", value); }
        }

        /// <summary>
        /// Gets or sets the working directory.
        /// </summary>
        public string WorkingDirectory
        {
            get
            {
                return this.workingDirectory;
            }

            set
            {
                string directory = value;

                if (directory.EndsWith(@"\") == false)
                {
                    directory += @"\";

                }

                this.workingDirectory = directory;
            }
        }

        /// <summary>
        /// Gets a value indicating whether [use local text templates].
        /// </summary>
        public bool UseLocalTextTemplates
        {
            get { return this.GetRegistryValue("Build", "UseLocalTextTemplates", "N") == "Y"; }
            set { this.SetRegistryValue("Build", "UseLocalTextTemplates", value ? "Y" : "N"); }
        }

        /// <summary>
        /// Gets the local text templates directory.
        /// </summary>
        public string LocalTextTemplatesDirectory
        {
            get { return this.GetRegistryValue("Build", "LocalTextTemplatesDirectory", @"B:\Scorchio\Projects\c#\NinjaCoderForMvvmCross\Config\TextTemplates"); }
        }

        /// <summary>
        /// Gets the github templates directory.
        /// </summary>
        public string GithubTemplatesDirectory
        {
            get { return "http://raw.githubusercontent.com/asudbury/NinjaCoderForMvvmCross/master/Config/TextTemplates"; }
        }

        /// <summary>
        /// Gets the idependency text template.
        /// </summary>
        public string IDependencyTextTemplate 
        {
            get
            {
                string path = this.GithubTemplatesDirectory;
                
                if (this.UseLocalTextTemplates)
                {
                    path = this.LocalTextTemplatesDirectory;
                }

                string file = this.GetRegistryValue("Build", "IDependencyTextTemplate", @"DependencyService\IDependencyService.t4");

                return string.Format(@"{0}\{1}", path, file);
            }
        }

        /// <summary>
        /// Gets the dependency text template.
        /// </summary>
        public string DependencyTextTemplate
        {
            get
            {
                string path = this.GithubTemplatesDirectory;

                if (this.UseLocalTextTemplates)
                {
                    path = this.LocalTextTemplatesDirectory;
                }

                string file = this.GetRegistryValue("Build", "DependencyTextTemplate", @"DependencyService\DependencyService.t4");

                return string.Format(@"{0}\{1}", path, file);
            }
        }

        /// <summary>
        /// Gets or sets the dependency directory.
        /// </summary>
        public string DependencyDirectory
        {
            get { return this.GetRegistryValue("Build", "DependencyDirectory", "DependencyServices"); }
            set { this.SetRegistryValue("Build", "DependencyDirectory", value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [automatically add serviceto dependency].
        /// </summary>
        public bool AutomaticallyAddServicetoDependency
        {
            get { return this.GetRegistryValue("Build", "AutomaticallyAddServicetoDependency", "Y") == "Y"; }
            set { this.SetRegistryValue("Build", "AutomaticallyAddServicetoDependency", value ? "Y" : "N"); }
        }

        /// <summary>
        /// Gets the base custom renderer text template.
        /// </summary>
        public string BaseCustomRendererTextTemplate
        {
            get
            {
                string path = this.GithubTemplatesDirectory;

                if (this.UseLocalTextTemplates)
                {
                    path = this.LocalTextTemplatesDirectory;
                }

                string file = this.GetRegistryValue("Build", "BaseCustomRendererTextTemplate", @"CustomRenderer\BaseCustomRenderer.t4");

                return string.Format(@"{0}\{1}", path, file);
            }
        }

        /// <summary>
        /// Gets the custom renderer text template.
        /// </summary>
        public string CustomRendererTextTemplate
        {
            get
            {
                string path = this.GithubTemplatesDirectory;

                if (this.UseLocalTextTemplates)
                {
                    path = this.LocalTextTemplatesDirectory;
                }

                string file = this.GetRegistryValue("Build", "CustomRendererTextTemplate", @"CustomRenderer\CustomRenderer.t4");

                return string.Format(@"{0}\{1}", path, file);
            }
        }

        /// <summary>
        /// Gets the effects text template.
        /// </summary>
        public string EffectsTextTemplate
        {
            get
            {
                string path = this.GithubTemplatesDirectory;

                if (this.UseLocalTextTemplates)
                {
                    path = this.LocalTextTemplatesDirectory;
                }

                string file = this.GetRegistryValue("Build", "EffectsTextTemplate", @"Effects\Effect.t4");

                return string.Format(@"{0}\{1}", path, file);
            }
        }

        /// <summary>
        /// Gets or sets the customer renderer directory.
        /// </summary>
        public string CustomRendererDirectory
        {
            get { return this.GetRegistryValue("Build", "CustomerRendererDirectory", "CustomRenderers"); }
            set { this.SetRegistryValue("Build", "CustomerRendererDirectory", value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [automatically add renderer].
        /// </summary>
        public bool AutomaticallyAddRenderer
        {
            get { return this.GetRegistryValue("Build", "AutomaticallyAddRenderer", "Y") == "Y"; }
            set { this.SetRegistryValue("Build", "AutomaticallyAddRenderer", value ? "Y" : "N"); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [automatically add effect].
        /// </summary>
        public bool AutomaticallyAddEffect
        {
            get { return this.GetRegistryValue("Build", "AutomaticallyAddEffect", "Y") == "Y"; }
            set { this.SetRegistryValue("Build", "AutomaticallyAddEffect", value ? "Y" : "N"); }
        }

        /// <summary>
        /// Gets or sets the effect directory.
        /// </summary>
        public string EffectDirectory
        {
            get { return this.GetRegistryValue("Build", "EffectDirectory", "Effects"); }
            set { this.SetRegistryValue("Build", "EffectDirectory", value); }
        }

        /// <summary>
        /// Gets the xamarin forms custom renderers URI.
        /// </summary>
        public string XamarinFormsCustomRenderersUri
        {
            get
            {
                return this.UseLocalUris ?
                    this.GetRegistryValue("Internals", "XamarinFormsCustomRenderersUri", this.ConfigPath + "XamarinFormsCustomRenderers.xml") :
                    this.GetRegistryValue("Internals", "XamarinFormsCustomRenderersUri", "https://raw.githubusercontent.com/asudbury/NinjaCoderForMvvmCross/master/Config/XamarinFormsCustomRenderers.xml");
            }            
        }

        /// <summary>
        /// Gets the dependency services web page.
        /// </summary>
        public string DependencyServicesWebPage
        {
            get { return this.GetRegistryValue("Internals", "DependencyServicesWebPage", "http://developer.xamarin.com/guides/cross-platform/xamarin-forms/dependency-service"); }
        }

        /// <summary>
        /// Gets or sets the item templates directory.
        /// </summary>
        public string ItemTemplatesDirectory
        {
            get
            {
                string path = this.GithubTemplatesDirectory;

                if (this.UseLocalTextTemplates)
                {
                    path = this.LocalTextTemplatesDirectory;
                }

                string file = this.GetRegistryValue("Build", "ItemTemplatesDirectory", "ItemTemplates");

                return string.Format(@"{0}\{1}", path, file);
            }
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
        /// Gets the version.
        /// </summary>
        /// <returns></returns>
        internal string GetVersion()
        {
            RegistryKey softwareKey = Registry.CurrentUser.OpenSubKey("Software");

            if (softwareKey != null)
            {
                RegistryKey microsoftKey = softwareKey.OpenSubKey("Microsoft");

                if (microsoftKey != null)
                {
                    RegistryKey vsKey = microsoftKey.OpenSubKey("VisualStudio");

                    if (vsKey != null)
                    {
                        RegistryKey versionKey = vsKey.OpenSubKey("14.0");

                        if (versionKey != null)
                        {
                            RegistryKey extensionManagerKey = versionKey.OpenSubKey("ExtensionManager");

                            if (extensionManagerKey != null)
                            {
                                RegistryKey enabledExtensionsKey = extensionManagerKey.OpenSubKey("EnabledExtensions");

                                if (enabledExtensionsKey != null)
                                {
                                    string[] valueNames = enabledExtensionsKey.GetValueNames();

                                    foreach (string valueName in valueNames)
                                    {
                                        if (valueName.StartsWith("NinjaCoderMvvmCross.vsix"))
                                        {
                                            string[] parts = valueName.Split(',');

                                            return parts[1];
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return string.Empty;
        }
    }
}

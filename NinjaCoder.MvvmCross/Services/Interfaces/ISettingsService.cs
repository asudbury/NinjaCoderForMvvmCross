// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ISettingsService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Services.Interfaces
{
    using Entities;

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
        /// Gets or sets the error file path.
        /// </summary>
        string ErrorFilePath { get; set; }

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
        /// Gets or sets a value indicating whether [remove this pointer].
        /// </summary>
        bool RemoveThisPointer { get; set; }

        /// <summary>
        /// Gets the config path.
        /// </summary>
        string ConfigPath { get; }

        /// <summary>
        /// Gets the application version.
        /// </summary>
        string ApplicationVersion { get; }

        /// <summary>
        /// Gets or sets a value indicating whether [format function parameters].
        /// </summary>
        bool FormatFunctionParameters { get; set; }

        /// <summary>
        /// Gets the MVVM cross home page.
        /// </summary>
        string MvvmCrossHomePage { get; }
        
        /// <summary>
        /// Gets the MVVM cross plugins wiki page.
        /// </summary>
        string MvvmCrossPluginsWikiPage { get;  }

        /// <summary>
        /// Gets the nuget website page.
        /// </summary>
        string NugetWebsitePage { get; }

        /// <summary>
        /// Gets the xamarin forms labs nuget packages git hub page.
        /// </summary>
        string XamarinFormsLabsNugetPackagesGitHubPage { get; }

        /// <summary>
        /// Gets the build date time.
        /// </summary>
        string BuildDateTime { get; }

        /// <summary>
        /// Gets or sets the active project.
        /// </summary>
        string ActiveProject { get; set; }

        /// <summary>
        /// Gets a value indicating whether [process nuget commands].
        /// </summary>
        bool ProcessNugetCommands { get; }

        /// <summary>
        /// Gets or sets a value indicating whether [beta testing].
        /// </summary>
        bool BetaTesting { get; set; }

        /// <summary>
        /// Gets or sets the default projects path.
        /// </summary>
        string DefaultProjectsPath { get; set; }
        
        /// <summary>
        /// Gets the theme.
        /// </summary>
        string Theme { get; }

        /// <summary>
        /// Gets the color of the theme.
        /// </summary>
        string ThemeColor { get; }

        /// <summary>
        /// Gets or sets the testing framework.
        /// </summary>
        string TestingFramework { get; set; }

        /// <summary>
        /// Gets or sets the mocking framework.
        /// </summary>
        string MockingFramework { get; set; }

        /// <summary>
        /// Gets the PCL profile.
        /// </summary>
        string PCLProfile { get; }

        /// <summary>
        /// Gets the windows phone build version.
        /// </summary>
        string WindowsPhoneBuildVersion { get; }

        /// <summary>
        /// Gets or sets the visual studio version.
        /// </summary>
        string VisualStudioVersion { get; set; }

        /// <summary>
        /// Gets or sets the type of the framework.
        /// </summary>
        FrameworkType FrameworkType { get; set;  }

        /// <summary>
        /// Gets a value indicating whether [fix information plist].
        /// </summary>
        bool FixInfoPlist { get; }

        /// <summary>
        /// Gets or sets a value indicating whether [use local uris].
        /// </summary>
        bool UseLocalUris { get; set; }

        /// <summary>
        /// Gets the mvvmcross plugins URI.
        /// </summary>
        string MvvmCrossPluginsUri { get; }

        /// <summary>
        /// Gets the nuget pacakges URI.
        /// </summary>
        string NugetPackagesUri { get; }

        /// <summary>
        /// Gets the xamarin forms nuget pacakges URI.
        /// </summary>
        string XamarinFormsNugetPackagesUri { get; }

        /// <summary>
        /// Gets the xamarin forms labs plugins URI.
        /// </summary>
        string XamarinFormsLabsPluginsUri { get; }

        /// <summary>
        /// Gets the ninja nuget packages URI.
        /// </summary>
        string NinjaNugetPackagesUri { get; }

        /// <summary>
        /// Gets the ninja community nuget packages URI.
        /// </summary>
        string NinjaCommunityNugetPackagesUri { get; }

        /// <summary>
        /// Gets the local nuget packages URI.
        /// </summary>
        string LocalNugetPackagesUri { get; }

        /// <summary>
        /// Gets the application commands URI.
        /// </summary>
        string ApplicationCommandsUri { get; }
        
        /// <summary>
        /// Gets or sets a value indicating whether [output nuget commands to read me].
        /// </summary>
        bool OutputNugetCommandsToReadMe { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [output errors to read me].
        /// </summary>
        bool OutputErrorsToReadMe { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [output read me to log file].
        /// </summary>
        bool OutputReadMeToLogFile { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [use pre release MVVM cross nuget packages].
        /// </summary>
        bool UsePreReleaseMvvmCrossNugetPackages { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [use pre release xamarin forms nuget packages].
        /// </summary>
        bool UsePreReleaseXamarinFormsNugetPackages { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [use pre release ninja nuget packages].
        /// </summary>
        bool UsePreReleaseNinjaNugetPackages { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [bind context in xaml for xamarin forms].
        /// </summary>
        bool BindContextInXamlForXamarinForms { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [bind xaml for xamarin forms].
        /// </summary>
        bool BindXamlForXamarinForms { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [create test projects for all projects].
        /// </summary>
        bool CreatePlatformTestProjects { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [use xamarin test cloud].
        /// </summary>
        bool UseXamarinTestCloud { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [use xamarin insights].
        /// </summary>
        bool UseXamarinInsights { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [use style cop].
        /// </summary>
        bool UseStyleCop { get; set; }

        /// <summary>
        /// Gets the xamarin pages help.
        /// </summary>
        string XamarinPagesHelp { get; }

        /// <summary>
        /// Gets the xamarin layouts help.
        /// </summary>
        string XamarinLayoutsHelp { get; }

        /// <summary>
        /// Gets a value indicating whether [use temporary project name].
        /// </summary>
        bool UseTempProjectName { get;  }

        /// <summary>
        /// Gets or sets a value indicating whether [suspend re sharper during build].
        /// </summary>
        bool SuspendReSharperDuringBuild { get; set; }

        /// <summary>
        /// Gets a value indicating whether [use local nuget].
        /// </summary>
        bool UseLocalNuget { get;  }

        /// <summary>
        /// Gets the name of the local nuget.
        /// </summary>
        string LocalNugetName { get;  }

        /// <summary>
        /// Gets or sets a value indicating whether [create test projects solution folder].
        /// </summary>
        bool CreateTestProjectsSolutionFolder { get; set; }

        /// <summary>
        /// Gets or sets the name of the test projects solution folder.
        /// </summary>
        string TestProjectsSolutionFolderName { get; set; }

        /// <summary>
        /// Gets or sets the working directory.
        /// </summary>
        string WorkingDirectory { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [use local text templates].
        /// </summary>
        /// <value>
        /// <c>true</c> if [use local text templates]; otherwise, <c>false</c>.
        /// </value>
        bool UseLocalTextTemplates { get; set; }

        /// <summary>
        /// Gets the local text templates directory.
        /// </summary>
        string LocalTextTemplatesDirectory { get; }

        /// <summary>
        /// Gets the idependency text template.
        /// </summary>
        string IDependencyTextTemplate { get; }

        /// <summary>
        /// Gets the dependency text template.
        /// </summary>
        string DependencyTextTemplate { get; }

        /// <summary>
        /// Gets or sets the dependency directory.
        /// </summary>
        string DependencyDirectory { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [automatically add serviceto dependency].
        /// </summary>
        bool AutomaticallyAddServicetoDependency { get; set; }
        
        /// <summary>
        /// Gets the base custom renderer text template.
        /// </summary>
        string BaseCustomRendererTextTemplate { get; }

        /// <summary>
        /// Gets the custom renderer text template.
        /// </summary>
        string CustomRendererTextTemplate { get; }

        /// <summary>
        /// Gets the effects text template.
        /// </summary>
        string EffectsTextTemplate { get; }

        /// <summary>
        /// Gets or sets the custom renderer directory.
        /// </summary>
        string CustomRendererDirectory { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [automatically add renderer].
        /// </summary>
        bool AutomaticallyAddRenderer { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [automatically add effect].
        /// </summary>
        bool AutomaticallyAddEffect { get; set; }

        /// <summary>
        /// Gets or sets the effect directory.
        /// </summary>
        string EffectDirectory { get; set; }

        /// <summary>
        /// Gets the xamarin forms custom renderers URI.
        /// </summary>
        string XamarinFormsCustomRenderersUri { get; }

        /// <summary>
        /// Gets the dependency services web page.
        /// </summary>
        string DependencyServicesWebPage { get; }

        /// <summary>
        /// Gets the item templates directory.
        /// </summary>
        string ItemTemplatesDirectory { get; }

        /// <summary>
        /// Gets the project template items directory.
        /// </summary>
        string ProjectTemplateItemsDirectory { get; }

        /// <summary>
        /// Gets the xamarin forms home page.
        /// </summary>
        string XamarinFormsHomePage { get; }

        /// <summary>
        /// Gets or sets a value indicating whether [add core tests project].
        /// </summary>
        bool AddCoreTestsProject { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [add android project].
        /// </summary>
        bool AddAndroidProject { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [addi os project].
        /// </summary>
        bool AddiOSProject { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [add windows phone project].
        /// </summary>
        bool AddWindowsPhoneProject { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [add WPF project].
        /// </summary>
        bool AddWpfProject { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [add windows universal project].
        /// </summary>
        bool AddWindowsUniversalProject { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [add xamarin forms tests project].
        /// </summary>
        bool AddXamarinFormsTestsProject { get; set; }

        /// <summary>
        /// Gets or sets the type of the selected MVVM cross ios view.
        /// </summary>
        string SelectedMvvmCrossiOSViewType { get; set; }

        /// <summary>
        /// Gets the MVVM crossi os view web page.
        /// </summary>
        string MvvmCrossiOSViewWebPage { get; }

        /// <summary>
        /// Gets or sets a value indicating whether [add projects skip application options].
        /// </summary>
        bool AddProjectsSkipApplicationOptions { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [add projects skip ninja coder options].
        /// </summary>
        bool AddProjectsSkipNinjaCoderOptions { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [add projects skip MVVM cross plugin options].
        /// </summary>
        bool AddProjectsSkipMvvmCrossPluginOptions { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [add projects skip nuget package options].
        /// </summary>
        bool AddProjectsSkipNugetPackageOptions { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [add projects skip view options].
        /// </summary>
        bool AddProjectsSkipViewOptions { get; set; }

        /// <summary>
        /// Gets or sets the default type of the view.
        /// </summary>
        string DefaultViewType { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [output text template content to trace file].
        /// </summary>
        bool OutputTextTemplateContentToTraceFile { get; set; }

        /// <summary>
        /// Gets or sets the start up project.
        /// </summary>
        string StartUpProject { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [use xamarin forms xaml compilation].
        /// </summary>
        bool UseXamarinFormsXamlCompilation { get; set; }

        /// <summary>
        /// Gets or sets the core project suffix.
        /// </summary>
        string CoreProjectSuffix { get; set; }

        /// <summary>
        /// Gets or sets the droid project suffix.
        /// </summary>
        string DroidProjectSuffix { get; set; }

        /// <summary>
        /// Gets or sets the i os project suffix.
        /// </summary>
        // ReSharper disable once InconsistentNaming
        string iOSProjectSuffix { get; set; }

        /// <summary>
        /// Gets or sets the windows phone project suffix.
        /// </summary>
        string WindowsPhoneProjectSuffix { get; set; }

        /// <summary>
        /// Gets or sets the WPF project suffix.
        /// </summary>
        string WpfProjectSuffix { get; set; }

        /// <summary>
        /// Gets or sets the xamarin forms project suffix.
        /// </summary>
        string XamarinFormsProjectSuffix { get; set; }

        /// <summary>
        /// Gets or sets the windows universal project suffix.
        /// </summary>
        string WindowsUniversalProjectSuffix { get; set; }

        /// <summary>
        /// Gets or sets the core tests project suffix.
        /// </summary>
        string CoreTestsProjectSuffix { get; set; }

        /// <summary>
        /// Gets or sets the droid tests project suffix.
        /// </summary>
        string DroidTestsProjectSuffix { get; set; }

        /// <summary>
        /// Gets or sets the ios tests project suffix.
        /// </summary>
        // ReSharper disable once InconsistentNaming
        string iOSTestsProjectSuffix { get; set; }

        /// <summary>
        /// Gets or sets the windows phone tests project suffix.
        /// </summary>
        string WindowsPhoneTestsProjectSuffix { get; set; }

        /// <summary>
        /// Gets or sets the WPF tests project suffix.
        /// </summary>
        string WpfTestsProjectSuffix { get; set; }

        /// <summary>
        /// Gets or sets the xamarin forms tests project suffix.
        /// </summary>
        string XamarinFormsTestsProjectSuffix { get; set; }

        /// <summary>
        /// Gets or sets the windows universal tests project suffix.
        /// </summary>
        string WindowsUniversalTestsProjectSuffix { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [extended logging].
        /// </summary>
        bool ExtendedLogging { get; set; }

        /// <summary>
        /// Gets the no framework projects URI.
        /// </summary>
        string NoFrameworkProjectsUri { get; }

        /// <summary>
        /// Gets the xamarin forms projects URI.
        /// </summary>
        string XamarinFormsProjectsUri { get; }

        /// <summary>
        /// Gets the MVVM cross projects URI.
        /// </summary>
        string MvvmCrossProjectsUri { get; }

        /// <summary>
        /// Gets the MVVM cross and xamarin forms projects URI.
        /// </summary>
        string MvvmCrossAndXamarinFormsProjectsUri { get; }

        /// <summary>
        /// Gets or sets a value indicating whether [output logs to read me].
        /// </summary>
        bool OutputLogsToReadMe { get; set; }
    }
}

// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the Settings type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Constants
{
    /// <summary>
    /// Defines the Settings type.
    /// </summary>
    internal static class Settings
    {
        /// <summary>
        /// The application name.
        /// </summary>
        public const string ApplicationName = "Ninja Coder";

        /// <summary>
        /// The non MVVM cross solution message.
        /// </summary>
        public const string NonMvvmCrossSolution = "This solution is not a MvvmCross solution.";
        
        /// <summary>
        /// The non MVVM cross or xamarin forms solution.
        /// </summary>
        public const string NonMvvmCrossOrXamarinFormsSolution = "This solution is not a MvvmCross or Xamarin Forms solution.";
        
        /// <summary>
        /// The non xamarin forms solution.
        /// </summary>
        public const string NonXamarinFormsSolution = "This solution is not a Xamarin Forms solution.";

        /// <summary>
        /// The replace blank lines reg ex
        /// </summary>
        public const string ReplaceBlankLinesRegEx = @"(?<!\r)\n";

        /// <summary>
        /// The ninja read me file.
        /// </summary>
        public const string NinjaReadMeFile = "NinjaReadMe.txt";

        /// <summary>
        /// The directory exists.
        /// </summary>
        public const string DirectoryExists = "The directory is not empty please select another one.";

        /// <summary>
        /// The xamarin test cloud and nunit.
        /// </summary>
        public const string XamarinTestCloudAndNUnit = "To use the Xamarin Test Cloud you must select NUnit as the testing framework.";
        
        /// <summary>
        /// The base view model name.
        /// </summary>
        public const string BaseViewModelName = "BaseViewModel";

        /// <summary>
        /// The resource path.
        /// </summary>
        public const string ResourcePath = "pack://application:,,,/NinjaCoder.MvvmCross;component/Resources";

        /// <summary>
        /// The xamarin resource path.
        /// </summary>
        public const string XamarinResourcePath = ResourcePath + "/Xamarin";

        /// <summary>
        /// The nuget include pre release
        /// </summary>
        public const string NugetIncludePreRelease = " –IncludePrerelease ";

        /// <summary>
        /// The nuget install package.
        /// </summary>
        public const string NugetInstallPackage = "Install-Package %s -FileConflictAction ignore -ProjectName";

        /// <summary>
        /// The nuget install package overwrite files.
        /// </summary>
        public const string NugetInstallPackageOverwriteFiles = "Install-Package %s -FileConflictAction Overwrite -ProjectName";

        /// <summary>
        /// The suspend re sharper command.
        /// </summary>
        public const string SuspendReSharperCommand = "ReSharper_Suspend";

        /// <summary>
        /// The resume re sharper command.
        /// </summary>
        public const string ResumeReSharperCommand = "ReSharper_Resume";

        /// <summary>
        /// The style cop ms build package.
        /// </summary>
        public const string StyleCopMsBuildPackage = "StyleCop.MSBuild";

        /// <summary>
        /// The t4 suffix.
        /// </summary>
        public const string T4Suffix = ".t4";
    }
}

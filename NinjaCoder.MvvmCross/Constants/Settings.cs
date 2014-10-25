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
        /// The view model exists.
        /// </summary>
        public const string ViewModelExists = "The view model already exists, please select another one.";

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
    }
}

// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ProjectSuffixes type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NinjaCoder.MvvmCross.Constants
{
    /// <summary>
    /// Defines the Project Suffixes type.
    /// </summary>
    public static class ProjectSuffixes
    {
        /// <summary>
        /// The core
        /// </summary>
        public const string Core = "." + Settings.Core;

        /// <summary>
        /// The tests
        /// </summary>
        public const string CoreTests = "." + Settings.Core + ".Tests";

        /// <summary>
        /// The iOS
        /// </summary>
        public const string iOS = "." + Settings.iOS;

        /// <summary>
        /// The droid
        /// </summary>
        public const string Droid = "." + Settings.Droid;

        /// <summary>
        /// The windows phone
        /// </summary>
        public const string WindowsPhone = "." + Settings.WindowsPhone;

        /// <summary>
        /// The windows store
        /// </summary>
        public const string WindowsStore = "." + Settings.WindowsStore;

        /// <summary>
        /// The windows WPF
        /// </summary>
        public const string WindowsWpf = "." + Settings.Wpf;
    }
}

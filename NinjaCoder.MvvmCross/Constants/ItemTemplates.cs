// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ItemTemplates type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Constants
{
    /// <summary>
    ///  Defines the ItemTemplates type.
    /// </summary>
    internal static class ItemTemplates
    {
        /// <summary>
        /// The view model
        /// </summary>
        public const string ViewModel = "MvvmCross.ViewModel.zip";

        /// <summary>
        /// The test view model
        /// </summary>
        public const string TestViewModel = "MvvmCross.Test.ViewModel.zip";

        /// <summary>
        /// The Views type.
        /// </summary>
        public static class Views
        {
            /// <summary>
            /// The iOS
            /// </summary>
            public const string IOS = "MvvmCross." + Settings.iOS + ".View.zip";

            /// <summary>
            /// The droid
            /// </summary>
            public const string Droid = "MvvmCross." + Settings.Droid + ".View.zip";

            /// <summary>
            /// The windows phone
            /// </summary>
            public const string WindowsPhone = "MvvmCross." + Settings.WindowsPhone + ".View.zip";

            /// <summary>
            /// The windows store
            /// </summary>
            public const string WindowsStore = "MvvmCross." + Settings.WindowsStore + ".View.zip";

            /// <summary>
            /// The windows Wpf
            /// </summary>
            public const string WindowsWPF = "MvvmCross." + Settings.Wpf  + ".View.zip";
        }
    }
}

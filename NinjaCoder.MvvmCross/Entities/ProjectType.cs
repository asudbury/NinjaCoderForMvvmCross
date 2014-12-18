// --------------------------------------------------------------------------------------------------------------------
// <summary>
// 		Defines the ProjectType type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NinjaCoder.MvvmCross.Entities
{
    using System.ComponentModel;

    /// <summary>
    /// efines the ProjectType type.
    /// </summary>
    public enum ProjectType
    {
        /// <summary>
        /// The core
        /// </summary>
        [Description("Core")]
        Core,

        /// <summary>
        /// The core tests
        /// </summary>
        [Description("Core Tests")]
        CoreTests,

        /// <summary>
        /// The iOS
        /// </summary>
        [Description("iOS")]
        iOS,

        /// <summary>
        /// The iOS Tests.
        /// </summary>
        [Description("iOS Tests")]
        iOSTests,

        /// <summary>
        /// The droid
        /// </summary>
        [Description("Android")]
        Droid,

        /// <summary>
        /// The droid tests.
        /// </summary>
        [Description("Androi Tests")]
        DroidTests,

        /// <summary>
        /// The windows phone
        /// </summary>
        [Description("Windows Phone")]
        WindowsPhone,

        /// <summary>
        /// The windows phone tests.
        /// </summary>
        [Description("Windows Phone Tests")]
        WindowsPhoneTests,

        /// <summary>
        /// The windows store
        /// </summary>
        [Description("Windows Store")]
        WindowsStore,

        /// <summary>
        /// The windows store tests.
        /// </summary>
        [Description("Windows Store Tests")]
        WindowsStoreTests,

        /// <summary>
        /// The windows WPF
        /// </summary>
        [Description("Windows WPF")]
        WindowsWpf,

        /// <summary>
        /// The windows WPF Tests
        /// </summary>
        [Description("Windows WPF Tests")]
        WindowsWpfTests,

        /// <summary>
        /// The xamarin forms
        /// </summary>
        [Description("Xamarin Forms")]
        XamarinForms,

        /// <summary>
        /// The core tests
        /// </summary>
        [Description("Xamarin Forms Tests")]
        XamarinFormsTests,
    }
}

// --------------------------------------------------------------------------------------------------------------------
// <summary>
// 		Defines the ProjectSuffix type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Entities
{
    using System.ComponentModel;

    /// <summary>
    /// Defines the ProjectSuffix type.
    /// </summary>
    public enum ProjectSuffix
    {
        /// <summary>
        /// The core
        /// </summary>
        [Description(".Core")]
        Core,

        /// <summary>
        /// The tests
        /// </summary>
        [Description(".Core.Tests")]
        CoreTests,

        /// <summary>
        /// The iOS
        /// </summary>
        [Description(".iOS")]
        iOS,

        /// <summary>
        /// The iOS Tests.
        /// </summary>
        [Description(".iOS.Tests")]
        iOSTests,

        /// <summary>
        /// The droid
        /// </summary>
        [Description(".Droid")]
        Droid,

        /// <summary>
        /// The droid tests
        /// </summary>
        [Description(".Droid.Tests")]
        DroidTests,

        /// <summary>
        /// The windows phone
        /// </summary>
        [Description(".WindowsPhone")]
        WindowsPhone,

        /// <summary>
        /// The windows phone tests.
        /// </summary>
        [Description(".WindowsPhone.Tests")]
        WindowsPhoneTests,

        /// <summary>
        /// The windows WPF
        /// </summary>
        [Description(".Wpf")]
        Wpf,

        /// <summary>
        /// The windows WPF tests.
        /// </summary>
        [Description(".Wpf.Tests")]
        WpfTests,

        /// <summary>
        /// The xamarin forms
        /// </summary>
        [Description(".Forms")]
        XamarinForms,

        /// <summary>
        /// The xamarin forms tests.
        /// </summary>
        [Description(".Forms.Tests")]
        XamarinFormsTests,

        /// <summary>
        /// The windows universal.
        /// </summary>
        [Description(".WindowsUniversal")]
        WindowsUniversal,

        /// <summary>
        /// The windows universal tests.
        /// </summary>
        [Description(".WindowsUniversal.Tests")]
        WindowsUniversalTests
    }
}
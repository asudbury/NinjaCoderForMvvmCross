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
        /// The droid
        /// </summary>
        [Description("Droid")]
        Droid,

        /// <summary>
        /// The windows phone
        /// </summary>
        [Description("Windows Phone")]
        WindowsPhone,

        /// <summary>
        /// The windows store
        /// </summary>
        [Description("Windows Store")]
        WindowsStore,

        /// <summary>
        /// The windows WPF
        /// </summary>
        [Description("Windows WPF")]
        WindowsWpf,

        /// <summary>
        /// The xamarin forms
        /// </summary>
        [Description("Xamarin.Forms")]
        XamarinForms
    }
}

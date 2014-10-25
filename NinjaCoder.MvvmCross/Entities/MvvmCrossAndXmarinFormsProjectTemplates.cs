// --------------------------------------------------------------------------------------------------------------------
// <summary>
// 		Defines the MvvmCrossAndXmarinFormsProjectTemplates type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NinjaCoder.MvvmCross.Entities
{
    using System.ComponentModel;

    /// <summary>
    /// Defines the MvvmCrossAndXmarinFormsProjectTemplates type.
    /// </summary>
    public enum MvvmCrossAndXmarinFormsProjectTemplates
    {
        /// <summary>
        /// The xamarin core
        /// </summary>
        [Description("MvvmCross and Xamarin.Core.zip")]
        Core,

        /// <summary>
        /// The xamarin forms
        /// </summary>
        [Description("MvvmCross and Xamarin.Forms.zip")]
        Forms,

        /// <summary>
        /// The iOS
        /// </summary>
        [Description("MvvmCross and Xamarin Forms.iOS.zip")]
        iOS,

        /// <summary>
        /// The Droid
        /// </summary>
        [Description("MvvmCross and Xamarin.Forms.Droid.zip")]
        Droid,

        /// <summary>
        /// The Windows Phone
        /// </summary>
        [Description("MvvmCross and Xamarin.Forms.WindowsPhone.zip")]
        WindowsPhone
    }
}

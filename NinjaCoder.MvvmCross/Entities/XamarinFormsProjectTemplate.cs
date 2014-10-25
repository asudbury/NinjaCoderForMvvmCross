// --------------------------------------------------------------------------------------------------------------------
// <summary>
//  Defines the XamarinFormsProjectTemplate type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NinjaCoder.MvvmCross.Entities
{
    using System.ComponentModel;

    /// <summary>
    /// Defines the XamarinFormsProjectTemplate type.
    /// </summary>
    public enum XamarinFormsProjectTemplate
    {
        /// <summary>
        /// The xamarin core
        /// </summary>
        [Description("Xamarin.Core.zip")]
        Core,

        /// <summary>
        /// The xamarin forms
        /// </summary>
        [Description("Xamarin.Forms.zip")]
        Forms,

        /// <summary>
        /// The iOS
        /// </summary>
        [Description("Xamarin.Forms.iOS.zip")]
        iOS,

        /// <summary>
        /// The Droid
        /// </summary>
        [Description("Xamarin.Forms.Droid.zip")]
        Droid,

        /// <summary>
        /// The Windows Phone
        /// </summary>
        [Description("Xamarin.Forms.WindowsPhone.zip")]
        WindowsPhone
    }
}

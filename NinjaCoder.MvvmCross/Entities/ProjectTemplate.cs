// --------------------------------------------------------------------------------------------------------------------
// <summary>
//  Defines the NoFrameworkProjectTemplate type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NinjaCoder.MvvmCross.Entities
{
    using System.ComponentModel;

    /// <summary>
    /// Defines the NoFrameworkProjectTemplate type.
    /// </summary>
    public enum ProjectTemplate
    {
        /// <summary>
        /// The core project
        /// </summary>
        [Description("NinjaCoder.Core.zip")]
        Core, 

        /// <summary>
        /// The xamarin forms project
        /// </summary>
        [Description("Xamarin.Forms.zip")]
        XamarinForms,

        /// <summary>
        /// The nunit tests
        /// </summary>
        [Description("NUnit.Tests.zip")]
        NUnitTests,

        /// <summary>
        /// The mstest tests
        /// </summary>
        [Description("MSTest.Tests.zip")]
        MsTestTests,

        /// <summary>
        /// The iOS
        /// </summary>
        [Description("NinjaCoder.iOS.zip")]
        iOS,

        /// <summary>
        /// The droid
        /// </summary>
        [Description("NinjaCoder.Droid.zip")]
        Droid,

        /// <summary>
        /// The windows phone
        /// </summary>
        [Description("Xamarin.WindowsPhone.zip")]
        WindowsPhone,

        /// <summary>
        /// The wpf.
        /// </summary>
        [Description("NinjaCoder.Wpf.zip")]
        Wpf
    }
}
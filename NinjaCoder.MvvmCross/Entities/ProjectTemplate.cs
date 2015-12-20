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
        /// The core project.
        /// </summary>
        [Description("NinjaCoder.Core.zip")]
        Core, 

        /// <summary>
        /// The xamarin forms project.
        /// </summary>
        [Description("NinjaCoder.Xamarin.Forms.zip")]
        XamarinForms,

        /// <summary>
        /// The nunit tests.
        /// </summary>
        [Description("NinjaCoder.NUnit.zip")]
        NUnitTests,

        /// <summary>
        /// The mstest tests.
        /// </summary>
        [Description("NinjaCoder.MSTest.zip")]
        MsTestTests,

        /// <summary>
        /// The iOS.
        /// </summary>
        [Description("NinjaCoder.iOS.zip")]
        iOS,

        /// <summary>
        /// The droid.
        /// </summary>
        [Description("NinjaCoder.Droid.zip")]
        Droid,

        /// <summary>
        /// The windows phone.
        /// </summary>
        [Description("NinjaCoder.WindowsPhone.zip")]
        WindowsPhone,

        /// <summary>
        /// The windows store.
        /// </summary>
        [Description("BlankApp.zip")]
        WindowsStore,

        /// <summary>
        /// The wpf.
        /// </summary>
        [Description("NinjaCoder.Wpf.zip")]
        Wpf,

        /// <summary>
        /// The windows universal.
        /// </summary>
        [Description("NinjaCoder.WindowsUniversal.zip")]
        WindowsUniversal
    }
}
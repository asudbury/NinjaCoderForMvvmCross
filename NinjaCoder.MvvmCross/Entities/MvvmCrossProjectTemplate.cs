// --------------------------------------------------------------------------------------------------------------------
// <summary>
//  Defines the MvvmCrossProjectTemplate type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NinjaCoder.MvvmCross.Entities
{
    using System.ComponentModel;

    /// <summary>
    /// Defines the MvvmCrossProjectTemplate type.
    /// </summary>
    public enum MvvmCrossProjectTemplate
    {
        /// <summary>
        /// The core
        /// </summary>
        [Description("MvvmCross.Core.zip")]
        Core,

        /// <summary>
        /// The nunit tests
        /// </summary>
        [Description("MvvmCross.Core.NUnit.Tests.zip")]
        NUnitTests,

        /// <summary>
        /// The mstest tests
        /// </summary>
        [Description("MvvmCross.Core.MSTest.Tests.zip")]
        MsTestTests,

        /// <summary>
        /// The iOS
        /// </summary>
        [Description("MvvmCross.iOS.zip")]
        iOS,

        /// <summary>
        /// The droid
        /// </summary>
        [Description("MvvmCross.Droid.zip")]
        Droid,

        /// <summary>
        /// The windows phone
        /// </summary>
        [Description("MvvmCross.WindowsPhone.zip")]
        WindowsPhone,

        /// <summary>
        /// The windows store
        /// </summary>
        [Description("MvvmCross.WindowsStore.zip")]
        WindowsStore,

        /// <summary>
        /// The windows WPF
        /// </summary>
        [Description("MvvmCross.Wpf.zip")]
        WindowsWpf
    }
}
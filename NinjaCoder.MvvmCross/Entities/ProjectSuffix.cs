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
        /// The droid
        /// </summary>
        [Description(".Droid")]
	    Droid,

        /// <summary>
        /// The windows phone
        /// </summary>
        [Description(".WindowsPhone")]
	    WindowsPhone,

        /// <summary>
        /// The windows store
        /// </summary>
        [Description(".WindowsStore")]
	    WindowsStore,

        /// <summary>
        /// The windows WPF
        /// </summary>
        [Description(".Wpf")]
	    Wpf,

        /// <summary>
        /// The windows WPF
        /// </summary>
        [Description(".Forms")]
	    Forms
    }
}
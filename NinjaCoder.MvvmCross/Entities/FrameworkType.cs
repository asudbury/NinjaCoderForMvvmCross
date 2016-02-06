// --------------------------------------------------------------------------------------------------------------------
// <summary>
// 		Defines the FrameworkType type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Entities
{
    using System.ComponentModel;

    /// <summary>
    /// Defines the FrameworkType type.
    /// </summary>
    public enum FrameworkType
    {
        /// <summary>
        /// Not Set.
        /// </summary>
        [Description("Not Set")]
        NotSet,

        /// <summary>
        /// No framework.
        /// </summary>
        [Description("No Framework")]
        NoFramework,

        /// <summary>
        /// The MvvmCross framework.
        /// </summary>
        [Description("MvvmCross")]
        MvvmCross,

        /// <summary>
        /// The Xamarin forms framework
        /// </summary>
        [Description("Xamarin Forms")]
        XamarinForms,

        /// <summary>
        /// The MvvmCross and Xamarin forms framework
        /// </summary>
        [Description("MvvmCross and Xamarin Forms")]
        MvvmCrossAndXamarinForms
    }
}
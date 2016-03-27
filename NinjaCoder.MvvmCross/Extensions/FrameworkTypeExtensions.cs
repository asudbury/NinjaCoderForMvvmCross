// --------------------------------------------------------------------------------------------------------------------
// <summary>
//  Defines the FrameworkTypeExtensions type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Extensions
{
    using Entities;

    /// <summary>
    ///  Defines the FrameworkTypeExtensions type.
    /// </summary>
    public static class FrameworkTypeExtensions
    {
        /// <summary>
        /// Determines whether [is MVVM cross solution type] [the specified instance].
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns>True or false.</returns>
        public static bool IsMvvmCrossSolutionType(this FrameworkType instance)
        {
            return instance == FrameworkType.MvvmCross || 
                   instance == FrameworkType.MvvmCrossAndXamarinForms;
        }

        /// <summary>
        /// Determines whether [is xamarin forms solution type] [the specified instance].
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns>True or false.</returns>
        public static bool IsXamarinFormsSolutionType(this FrameworkType instance)
        {
            return instance == FrameworkType.XamarinForms ||
                   instance == FrameworkType.MvvmCrossAndXamarinForms;
        }
    }
}

// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the IMvxFormsAndroidNavigationProvider type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace $rootnamespace$.Presenters
{
    using Xamarin.Forms;

    /// <summary>
    /// Defines the IMvxFormsAndroidNavigationProvider type.
    /// </summary>
    public interface IMvxFormsAndroidNavigationProvider
    {
        /// <summary>
        /// Pushes the specified page.
        /// </summary>
        /// <param name="page">The page.</param>
        void Push(Page page);

        /// <summary>
        /// Pops this instance.
        /// </summary>
        void Pop();
    }
}
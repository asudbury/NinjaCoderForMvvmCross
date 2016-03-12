// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the INavigationService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace $rootnamespace$.Interfaces
{
    /// <summary>
    ///  Defines the INavigationService type.
    /// </summary>
    public interface INavigationService
    {
        /// <summary>
        /// Navigates to the View Model.
        /// </summary>
        /// <typeparam name="TViewModel">The View Model.</typeparam>
        void NavigateToViewModel<TViewModel>();
    }
}

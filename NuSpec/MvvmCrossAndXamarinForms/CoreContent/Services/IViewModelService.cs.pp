// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the IViewModelService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace $rootnamespace$.Services
{
    using Cirrious.MvvmCross.ViewModels;

    /// <summary>
    ///  Defines the IViewModelService type.
    /// </summary>
    public interface IViewModelService
    {
        /// <summary>
        /// Gets the view model.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>The View Model.</returns>
        IMvxViewModel GetViewModel(MvxViewModelRequest request);
    }
}

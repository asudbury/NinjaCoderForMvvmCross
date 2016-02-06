// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ViewModelService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace $rootnamespace$.Services
{
    using MvvmCross.Core.ViewModels;
    using MvvmCross.Platform;

    /// <summary>
    ///  Defines the ViewModelService type.
    /// </summary>
    public class ViewModelService : IViewModelService
    {
        /// <summary>
        /// Gets the view model.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>
        /// The View Model.
        /// </returns>
        public IMvxViewModel GetViewModel(MvxViewModelRequest request)
        {
            return Mvx.Resolve<IMvxViewModelLoader>().LoadViewModel(request, null);
        }
    }
}

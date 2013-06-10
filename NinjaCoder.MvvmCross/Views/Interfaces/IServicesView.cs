// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the IServicesView type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Views.Interfaces
{
    /// <summary>
    ///  Defines the IServicesView type.
    /// </summary>
    public interface IServicesView : IItemTemplatesView
    {
        /// <summary>
        /// Gets the implement in view model.
        /// </summary>
        string ImplementInViewModel { get; }

        /// <summary>
        /// Adds the viewModel.
        /// </summary>
        /// <param name="viewModelName">Name of the view model.</param>
        void AddViewModel(string viewModelName);
    }
}

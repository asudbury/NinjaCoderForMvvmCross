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
        /// Gets or sets a value indicating whether [use nuget].
        /// </summary>
        bool UseNuget { get; set; }

        /// <summary>
        /// Gets a value indicating whether [include unit tests].
        /// </summary>
        bool IncludeUnitTests { get; }

        /// <summary>
        /// Adds the viewModel.
        /// </summary>
        /// <param name="viewModelName">Name of the view model.</param>
        void AddViewModel(string viewModelName);
    }
}

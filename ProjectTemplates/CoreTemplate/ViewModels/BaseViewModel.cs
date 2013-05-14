// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the BaseViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CoreTemplate.ViewModels
{
    using Cirrious.CrossCore;
    using Cirrious.MvvmCross.ViewModels;

    using CoreTemplate.Services;

    /// <summary>
    ///    Defines the BaseViewModel type.
    /// </summary>
    public abstract class BaseViewModel : MvxViewModel
    {
        /// <summary>
        /// Reports the error.
        /// </summary>
        /// <param name="error">The error.</param>
        public void ReportError(string error)
        {
            Mvx.Resolve<IErrorService>().ReportError(error);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.ViewModels;

namespace $rootnamespace$.Services.ViewModel
{
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

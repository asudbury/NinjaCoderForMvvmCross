// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the NavigationService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace $rootnamespace$.Services
{
    using Core.Interfaces;
    using Core.ViewModels;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Xamarin.Forms;

    /// <summary>
    ///  Defines the NavigationService type.
    /// </summary>
    public class NavigationService : INavigationService
    {
        /// <summary>
        /// The view model suffix.
        /// </summary>
        private readonly string viewModelSuffix;

        /// <summary>
        /// The view suffix.
        /// </summary>
        private readonly string viewSuffix;

        /// <summary>
        /// A read-only reference to our Xamarin.Forms Application instance
        /// </summary>
        protected readonly Application Application;

        /// <summary>
        /// The available view types.
        /// </summary>
        private readonly List<Type> availableViewTypes;

        /// <summary>
        /// Initializes a new instance of the <see cref="NavigationService" /> class.
        /// </summary>
        /// <param name="application">The application.</param>
        /// <param name="viewModelSuffix">The view model suffix.</param>
        /// <param name="viewSuffix">The view suffix.</param>
        public NavigationService(
            Application application,
            string viewModelSuffix = "ViewModel",
            string viewSuffix = "View")
        {
            this.Application = application;
            this.viewModelSuffix = viewModelSuffix;
            this.viewSuffix = viewSuffix;

            this.availableViewTypes = this.Application.
                GetType().
                GetTypeInfo().
                Assembly.
                DefinedTypes.
                Select(t => t.AsType()).ToList();
        }

        /// <summary>
        /// Navigates to the View Model.
        /// </summary>
        /// <typeparam name="TViewModel">The View Model.</typeparam>
        public void NavigateToViewModel<TViewModel>()
        {
            Type viewModelType = typeof(TViewModel);

            BaseViewModel viewModel = this.CreateViewModel<TViewModel>(viewModelType);

            if (viewModel == null)
            {
                throw new ArgumentException(viewModelType + " is not a NinjaCoder ViewModel.");
            }

            Page page = this.CreateView(viewModelType);

            this.ShowView(page);
        }

        /// <summary>
        /// Shows the view.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <exception cref="System.ArgumentNullException">page</exception>
        protected virtual void ShowView(Page page)
        {
            Device.BeginInvokeOnMainThread(async () => await this.GetCurrentPage().Navigation.PushAsync(page, true));
        }

        /// <summary>
        /// Creates the view model.
        /// </summary>
        /// <typeparam name="TViewModel">The type of the view model.</typeparam>
        /// <param name="viewModelType">Type of the view model.</param>
        /// <returns></returns>
        internal BaseViewModel CreateViewModel<TViewModel>(Type viewModelType)
        {
            Type viewModel = typeof(TViewModel);

            if (viewModel == null)
            {
                throw new ArgumentException(viewModelType + " does not implement BaseViewModel.");
            }

            return Activator.CreateInstance(viewModel) as BaseViewModel;
        }

        /// <summary>
        /// Creates the view.
        /// </summary>
        /// <param name="viewModelType">Type of the view model.</param>
        /// <returns></returns>
        internal Page CreateView(Type viewModelType)
        {
            string viewName = viewModelType.Name.Replace(this.viewModelSuffix, this.viewSuffix);

            Type pageType = this.availableViewTypes.FirstOrDefault(t => t.Name == viewName);

            var view = Activator.CreateInstance(pageType) as Page;

            if (view == null)
            {
                throw new ArgumentException(viewName + " is not a Xamarin.Forms Page.");
            }

            return view;
        }

        /// <summary>
        /// Gets the current page.
        /// </summary>
        internal Page GetCurrentPage()
        {
            Page modalPage = this.Application.MainPage.Navigation.ModalStack.LastOrDefault();

            if (modalPage != null)
            {
                return modalPage;
            }

            Page contentPage = this.Application.MainPage.Navigation.NavigationStack.LastOrDefault();

            if (contentPage != null)
            {
                return contentPage;
            }

            return this.Application.MainPage;
        }
    }
}

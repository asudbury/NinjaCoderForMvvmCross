// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the BaseViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.ViewModels
{
    using System.Windows.Input;

    using MahApps.Metro;

    using NinjaCoder.MvvmCross.Infrastructure.Services;

    using Scorchio.Infrastructure.Wpf;
    using Scorchio.Infrastructure.Wpf.ViewModels;

    /// <summary>
    ///  Defines the BaseViewModel type.
    /// </summary>
    public abstract class BaseViewModel : BaseDialogViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseViewModel" /> class.
        /// </summary>
        /// <param name="settingsService">The settings service.</param>
        protected BaseViewModel(ISettingsService settingsService)
        {
            this.SettingsService = settingsService;
        }

        /// <summary>
        /// Gets the settings service.
        /// </summary>
        public ISettingsService SettingsService { get; private set; }

        /// <summary>
        /// Gets the ok command.
        /// </summary>
        public ICommand OkCommand
        {
            get { return new RelayCommand(this.OnOk); }
        }

        /// <summary>
        /// Gets the cancel command.
        /// </summary>
        public ICommand CancelCommand
        {
            get { return new RelayCommand(this.OnCancel); }
        }

        /// <summary>
        /// Gets the current theme.
        /// </summary>
        protected Theme CurrentTheme
        {
            get
            {
                return this.SettingsService.Theme == "Dark" ? Theme.Dark : Theme.Light;
            }
        }
    }
}

// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the NinjaBaseViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using MahApps.Metro.Controls;

namespace NinjaCoder.MvvmCross.ViewModels
{
    using MahApps.Metro;
    using Scorchio.Infrastructure.Wpf;
    using Scorchio.Infrastructure.Wpf.ViewModels;
    using Services.Interfaces;
    using System.Windows.Input;

    /// <summary>
    ///  Defines the NinjaBaseViewModel type.
    /// </summary>
    public abstract class NinjaBaseViewModel : BaseDialogViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NinjaBaseViewModel" /> class.
        /// </summary>
        /// <param name="settingsService">The settings service.</param>
        protected NinjaBaseViewModel(ISettingsService settingsService)
        {
            this.SettingsService = settingsService;
        }

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
        /// Gets or sets the settings service.
        /// </summary>
        protected ISettingsService SettingsService { get; set; }

        /// <summary>
        /// Gets the current theme.
        /// </summary>
        protected Theme CurrentTheme
        {
            get { return this.SettingsService.Theme == "Dark" ? Theme.Dark : Theme.Light; }
        }
    }
}

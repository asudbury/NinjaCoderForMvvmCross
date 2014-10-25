// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the DownloadViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.ViewModels
{
    using NinjaCoder.MvvmCross.Services.Interfaces;

    using Scorchio.Infrastructure.Wpf;
    using System.Diagnostics;
    using System.Windows.Input;

    /// <summary>
    /// Defines the DownloadViewModel type.
    /// </summary>
    internal class DownloadViewModel : NinjaBaseViewModel
    {
        /// <summary>
        /// The check for updates.
        /// </summary>
        private bool checkForUpdates;

        public DownloadViewModel(ISettingsService settingsService)
            : base(settingsService)
        {
            this.Init();
        }

        /// <summary>
        /// Gets or sets a value indicating whether [check for updates].
        /// </summary>
        public bool CheckForUpdates
        {
            get { return this.checkForUpdates; }
            set { this.SetProperty(ref this.checkForUpdates, value); }
        }

        /// <summary>
        /// Gets the view download page command.
        /// </summary>
        public ICommand ViewDownloadPageCommand
        {
            get { return new RelayCommand(this.DisplayDownloadPage); }
        }

        /// <summary>
        /// Called when cancel button pressed.
        /// </summary>
        public override void OnCancel()
        {
            this.SettingsService.CheckForUpdates = this.checkForUpdates;
            base.OnCancel();
        }

        /// <summary>
        /// Inits this instance.
        /// </summary>
        internal void Init()
        {
            this.checkForUpdates = this.SettingsService.CheckForUpdates;
        }

        /// <summary>
        /// Displays the download page.
        /// </summary>
        internal void DisplayDownloadPage()
        {
            Process.Start(this.SettingsService.NinjaCoderDownloadUrl);
            this.OnCancel();
        }
    }
}
 
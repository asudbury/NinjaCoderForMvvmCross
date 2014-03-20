// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the TracingViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.ViewModels.Options
{
    using System.Windows;
    using System.Windows.Input;

    using NinjaCoder.MvvmCross.Infrastructure.Services;
    using NinjaCoder.MvvmCross.Services.Interfaces;

    using Scorchio.Infrastructure.Services;
    using Scorchio.Infrastructure.Wpf;

    /// <summary>
    ///  Defines the TracingViewModel type.
    /// </summary>
    public class TracingViewModel : BaseViewModel
    {
        /// <summary>
        /// The application service.
        /// </summary>
        private readonly IApplicationService applicationService;

        /// <summary>
        /// The message box service.
        /// </summary>
        private readonly IMessageBoxService messageBoxService;

        /// <summary>
        /// The trace output enabled.
        /// </summary>
        private bool traceOutputEnabled;

        /// <summary>
        /// The log to file.
        /// </summary>
        private bool logToFile;

        /// <summary>
        /// The display errors.
        /// </summary>
        private bool displayErrors;

        /// <summary>
        /// The log file path.
        /// </summary>
        private string logFilePath;

        /// <summary>
        /// Initializes a new instance of the <see cref="TracingViewModel" /> class.
        /// </summary>
        /// <param name="applicationService">The application service.</param>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="messageBoxService">The message box service.</param>
        public TracingViewModel(
            IApplicationService applicationService,
            ISettingsService settingsService,
            IMessageBoxService messageBoxService)
            : base(settingsService)
        {
            this.applicationService = applicationService;
            this.messageBoxService = messageBoxService;

            this.Init();
        }

        /// <summary>
        /// Gets or sets the language dictionary.
        /// </summary>
        public ResourceDictionary LanguageDictionary { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [trace output enabled].
        /// </summary>
        public bool TraceOutputEnabled
        {
            get { return this.traceOutputEnabled; }
            set { this.SetProperty(ref this.traceOutputEnabled, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [log to file].
        /// </summary>
        public bool LogToFile
        {
            get { return this.logToFile; }
            set { this.SetProperty(ref this.logToFile, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [display errors].
        /// </summary>
        public bool DisplayErrors
        {
            get { return this.displayErrors; }
            set { this.SetProperty(ref this.displayErrors, value); }
        }

        /// <summary>
        /// Gets or sets the log file path.
        /// </summary>
        public string LogFilePath
        {
            get { return this.logFilePath; }
            set { this.SetProperty(ref this.logFilePath, value); }
        }

        /// <summary>
        /// Gets the clear log command.
        /// </summary>
        public ICommand ClearLogCommand
        {
            get { return new RelayCommand(this.ClearLog); }
        }

        /// <summary>
        /// Gets the view log command.
        /// </summary>
        public ICommand ViewLogCommand
        {
            get { return new RelayCommand(this.ViewLog); }
        }

        /// <summary>
        /// Inits this instance.
        /// </summary>
        internal void Init()
        {
            this.TraceOutputEnabled = this.SettingsService.LogToTrace;
            this.LogToFile = this.SettingsService.LogToFile;
            this.LogFilePath = this.SettingsService.LogFilePath;
            this.DisplayErrors = this.SettingsService.DisplayErrors;
        }

        /// <summary>
        /// Saves this instance.
        /// </summary>
        internal void Save()
        {
            this.SettingsService.LogToTrace = this.TraceOutputEnabled;
            this.SettingsService.LogToFile = this.LogToFile;
            this.SettingsService.LogFilePath = this.LogFilePath;
            this.SettingsService.DisplayErrors = this.DisplayErrors;
        }

        /// <summary>
        /// Clears the log.
        /// </summary>
        internal void ClearLog()
        {
            this.applicationService.ClearLogFile();

            this.messageBoxService.Show(
                "The log has been cleared.",
                Constants.Settings.ApplicationName,
                true,
                this.CurrentTheme,
                this.SettingsService.ThemeColor);
        }

        /// <summary>
        /// Views the log.
        /// </summary>
        internal void ViewLog()
        {
            this.applicationService.ViewLogFile();
        }
    }
}

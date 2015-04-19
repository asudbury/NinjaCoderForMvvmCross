// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the CodingStyleViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.ViewModels.Options
{
    using System.Windows;

    using NinjaCoder.MvvmCross.Services.Interfaces;

    /// <summary>
    ///  Defines the CodingStyleViewModel type.
    /// </summary>
    public class CodingStyleViewModel : NinjaBaseViewModel
    {
        /// <summary>
        /// The remove default file headers.
        /// </summary>
        private bool removeDefaultFileHeaders;

        /// <summary>
        /// The remove default comments.
        /// </summary>
        private bool removeDefaultComments;

        /// <summary>
        /// The format function parameters.
        /// </summary>
        private bool formatFunctionParameters;

        /// <summary>
        /// Initializes a new instance of the <see cref="NinjaBaseViewModel" /> class.
        /// </summary>
        /// <param name="settingsService">The settings service.</param>
        public CodingStyleViewModel(ISettingsService settingsService)
            : base(settingsService)
        {
            this.Init();
        }
        
        /// <summary>
        /// Gets or sets the language dictionary.
        /// </summary>
        public ResourceDictionary LanguageDictionary { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [remove default file headers].
        /// </summary>
        public bool RemoveDefaultFileHeaders
        {
            get { return this.removeDefaultFileHeaders; }
            set { this.SetProperty(ref this.removeDefaultFileHeaders, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [remove default comments].
        /// </summary>
        public bool RemoveDefaultComments
        {
            get { return this.removeDefaultComments; }
            set { this.SetProperty(ref this.removeDefaultComments, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [format function parameters].
        /// </summary>
        public bool FormatFunctionParameters
        {
            get { return this.formatFunctionParameters; }
            set { this.SetProperty(ref this.formatFunctionParameters, value); }
        }
    
        /// <summary>
        /// Saves this instance.
        /// </summary>
        public void Save()
        {
            this.SettingsService.RemoveDefaultComments = this.RemoveDefaultComments;
            this.SettingsService.RemoveDefaultFileHeaders = this.RemoveDefaultFileHeaders;
            this.SettingsService.FormatFunctionParameters = this.FormatFunctionParameters;
        }

        /// <summary>
        /// Inits this instance.
        /// </summary>
        internal void Init()
        {
            this.RemoveDefaultComments = this.SettingsService.RemoveDefaultComments;
            this.RemoveDefaultFileHeaders = this.SettingsService.RemoveDefaultFileHeaders;
            this.FormatFunctionParameters = this.SettingsService.FormatFunctionParameters;
        }
    }
}

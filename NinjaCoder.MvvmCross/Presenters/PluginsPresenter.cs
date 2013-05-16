// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the PluginsPresenter type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NinjaCoder.MvvmCross.Presenters
{
    using NinjaCoder.MvvmCross.Services;

    /// <summary>
    ///  Defines the PluginsPresenter type.
    /// </summary>
    public class PluginsPresenter
    {
        /// <summary>
        /// The Settings Service.
        /// </summary>
        private readonly ISettingsService settingsService;
        /// <summary>
        /// Initializes a new instance of the <see cref="PluginsPresenter" /> class.
        /// </summary>
        public PluginsPresenter()
            : this(new SettingsService())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PluginsPresenter" /> class.
        /// </summary>
        /// <param name="settingsService">The settings service.</param>
        public PluginsPresenter(ISettingsService settingsService)
        {
            this.settingsService = settingsService;
        }

        /// <summary>
        /// Loads this instance.
        /// </summary>
        public void Load()
        {

        }
    }
}

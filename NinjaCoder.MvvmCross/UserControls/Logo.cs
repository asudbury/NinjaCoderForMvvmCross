// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the Logo type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.UserControls
{
    using Services;
    using System.Windows.Forms;

    /// <summary>
    /// Defines the Logo type.
    /// </summary>
    public partial class Logo : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Logo"/> class.
        /// </summary>
        public Logo()
        {
            this.InitializeComponent();

            SettingsService settingsService = new SettingsService();

            this.labelVersion.Text += " v" + settingsService.ApplicationVersion;

            this.labelMvvmCross.Text += " v" + settingsService.MvvmCrossVersion;
        }
    }
}

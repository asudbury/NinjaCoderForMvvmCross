// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the WebRequestServiceView type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.TemplateWizards.WebRequestService
{
    using System;
    using System.Globalization;
    using System.Windows.Forms;

    /// <summary>
    /// Defines the WebRequestServiceView.
    /// </summary>Forest23
    public partial class WebRequestServiceView : BaseForm 
    {
        /// <summary>
        /// The web request service.
        /// </summary>
        private const string WebRequestService = "WebRequestService";

        /// <summary>
        /// Initializes a new instance of the <see cref="WebRequestServiceView" /> class.
        /// </summary>
        /// <param name="displayLogo">if set to <c>true</c> [display logo].</param>
        public WebRequestServiceView(bool displayLogo)
        {
            this.InitializeComponent();
            this.DialogResult = DialogResult.None;
            this.SetLogoVisibility(this.logo1, displayLogo);
        }

        /// <summary>
        /// Gets the name of the entity.
        /// </summary>
        public string EntityName
        {
            get { return this.textBoxEntityName.Text; }
        }

        /// <summary>
        /// Webs the request service view load.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void WebRequestServiceViewLoad(
            object sender, 
            EventArgs e)
        {
            this.textBoxEntityName.Focus();
            this.ActiveControl = this.textBoxEntityName;
        }

        /// <summary>
        /// Handles the Click event of the buttonOK control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ButtonOkClick(
            object sender, 
            EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// Handles the TextChanged event of the textBoxEntityName control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void TextBoxEntityNameTextChanged(object sender, EventArgs e)
        {
            this.TextBoxDataServiceInterface.Text = "I" + this.textBoxEntityName.Text + WebRequestService;
            this.TextBoxDataService.Text = this.textBoxEntityName.Text + WebRequestService;

            this.textBoxEntityName.Text = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(this.textBoxEntityName.Text);

            //// position the cursor in the correct position.
            this.textBoxEntityName.SelectionStart = this.textBoxEntityName.Text.Length;
        }
    }
}

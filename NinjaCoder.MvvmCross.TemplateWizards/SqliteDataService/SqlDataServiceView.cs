// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the SqlDataServiceView type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.TemplateWizards.SqliteDataService
{
    using System;
    using System.Globalization;
    using System.Windows.Forms;

    /// <summary>
    /// Defines the SqlDataServiceView type/
    /// </summary>
    public partial class SqlDataServiceView : BaseForm
    {
        /// <summary>
        /// The data service.
        /// </summary>
        private const string DataService = "DataService";

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlDataServiceView" /> class.
        /// </summary>
        public SqlDataServiceView()
        {
            this.InitializeComponent();
            this.DialogResult = DialogResult.None;
        }

        /// <summary>
        /// Gets the name of the entity.
        /// </summary>
        public string EntityName
        { 
            get { return this.textBoxEntityName.Text; }
        }

        /// <summary>
        /// Handles the Load event of the SqlDataServiceView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void SqlDataServiceViewLoad(
            object sender, 
            EventArgs e)
        {
            this.textBoxEntityName.Focus();
            this.ActiveControl = this.textBoxEntityName;
        }

        /// <summary>
        /// Texts the box entity name text changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void TextBoxEntityNameTextChanged(
            object sender, 
            EventArgs e)
        {
            this.TextBoxDataServiceInterface.Text = "I" + this.textBoxEntityName.Text + DataService;
            this.TextBoxDataService.Text = this.textBoxEntityName.Text + DataService;

            this.textBoxEntityName.Text = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(this.textBoxEntityName.Text);

            //// position the cursor in the correct position.
            this.textBoxEntityName.SelectionStart = this.textBoxEntityName.Text.Length;
        }

        /// <summary>
        /// Buttons the ok click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ButtonOKClick(
            object sender, 
            EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}

// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the OptionsForm type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Views
{
    using System;
    using Interfaces;
    using Presenters;

    /// <summary>
    ///    Defines the OptionsForm type.
    /// </summary>
    public partial class OptionsForm : BaseView, IOptionsView
    {
        /// <summary>
        /// The presenter.
        /// </summary>
        private readonly OptionsPresenter presenter;

        /// <summary>
        /// Initializes a new instance of the <see cref="OptionsForm" /> class.
        /// </summary>
        /// <param name="displayLogo">if set to <c>true</c> [display logo].</param>
        public OptionsForm(bool displayLogo)
        {
            this.InitializeComponent();

            this.presenter = new OptionsPresenter(this, displayLogo);

            this.presenter.LoadSettings();
        }

        /// <summary>
        /// Sets a value indicating whether [display logo].
        /// </summary>
        public bool DisplayLogo
        {
            set { this.SetLogoVisibility(this.logo1, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [trace output enabled].
        /// </summary>
        public bool TraceOutputEnabled
        {
            get { return this.checkBoxTrace.Checked; }
            set { this.checkBoxTrace.Checked = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether log to file.
        /// </summary>
        public bool LogToFile
        {
            get { return this.chkCreateLogFile.Checked; }
            set { this.chkCreateLogFile.Checked = value; }
        }

        /// <summary>
        /// Gets or sets the log file path.
        /// </summary>
        public string LogFilePath 
        {
            get { return this.textBoxLogFile.Text; }
            set { this.textBoxLogFile.Text = value; } 
        }

        /// <summary>
        /// Gets or sets a value indicating whether [include lib folder in projects].
        /// </summary>
        public bool IncludeLibFolderInProjects
        {
            get { return this.checkBoxIncludeLibFolders.Checked; }
            set { this.checkBoxIncludeLibFolders.Checked = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [display errors].
        /// </summary>
        public bool DisplayErrors
        {
            get { return this.checkBoxDisplayErrors.Checked; }
            set { this.checkBoxDisplayErrors.Checked = value; }
        }

        /// <summary>
        /// Buttons the cancel click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ButtonCancelClick(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Buttons the OK click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ButtonOKClick(object sender, EventArgs e)
        {
            this.Continue = true;

            this.presenter.SaveSettings();
            this.Close();
        }
    }
}

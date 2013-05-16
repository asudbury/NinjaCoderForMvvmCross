// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the OptionsForm type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NinjaCoder.MvvmCross.Views
{
    using System;
    using System.Windows.Forms;

    using NinjaCoder.MvvmCross.Presenters;
    using NinjaCoder.MvvmCross.Views.Interfaces;

    /// <summary>
    ///    Defines the OptionsForm type.
    /// </summary>
    public partial class OptionsForm : Form, IOptionsView
    {
        /// <summary>
        /// The presenter.
        /// </summary>
        private readonly OptionsPresenter presenter;

        /// <summary>
        /// Initializes a new instance of the <see cref="OptionsForm"/> class.
        /// </summary>
        public OptionsForm()
        {
            this.InitializeComponent();

            this.presenter = new OptionsPresenter(this);

            this.presenter.LoadSettings();
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="SolutionOptionsForm"/> is continue.
        /// </summary>
        public bool Continue { get; set; }

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

        /// <summary>
        /// Buttons the path click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ButtonPathClick(object sender, EventArgs e)
        {

        }
    }
}

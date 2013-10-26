// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the OptionsForm type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Views
{
    using System;
    using System.Windows.Forms;

    using Interfaces;

    using NinjaCoder.MvvmCross.Infrastructure.Services;

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
        /// <param name="settingsService">The settings service.</param>
        public OptionsForm(ISettingsService settingsService)
        {
            this.InitializeComponent();

            this.presenter = new OptionsPresenter(this, settingsService);

            this.presenter.LoadSettings();
        }

        /// <summary>
        /// Sets a value indicating whether [display logo].
        /// </summary>
        public bool DisplayLogo
        {
            set { }
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
        /// Gets or sets a value indicating whether [remove default file headers].
        /// </summary>
        public bool RemoveDefaultFileHeaders
        {
            get { return this.checkBoxRemoveFileHeaders.Checked; }
            set { this.checkBoxRemoveFileHeaders.Checked = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [remove default comments].
        /// </summary>
        public bool RemoveDefaultComments
        {
            get { return this.checkBoxRemoveCodeComments.Checked; }
            set { this.checkBoxRemoveCodeComments.Checked = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [use nuget for project templates].
        /// </summary>
        public bool UseNugetForProjectTemplates
        {
            get { return this.checkBoxUseNugetForProjectTemplates.Checked; }
            set { this.checkBoxUseNugetForProjectTemplates.Checked = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [use nuget for plugins].
        /// </summary>
        public bool UseNugetForPlugins
        {
            get { return this.checkBoxUseNugetForPlugins.Checked; }
            set { this.checkBoxUseNugetForPlugins.Checked = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [use nuget for services].
        /// </summary>
        public bool UseNugetForServices
        {
            get { return this.checkBoxUseNugetForServices.Checked; }
            set { this.checkBoxUseNugetForServices.Checked = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [suspend re sharper during build].
        /// </summary>
        public bool SuspendReSharperDuringBuild
        {
            get { return this.checkBoxSuspendReSharper.Checked; }
            set { this.checkBoxSuspendReSharper.Checked = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [format function parameters].
        /// </summary>
        public bool FormatFunctionParameters 
        {
            get { return this.checkBoxFormatFunctionParameters.Checked; }
            set { this.checkBoxFormatFunctionParameters.Checked = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [copy assemblies to lib folder].
        /// </summary>
        public bool CopyAssembliesToLibFolder
        {
            get { return this.checkBoxCopyAssembliesToLibFolder.Checked; }
            set { this.checkBoxCopyAssembliesToLibFolder.Checked = value; }
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
        private void ButtonOKClick(
            object sender, 
            EventArgs e)
        {
            this.DialogResult = DialogResult.OK;

            this.presenter.SaveSettings();
            this.Close();
        }

        /// <summary>
        /// Links the label link clicked.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void LinkLabelLinkClicked(
            object sender,
            LinkLabelLinkClickedEventArgs e)
        {
            this.presenter.ShowDownloadNugetPage();
        }

        /// <summary>
        /// Links the label clear log link clicked.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void LinkLabelClearLogLinkClicked(
            object sender, 
            LinkLabelLinkClickedEventArgs e)
        {
            this.presenter.ClearLog();
        }

        /// <summary>
        /// Links the label view log link clicked.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void LinkLabelViewLogLinkClicked(
            object sender,
            LinkLabelLinkClickedEventArgs e)
        {
            this.presenter.ViewLog();
        }
    }
}


 // --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the SolutionOptionsForm type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NinjaCoder.MvvmCross.Views
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Forms;

    using NinjaCoder.MvvmCross.Presenters;
    using NinjaCoder.MvvmCross.Views.Interfaces;

    using Scorchio.VisualStudio.Entities;

    /// <summary>
    /// Defines the SolutionOptionsForm type.
    /// </summary>
    public partial class SolutionOptionsForm : Form, ISolutionOptionsView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SolutionOptionsForm" /> class.
        /// </summary>
        /// <param name="defaultProjectsLocation">The default projects location.</param>
        /// <param name="defaultProjectName">Default name of the project.</param>
        /// <param name="projectInfos">The project infos.</param>
        public SolutionOptionsForm(
            string defaultProjectsLocation,
            string defaultProjectName,
            List<ProjectInfo> projectInfos)
        {
            this.Presenter = new SolutionOptionsPresenter(
                this, 
                defaultProjectsLocation, 
                defaultProjectName,
                projectInfos);

            this.InitializeComponent();

            this.LoadSettings();
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="SolutionOptionsForm"/> is continue.
        /// </summary>
        /// <value>
        ///   <c>true</c> if continue; otherwise, <c>false</c>.
        /// </value>
        public bool Continue { get; set; }

        /// <summary>
        /// Gets or sets the presenter.
        /// </summary>
        /// <value>
        /// The presenter.
        /// </value>
        public SolutionOptionsPresenter Presenter { get; set; }

        /// <summary>
        /// Gets or sets the path.
        /// </summary>
        public string Path
        {
            get
            {
                string path = this.textBoxPath.Text;

                if (path.EndsWith("\\") == false)
                {
                    path = string.Format("{0}\\", path);
                }   

                return path;
            }

            set
            {
                this.textBoxPath.Text = value;
            }
        }

        /// <summary>
        /// Gets or sets the name of the project.
        /// </summary>
        public string ProjectName
        {
            get { return this.textBoxProject.Text; } 
            set { this.textBoxProject.Text = value; }
        }

        /// <summary>
        /// Sets a value indicating whether [enable project name].
        /// </summary>
        public bool EnableProjectName
        {
            set { this.textBoxProject.ReadOnly = value; }
        }

        /// <summary>
        /// Gets the required projects.
        /// </summary>
        public List<ProjectInfo> RequiredProjects
        {
            get { return this.checkedListBox.CheckedItems.Cast<ProjectInfo>().ToList(); }
        }

        /// <summary>
        /// Adds the template.
        /// </summary>
        /// <param name="projectInfo">The project info.</param>
        public void AddTemplate(ProjectInfo projectInfo)
        {
            this.checkedListBox.Items.Add(projectInfo, true);
        }

        /// <summary>
        /// Handles the TextChanged event of the textBoxProject control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void TextBoxProjectTextChanged(object sender, EventArgs e)
        {
            this.buttonOK.Enabled = this.textBoxProject.Text.Length > 0;
        }
        
        /// <summary>
        /// Handles the Click event of the buttonOK control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ButtonOKClick(object sender, EventArgs e)
        {
            this.SaveSettings();
            this.Continue = true;
        }

        /// <summary>
        /// Handles the Click event of the buttonPath control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ButtonPathClick(object sender, EventArgs e)
        {
            this.folderBrowserDialog1.SelectedPath = this.textBoxPath.Text;

            DialogResult result = this.folderBrowserDialog1.ShowDialog();

            if (result == DialogResult.OK)
            {
                this.textBoxPath.Text = this.folderBrowserDialog1.SelectedPath;
            }
        }

        /// <summary>
        /// Loads the settings.
        /// </summary>
        private void LoadSettings()
        {
            this.Presenter.LoadSettings();
        }

        /// <summary>
        /// Saves the settings.
        /// </summary>
        private void SaveSettings()
        {
            this.Presenter.SaveSettings();
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
        /// Buttons the help click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ButtonHelpClick(object sender, EventArgs e)
        {
        }
    }
}

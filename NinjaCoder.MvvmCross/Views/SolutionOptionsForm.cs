
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
    public partial class SolutionOptionsForm : BaseView, ISolutionOptionsView
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
            List<ProjectTemplateInfo> projectInfos)
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

                if (path.EndsWith(@"\") == false)
                {
                    path = string.Format(@"{0}\", path);
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
        /// <value>
        /// The required projects.
        /// </value>
        public List<ProjectTemplateInfo> RequiredProjects
        {
            get { return this.mvxListView1.RequiredTemplates.Cast<ProjectTemplateInfo>().ToList(); }
        }

        /// <summary>
        /// Adds the template.
        /// </summary>
        /// <param name="projectTemplateInfo">The project template info.</param>
        public void AddTemplate(ProjectTemplateInfo projectTemplateInfo)
        {
            this.mvxListView1.AddTemplate(projectTemplateInfo);
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
            this.Presenter.Load();
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
        /// Handles the KeyDown event of the textBoxProject control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        private void TextBoxProjectKeyDown(object sender, KeyEventArgs e)
        {
            if (this.textBoxProject.Text.Length > 0)
            {
                int start = this.textBoxProject.SelectionStart;

                this.textBoxProject.Text = this.textBoxProject.Text.Substring(0, 1).ToUpper() + this.textBoxProject.Text.Substring(1);
                this.textBoxProject.SelectionStart = start;
            }
        }

        /// <summary>
        /// Solutions the options form load.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void SolutionOptionsFormLoad(object sender, EventArgs e)
        {
            this.textBoxProject.Focus();
            this.textBoxProject.Select();
        }
    }
}

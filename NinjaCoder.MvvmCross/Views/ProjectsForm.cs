// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ProjectsForm type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Views
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Forms;
    using Interfaces;

    using NinjaCoder.MvvmCross.Infrastructure.Services;

    using Presenters;
    using Scorchio.VisualStudio.Entities;

    /// <summary>
    /// Defines the ProjectsForm type.
    /// </summary>
    public partial class ProjectsForm : BaseView, IProjectsView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectsForm" /> class.
        /// </summary>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="defaultProjectsLocation">The default projects location.</param>
        /// <param name="defaultProjectName">Default name of the project.</param>
        /// <param name="projectInfos">The project infos.</param>
        public ProjectsForm(
            ISettingsService settingsService,
            string defaultProjectsLocation,
            string defaultProjectName,
            IEnumerable<ProjectTemplateInfo> projectInfos)
        {
            this.Presenter = new ProjectsPresenter(
                this, 
                settingsService);

            this.InitializeComponent();

            this.mvxListView1.SetBorderVisibility(BorderStyle.None);

            this.Presenter.Load(
                defaultProjectsLocation,
                defaultProjectName,
                projectInfos);
        }

        /// <summary>
        /// Gets or sets the presenter.
        /// </summary>
        /// <value>
        /// The presenter.
        /// </value>
        public ProjectsPresenter Presenter { get; set; }

        /// <summary>
        /// Sets a value indicating whether [display logo].
        /// </summary>
        public bool DisplayLogo 
        { 
            set { this.SetLogoVisibility(this.logo1, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [use nuget].
        /// </summary>
        public bool UseNuget
        {
            get { return this.checkBoxUseNuget.Checked; }
            set { this.checkBoxUseNuget.Checked = value; }
        }

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

            if (this.textBoxProject.Text.Length > 0)
            {
                this.textBoxProject.Text = char.ToUpper(this.textBoxProject.Text[0]) + this.textBoxProject.Text.Substring(1);
            }

            this.textBoxProject.SelectionStart = this.textBoxProject.Text.Length;
        }
        
        /// <summary>
        /// Handles the Click event of the buttonOK control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ButtonOkClick(object sender, EventArgs e)
        {
            if (this.Presenter.GetRequiredTemplates().Any())
            {
                this.Presenter.SaveSettings();
                this.DialogResult = DialogResult.OK;
            }
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
        /// Buttons the cancel click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ButtonCancelClick(object sender, EventArgs e)
        {
            this.Close();
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
            this.ActiveControl = this.textBoxProject;
        }
    }
}

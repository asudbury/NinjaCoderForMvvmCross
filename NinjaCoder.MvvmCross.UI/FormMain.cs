// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the Form1 type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NinjaCoder.MvvmCross.UI
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;

    using EnvDTE;

    using NinjaCoder.MvvmCross.Controllers;

    /// <summary>
    ///    Defines the Form1 type.
    /// </summary>
    public partial class FormMain : Form
    {
        /// <summary>
        /// The MvvmCrossController.
        /// </summary>
        private readonly MvvmCrossController controller;

        /// <summary>
        /// Initializes a new instance of the <see cref="FormMain" /> class.
        /// </summary>
        public FormMain()
        {
            this.InitializeComponent();

            this.controller = new MvvmCrossController();

            IEnumerable<Project> projects = controller.GetProjects();

            foreach (Project project in projects)
            {
                this.treeView1.Nodes.Add(project.Name);
            }
        }

        /// <summary>
        /// Handles the Click event of the addProjectsToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void AddProjectsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            controller.BuildProjects();
        }

        /// <summary>
        /// Handles the Click event of the AddViewModelToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void AddViewModelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            controller.AddViewModelAndViews();
        }

        /// <summary>
        /// Options the tool strip menu item click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void OptionsToolStripMenuItemClick(object sender, EventArgs e)
        {
            controller.ShowOptions();
        }

        /// <summary>
        /// Handles the Click event of the addConvertersToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void AddConvertersToolStripMenuItemClick(object sender, EventArgs e)
        {
            controller.AddConverters();
        }

        /// <summary>
        /// Handles the Click event of the exitToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ExitToolStripMenuItemClick(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Handles the Click event of the stackOverflowToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void StackOverflowToolStripMenuItemClick(object sender, EventArgs e)
        {
            controller.ShowStackOverFlow();
        }

        /// <summary>
        /// JabbR tool strip menu item click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void JabbRToolStripMenuItemClick(object sender, EventArgs e)
        {
            controller.ShowJabbrRoom();
        }

        /// <summary>
        /// Handles the Click event of the gitHubToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void GitHubToolStripMenuItemClick(object sender, EventArgs e)
        {
            controller.ShowGitHub();
        }
    }
}

// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the Form1 type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NinjaCoder.MvvmCross.UI
{
    using System;
    using System.Windows.Forms;

    using NinjaCoder.MvvmCross.Controllers;

    /// <summary>
    ///    Defines the Form1 type.
    /// </summary>
    public partial class FormMain : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FormMain"/> class.
        /// </summary>
        public FormMain()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Handles the Click event of the addProjectsToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void AddProjectsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MvvmCrossController controller = new MvvmCrossController();
            controller.BuildProjects();
        }

        /// <summary>
        /// Handles the Click event of the AddViewModelToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void AddViewModelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MvvmCrossController controller = new MvvmCrossController();
            controller.AddViewModelAndViews();
        }

        /// <summary>
        /// Options the tool strip menu item click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void OptionsToolStripMenuItemClick(object sender, EventArgs e)
        {
            MvvmCrossController controller = new MvvmCrossController();
            controller.ShowOptions();
        }

        /// <summary>
        /// Handles the Click event of the addConvertersToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void AddConvertersToolStripMenuItemClick(object sender, EventArgs e)
        {
            MvvmCrossController controller = new MvvmCrossController();
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
            MvvmCrossController controller = new MvvmCrossController();
            controller.ShowStackOverFlow();
        }

        /// <summary>
        /// JabbR tool strip menu item click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void JabbRToolStripMenuItemClick(object sender, EventArgs e)
        {
            MvvmCrossController controller = new MvvmCrossController();
            controller.ShowJabbrRoom();
        }

        /// <summary>
        /// Handles the Click event of the gitHubToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void GitHubToolStripMenuItemClick(object sender, EventArgs e)
        {
            MvvmCrossController controller = new MvvmCrossController();
            controller.ShowGitHub();
        }
    }
}

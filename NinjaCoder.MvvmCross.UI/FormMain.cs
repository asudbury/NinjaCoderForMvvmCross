// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the Form1 type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.UI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Forms;
    using Controllers;
    using EnvDTE;

    /// <summary>
    ///    Defines the Form1 type.
    /// </summary>
    public partial class FormMain : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FormMain" /> class.
        /// </summary>
        public FormMain()
        {
            this.InitializeComponent();

            NinjaController.RunConfigurationController();

            this.ShowProjects();
        }

        /// <summary>
        /// Shows the projects.
        /// </summary>
        private void ShowProjects()
        {
            IEnumerable<Project> projects = NinjaController.GetProjects();

            projects.ToList().ForEach(x => this.treeView1.Nodes.Add(x.Name));
        }

        /// <summary>
        /// Handles the Click event of the addProjectsToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void AddProjectsToolStripMenuItemClick(
            object sender, 
            EventArgs e)
        {
            NinjaController.RunProjectsController();
            this.ShowProjects();
        }

        /// <summary>
        /// Handles the Click event of the AddViewModelToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void AddViewModelToolStripMenuItemClick(
            object sender, 
            EventArgs e)
        {
            NinjaController.RunViewModelViewsController();
        }

        /// <summary>
        /// Handles the Click event of the addConvertersToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void AddConvertersToolStripMenuItemClick(
            object sender, 
            EventArgs e)
        {
            NinjaController.RunConvertersController();
        }

        /// <summary>
        /// Adds the plugins tool strip menu item click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void AddPluginsToolStripMenuItemClick(
            object sender, 
            EventArgs e)
        {
            NinjaController.RunPluginsController();
        }
        
        /// <summary>
        /// Adds the services tool strip menu item click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void AddServicesToolStripMenuItemClick(
            object sender, 
            EventArgs e)
        {
            NinjaController.RunServicesController();
        }

        /// <summary>
        /// Options the tool strip menu item click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void OptionsToolStripMenuItemClick(
            object sender, 
            EventArgs e)
        {
            NinjaController.ShowOptions();
        }

        /// <summary>
        /// Abouts the tool strip menu item click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void AboutToolStripMenuItemClick(
            object sender, 
            EventArgs e)
        {
            NinjaController.ShowAboutBox();
        }

        /// <summary>
        /// Handles the Click event of the exitToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ExitToolStripMenuItemClick(
            object sender, 
            EventArgs e)
        {
            this.Close();
        }
    }
}

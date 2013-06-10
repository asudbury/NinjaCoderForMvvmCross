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
        /// Initializes a new instance of the <see cref="FormMain" /> class.
        /// </summary>
        public FormMain()
        {
            this.InitializeComponent();
            this.ShowProjects();
        }

        /// <summary>
        /// Shows the projects.
        /// </summary>
        private void ShowProjects()
        {
            ApplicationController controller = new ApplicationController();

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
            ProjectsController controller = new ProjectsController();
            controller.Run(); 
            this.ShowProjects();
        }

        /// <summary>
        /// Handles the Click event of the AddViewModelToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void AddViewModelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewModelViewsController controller = new ViewModelViewsController();
            controller.Run(); 
        }

        /// <summary>
        /// Handles the Click event of the addConvertersToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void AddConvertersToolStripMenuItemClick(object sender, EventArgs e)
        {
            ConvertersController controller = new ConvertersController();
            controller.Run(); 
        }

        /// <summary>
        /// Adds the plugins tool strip menu item click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void AddPluginsToolStripMenuItemClick(object sender, EventArgs e)
        {
            PluginsController controller = new PluginsController();
            controller.Run(); 
        }
        
        /// <summary>
        /// Adds the services tool strip menu item click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void AddServicesToolStripMenuItemClick(object sender, EventArgs e)
        {
            ServicesController controller = new ServicesController();
            controller.Run(); 
        }

        /// <summary>
        /// Options the tool strip menu item click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void OptionsToolStripMenuItemClick(object sender, EventArgs e)
        {
            ApplicationController controller = new ApplicationController();
            controller.ShowOptions();
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
    }
}

// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the PluginsForm type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NinjaCoder.MvvmCross.Views
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Forms;

    using NinjaCoder.MvvmCross.Entities;
    using NinjaCoder.MvvmCross.Presenters;
    using NinjaCoder.MvvmCross.Views.Interfaces;

    /// <summary>
    ///  Defines the PluginsForm type.
    /// </summary>
    public partial class PluginsForm : Form, IPluginsView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PluginsForm" /> class.
        /// </summary>
        /// <param name="viewModelNames">The view model names.</param>
        public PluginsForm(List<string> viewModelNames)
        {
            this.InitializeComponent();

            this.Presenter = new PluginsPresenter(this);
            this.Presenter.Load(viewModelNames);
        }

        /// <summary>
        /// Gets the presenter.
        /// </summary>
        public PluginsPresenter Presenter { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="SolutionOptionsForm"/> is continue.
        /// </summary>
        public bool Continue { get; set; }

        /// <summary>
        /// Gets the implement in view model.
        /// </summary>
        public string ImplementInViewModel { get { return this.comboBoxViewModel.SelectedItem as string; } }

        /// <summary>
        /// Gets the required templates.
        /// </summary>
        public List<Plugin> RequiredPlugins 
        {
            get { return this.mvxListView1.RequiredTemplates.Cast<Plugin>().ToList(); }
        }

        /// <summary>
        /// Adds the plugin.
        /// </summary>
        /// <param name="plugin">The plugin.</param>
        public void AddPlugin(Plugin plugin)
        {
            this.mvxListView1.AddPlugin(plugin);
        }

        /// <summary>
        /// Adds the plugin.
        /// </summary>
        /// <param name="viewModelName">Name of the view model.</param>
        public void AddViewModel(string viewModelName)
        {
            this.comboBoxViewModel.Items.Add(viewModelName);
        }

        /// <summary>
        /// Buttons the OK click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ButtonOKClick(object sender, System.EventArgs e)
        {
            this.Continue = true;
        }
    }
}

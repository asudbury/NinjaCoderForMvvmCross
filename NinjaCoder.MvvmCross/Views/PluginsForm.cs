// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the PluginsForm type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Views
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Forms;

    using Entities;

    using NinjaCoder.MvvmCross.Infrastructure.Services;

    using Presenters;
    using Views.Interfaces;

    /// <summary>
    ///  Defines the PluginsForm type.
    /// </summary>
    public partial class PluginsForm : BaseView, IPluginsView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PluginsForm" /> class.
        /// </summary>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="viewModelNames">The view model names.</param>
        /// <param name="plugins">The plugins.</param>
        public PluginsForm(
            ISettingsService settingsService,
            IEnumerable<string> viewModelNames,
            Plugins plugins)
        {
            this.InitializeComponent();

            this.mvxListViewCore.SetBorderVisibility(BorderStyle.None);
            this.mvxListViewCommunity.SetBorderVisibility(BorderStyle.None);
            this.mvxListViewUser.SetBorderVisibility(BorderStyle.None);

            this.Presenter = new PluginsPresenter(this, settingsService);

            this.Presenter.Load(viewModelNames, plugins.Items);
        }

        /// <summary>
        /// Gets the presenter.
        /// </summary>
        public PluginsPresenter Presenter { get; private set; }

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
        /// Gets the implement in view model.
        /// </summary>
        public string ImplementInViewModel
        {
            get { return this.comboBoxViewModel.SelectedItem as string; }
        }

        /// <summary>
        /// Gets the required templates.
        /// </summary>
        public IEnumerable<Plugin> RequiredPlugins 
        {
            get
            {
                return this.mvxListViewCore.RequiredTemplates.Cast<Plugin>()
                    .Union(this.mvxListViewCommunity.RequiredTemplates.Cast<Plugin>()
                    .Union(this.mvxListViewUser.RequiredTemplates.Cast<Plugin>()));
            }
        }

        /// <summary>
        /// Gets a value indicating whether [include unit tests].
        /// </summary>
        public bool IncludeUnitTests
        {
            get { return this.checkBoxIncludeUnitTests.Checked; }
        }

        /// <summary>
        /// Adds the core plugin.
        /// </summary>
        /// <param name="plugin">The plugin.</param>
        public void AddCorePlugin(Plugin plugin)
        {
            this.mvxListViewCore.AddPlugin(plugin);
        }

        /// <summary>
        /// Adds the community plugin.
        /// </summary>
        /// <param name="plugin">The plugin.</param>
        public void AddCommunityPlugin(Plugin plugin)
        {
            this.mvxListViewCommunity.AddPlugin(plugin);
        }

        /// <summary>
        /// Adds the user plugin.
        /// </summary>
        /// <param name="plugin">The plugin.</param>
        public void AddUserPlugin(Plugin plugin)
        {
            this.mvxListViewUser.AddPlugin(plugin);
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
        private void ButtonOKClick(
            object sender, 
            EventArgs e)
        {
            if (this.RequiredPlugins.Any())
            {
                this.Presenter.SaveSettings();
                this.DialogResult = DialogResult.OK;
            }
        }

        /// <summary>
        /// when the view model selected value changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ComboBoxViewModelSelectedValueChanged(
            object sender, 
            EventArgs e)
        {
            this.checkBoxIncludeUnitTests.Visible = (string)this.comboBoxViewModel.SelectedItem != string.Empty;
        }

        /// <summary>
        /// Links the label display wiki page link clicked.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void LinkLabelDisplayWikiPageLinkClicked(
            object sender, 
            LinkLabelLinkClickedEventArgs e)
        {
            this.Presenter.ShowMvvmCrossPluginsWikiPage();
        }

        /// <summary>
        /// Links the label open user plugins folder link clicked.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void LinkLabelOpenUserPluginsFolderLinkClicked(
            object sender, 
            LinkLabelLinkClickedEventArgs e)
        {
            this.Presenter.OpenUserPluginsFolder();
        }
    }
}

// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ServicesForm type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Views
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Interfaces;
    using Presenters;
    using Scorchio.VisualStudio.Entities;

    /// <summary>
    ///  Defines the ServicesForm type.
    /// </summary>
    public partial class ServicesForm : BaseView, IServicesView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServicesForm" /> class.
        /// </summary>
        /// <param name="viewModelNames">The view model names.</param>
        /// <param name="itemTemplateInfos">The item template infos.</param>
        /// <param name="displayLogo">if set to <c>true</c> [display logo].</param>
        public ServicesForm(
            List<string> viewModelNames,
             List<ItemTemplateInfo> itemTemplateInfos,
             bool displayLogo)
        {
            this.InitializeComponent();

            this.Presenter = new ServicesPresenter(this, itemTemplateInfos, displayLogo);
            this.Presenter.Load(viewModelNames);
        }

        /// <summary>
        /// Gets the presenter.
        /// </summary>
        public ServicesPresenter Presenter { get; private set; }
        
        /// <summary>
        /// Sets a value indicating whether [display logo].
        /// </summary>
        public bool DisplayLogo
        {
            set { this.SetLogoVisibility(this.logo1, value); }
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
        public List<ItemTemplateInfo> RequiredTemplates
        {
            get { return this.mvxListView1.RequiredTemplates.Cast<ItemTemplateInfo>().ToList(); }
        }

        /// <summary>
        /// Gets a value indicating whether [include unit tests].
        /// </summary>
        public bool IncludeUnitTests
        {
            get { return this.checkBoxIncludeUnitTests.Checked; }
        }

        /// <summary>
        /// Adds the service.
        /// </summary>
        /// <param name="itemTemplateInfo">The item template info.</param>
        public void AddTemplate(ItemTemplateInfo itemTemplateInfo)
        {
            this.mvxListView1.AddTemplate(itemTemplateInfo);
        }

        /// <summary>
        /// Adds the viewModel.
        /// </summary>
        /// <param name="viewModelName">Name of the view model.</param>
        public void AddViewModel(string viewModelName)
        {
            this.comboBoxViewModel.Items.Add(viewModelName);
        }

        /// <summary>
        /// Handles the Click event of the buttonOK control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ButtonOKClick(object sender, EventArgs e)
        {
            this.Continue = true;
        }

        /// <summary>
        /// When view model selected value changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ComboBoxViewModelSelectedValueChanged(
            object sender, 
            EventArgs e)
        {
            this.checkBoxIncludeUnitTests.Visible = this.comboBoxViewModel.SelectedItem != string.Empty;
        }
    }
}

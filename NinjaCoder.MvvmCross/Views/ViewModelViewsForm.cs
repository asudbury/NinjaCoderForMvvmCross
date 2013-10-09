// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ViewModelViewsForm type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Views
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Windows.Forms;
    using Interfaces;

    using NinjaCoder.MvvmCross.Services.Interfaces;

    using Presenters;
    using Scorchio.VisualStudio.Entities;

    /// <summary>
    /// Define the ViewModelViewsForm type. 
    /// </summary>
    public partial class ViewModelViewsForm : BaseView, IViewModelViewsView
    {
        /// <summary>
        /// The current view model name.
        /// </summary>
        private string currentViewModelName = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewModelViewsForm" /> class.
        /// </summary>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="itemTemplateInfos">The item template infos.</param>
        /// <param name="viewModelNames">The view model names.</param>
        public ViewModelViewsForm(
            ISettingsService settingsService,
            IEnumerable<ItemTemplateInfo> itemTemplateInfos,
            IEnumerable<string> viewModelNames)
        {
            this.InitializeComponent();

            this.Presenter = new ViewModelViewsPresenter(this, settingsService);
            this.Presenter.Load(itemTemplateInfos, viewModelNames);

            this.textBoxViewModel.Focus();
        }

        /// <summary>
        /// Gets or sets the presenter.
        /// </summary>
        public ViewModelViewsPresenter Presenter { get; set; }

        /// <summary>
        /// Sets a value indicating whether [display logo].
        /// </summary>
        public bool DisplayLogo
        {
            set { this.SetLogoVisibility(this.logo1, value); }
        }

        /// <summary>
        /// Gets or sets the name of the view model.
        /// </summary>
        public string ViewModelName
        {
            get { return this.textBoxViewModel.Text; }
            set { this.textBoxViewModel.Text = value; }
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
        /// Sets a value indicating whether [show view model navigation options].
        /// </summary>
        public bool ShowViewModelNavigationOptions
        {
            set
            {
                this.labelInitViewModel.Visible = value;
                this.comboBoxInitViewModel.Visible = value;

                this.labelNavigateToViewModel.Visible = value;
                this.comboBoxNavigateToViewModel.Visible = value;
            }
        }

        /// <summary>
        /// Gets or sets the view model initiated from.
        /// </summary>
        public string ViewModelInitiatedFrom
        {
            get { return this.comboBoxInitViewModel.SelectedItem as string; }

            set { this.comboBoxInitViewModel.SelectedText = value; }
        }

        /// <summary>
        /// Gets or sets the view model to navigate to.
        /// </summary>
        public string ViewModelToNavigateTo
        {
            get { return this.comboBoxNavigateToViewModel.SelectedItem as string; }

            set { this.comboBoxNavigateToViewModel.SelectedText = value; }
        }

        /// <summary>
        /// Adds the template.
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
            this.comboBoxInitViewModel.Items.Add(viewModelName);
            this.comboBoxNavigateToViewModel.Items.Add(viewModelName);
        }

        /// <summary>
        /// Views the model options view load.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ViewModelOptionsViewLoad(object sender, EventArgs e)
        {
            this.textBoxViewModel.Focus();
            this.textBoxViewModel.SelectionStart = 0;
        }

        /// <summary>
        /// Buttons the OK click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ButtonOkClick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.textBoxViewModel.Text) == false)
            {
                this.DialogResult = DialogResult.OK;
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
        /// Texts the box view model text changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void TextBoxViewModelTextChanged(
            object sender, 
            EventArgs e)
        {
            //// surely a better way of doing this!

            if (this.textBoxViewModel.Text.ToLower() != this.currentViewModelName.ToLower())
            {
                this.textBoxViewModel.Text = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(this.textBoxViewModel.Text);

                //// position the cursor in the correct position.
                if (this.textBoxViewModel.Text.ToLower().Contains("viewmodel"))
                {
                    this.currentViewModelName = this.textBoxViewModel.Text.Replace("viewmodel", "ViewModel");
                    this.textBoxViewModel.Text = this.currentViewModelName;
                    this.textBoxViewModel.SelectionStart = this.textBoxViewModel.Text.Length - "ViewModel".Length;
                }
                else
                {
                    this.textBoxViewModel.SelectionStart = this.textBoxViewModel.Text.Length;
                }
            }
        }
    }
}

// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ViewModelOptionsView type.
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
    /// Define the ViewModelOptionsView type. 
    /// </summary>
    public partial class ViewModelOptionsView : Form, IViewModelOptionsView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ViewModelOptionsView" /> class.
        /// </summary>
        /// <param name="itemTemplateInfos">The item template infos.</param>
        public ViewModelOptionsView(List<ItemTemplateInfo> itemTemplateInfos)
        {
            this.InitializeComponent();

            this.Presenter = new ViewModelOptionsPresenter(this, itemTemplateInfos);
            this.Presenter.Load();
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="SolutionOptionsForm"/> is continue.
        /// </summary>
        /// <value>
        ///   <c>true</c> if continue; otherwise, <c>false</c>.
        /// </value>
        public bool Continue { get; set; }

        /// <summary>
        /// Gets or sets the presenter.
        /// </summary>
        public ViewModelOptionsPresenter Presenter { get; set; }

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
        /// Adds the template.
        /// </summary>
        /// <param name="itemTemplateInfo">The item template info.</param>
        public void AddTemplate(ItemTemplateInfo itemTemplateInfo)
        {
            this.mvxListView1.AddTemplate(itemTemplateInfo);
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
        private void ButtonOKClick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.textBoxViewModel.Text) == false)
            {
                this.Continue = true;
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
        /// Texts the box view model key down.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        private void TextBoxViewModelKeyDown(object sender, KeyEventArgs e)
        {
            if (this.textBoxViewModel.Text.Length > 0)
            {
                int start = this.textBoxViewModel.SelectionStart;

                this.textBoxViewModel.Text = this.textBoxViewModel.Text.Substring(0, 1).ToUpper() + this.textBoxViewModel.Text.Substring(1);
                this.textBoxViewModel.SelectionStart = start;
            }
        }
    }
}

// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ConvertersForm type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NinjaCoder.MvvmCross.Views
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Forms;

    using NinjaCoder.MvvmCross.Presenters;
    using NinjaCoder.MvvmCross.Views.Interfaces;

    using Scorchio.VisualStudio.Entities;

    /// <summary>
    ///    Defines the ConvertersForm type.
    /// </summary>
    public partial class ItemTemplatesForm : Form, IItemTemplatesView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ItemTemplatesForm" /> class.
        /// </summary>
        /// <param name="itemTemplateInfos">The item template infos.</param>
        public ItemTemplatesForm(List<ItemTemplateInfo> itemTemplateInfos)
        {
            this.InitializeComponent();

            this.Presenter = new ItemTemplatesPresenter(this, itemTemplateInfos);
            this.Presenter.Load();
        }

        /// <summary>
        /// Gets the presenter.
        /// </summary>
        public ItemTemplatesPresenter Presenter { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="SolutionOptionsForm"/> is continue.
        /// </summary>
        public bool Continue { get; set; }

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
        /// Buttons the OK click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ButtonOKClick(object sender, System.EventArgs e)
        {
            this.Continue = true;
            this.Close();
        }

        /// <summary>
        /// Handles the Click event of the buttonCancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ButtonCancelClick(object sender, System.EventArgs e)
        {
            this.Close();
        }
    }
}

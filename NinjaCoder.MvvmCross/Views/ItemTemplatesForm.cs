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

    using Interfaces;

    using NinjaCoder.MvvmCross.Services.Interfaces;

    using Presenters;
    using Scorchio.VisualStudio.Entities;

    /// <summary>
    ///    Defines the ConvertersForm type.
    /// </summary>
    public partial class ItemTemplatesForm : BaseView, IItemTemplatesView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ItemTemplatesForm" /> class.
        /// </summary>
        /// <param name="itemTemplateInfos">The item template infos.</param>
        /// <param name="settingsService">The settings service.</param>
        public ItemTemplatesForm(
            IEnumerable<ItemTemplateInfo> itemTemplateInfos,
            ISettingsService settingsService)
        {
            this.InitializeComponent();

            this.Presenter = new ItemTemplatesPresenter(this, settingsService);
            this.Presenter.Load(itemTemplateInfos);
        }

        /// <summary>
        /// Gets the presenter.
        /// </summary>
        public ItemTemplatesPresenter Presenter { get; private set; }
        
        /// <summary>
        /// Sets a value indicating whether [display logo].
        /// </summary>
        public bool DisplayLogo
        {
            set { this.SetLogoVisibility(this.logo1, value); }
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
        /// Buttons the OK click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ButtonOKClick(object sender, System.EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
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

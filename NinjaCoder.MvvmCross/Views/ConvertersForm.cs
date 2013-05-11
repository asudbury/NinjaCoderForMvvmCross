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

    /// <summary>
    ///    Defines the ConvertersForm type.
    /// </summary>
    public partial class ConvertersForm : Form, IConvertersView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConvertersForm" /> class.
        /// </summary>
        /// <param name="templatesPath">The templates path.</param>
        public ConvertersForm(string templatesPath)
        {
            this.InitializeComponent();

            this.Presenter = new ConvertersPresenter(this)
                                 {
                                     TemplatesPath = templatesPath
                                 };

            this.Presenter.LoadTemplates();
        }

        /// <summary>
        /// Gets the presenter.
        /// </summary>
        public ConvertersPresenter Presenter { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="SolutionOptionsForm"/> is continue.
        /// </summary>
        public bool Continue { get; set; }

        /// <summary>
        /// Gets the required converters.
        /// </summary>
        public List<string> RequiredConverters
        {
            get
            {
                return this.checkedListBox.CheckedItems.Cast<string>().ToList();
            }
        }

        /// <summary>
        /// Adds the template.
        /// </summary>
        /// <param name="name">The name.</param>
        public void AddTemplate(string name)
        {
            this.checkedListBox.Items.Add(name);
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

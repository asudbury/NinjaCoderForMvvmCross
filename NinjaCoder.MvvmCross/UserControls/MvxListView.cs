// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the MvxListView type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.UserControls
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Forms;

    using NinjaCoder.MvvmCross.Entities;

    using Scorchio.VisualStudio.Entities;

    /// <summary>
    /// Defines the MvxListView type.
    /// </summary>
    public partial class MvxListView : UserControl, IMvxListView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MvxListView"/> class.
        /// </summary>
        public MvxListView()
        {
            this.InitializeComponent();
            
            this.listView.View = View.List;
            this.listView.CheckBoxes = true;
            this.listView.SmallImageList = this.imageList;
            this.listView.HotTracking = false;
        }

        /// <summary>
        /// Gets the required templates.
        /// </summary>
        public List<object> RequiredTemplates
        {
            get
            {
                return (from ListViewItem checkedItem in this.listView.CheckedItems 
                        select checkedItem.Tag).ToList();
            }
        }

        /// <summary>
        /// Adds the template.
        /// </summary>
        /// <param name="baseTemplateInfo">The base template info.</param>
        public void AddTemplate(BaseTemplateInfo baseTemplateInfo)
        {
            this.listView.Items.Add(new ListViewItem
                                        {
                                            Text = baseTemplateInfo.FriendlyName, 
                                            ImageIndex = 0,
                                            Tag = baseTemplateInfo,
                                            Checked = baseTemplateInfo.PreSelected
                                        });
        }

        /// <summary>
        /// Adds the plugin.
        /// </summary>
        /// <param name="plugin">The plugin.</param>
        public void AddPlugin(Plugin plugin)
        {
            this.listView.Items.Add(new ListViewItem
            {
                Text = plugin.FriendlyName,
                ImageIndex = 0,
                Tag = plugin,
            });
        }
    }
}

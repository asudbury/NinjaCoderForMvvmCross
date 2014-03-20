// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the PickListView type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Scorchio.Infrastructure.Wpf.UserControls
{
    using System.Windows;

    /// <summary>
    /// Interaction logic for PickListView.xaml
    /// </summary>
    public partial class PickListView 
    {
        /// <summary>
        /// The group box title property
        /// </summary>
        public static readonly DependencyProperty GroupBoxTitleProperty =
            DependencyProperty.Register(
            "GroupBoxTitle",
            typeof(string),
            typeof(PickListView),
            new UIPropertyMetadata("value not set"));

        /// <summary>
        /// Initializes a new instance of the <see cref="PickListView"/> class.
        /// </summary>
        public PickListView()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Gets or sets the group box title.
        /// </summary>
        public string GroupBoxTitle
        {
            get { return (string)this.GetValue(GroupBoxTitleProperty); }
            set { this.SetValue(GroupBoxTitleProperty, value); }
        }
    }
}

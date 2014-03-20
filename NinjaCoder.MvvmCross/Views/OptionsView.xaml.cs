// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the OptionsView type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Views
{
    using System.Collections.Generic;

    using MahApps.Metro;
    using Scorchio.Infrastructure.EventArguments;

    /// <summary>
    /// Interaction logic for OptionsView.xaml
    /// </summary>
    public partial class OptionsView 
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OptionsView"/> class.
        /// </summary>
        public OptionsView()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Gets the colors.
        /// </summary>
        public IList<Accent> Colors
        {
            get { return ThemeManager.DefaultAccents; }
        }

        /// <summary>
        /// Themes the changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="ThemeChangedEventArgs"/> instance containing the event data.</param>
        public void ThemeChanged(
            object sender, 
            ThemeChangedEventArgs e)
        {
            ThemeManager.ChangeTheme(this.Resources, e.Accent, e.Theme);
        }
    }
}

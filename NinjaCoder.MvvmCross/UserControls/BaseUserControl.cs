// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the BaseUserControl type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.UserControls
{
    using System.Windows;
    using System.Windows.Controls;
    using Scorchio.Infrastructure.Extensions;

    /// <summary>
    ///  Defines the BaseUserControl type.
    /// </summary>
    public class BaseUserControl : UserControl
    {
        /// <summary>
        /// The resource dictionary property.
        /// </summary>
        public static readonly DependencyProperty LanguageDictionaryProperty =
            DependencyProperty.Register(
            "LanguageDictionary",
            typeof(ResourceDictionary),
            typeof(UserControl),
            new UIPropertyMetadata(OnLanguageDictionaryChanged));

        /// <summary>
        /// Gets or sets the language dictionary.
        /// </summary>
        public ResourceDictionary LanguageDictionary
        {
            get { return (ResourceDictionary)this.GetValue(LanguageDictionaryProperty); }
            set { this.SetValue(LanguageDictionaryProperty, value); }
        }

        /// <summary>
        /// Called when [language dictionary changed].
        /// </summary>
        /// <param name="dependencyObject">The dependency object.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        public static void OnLanguageDictionaryChanged(
            DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs e)
        {
            UserControl userControl = dependencyObject as UserControl;

            if (userControl != null)
            {
                if (e.NewValue != null)
                {
                    ResourceDictionary resourceDictionary = e.NewValue as ResourceDictionary;

                    userControl.Resources.SetLanguageDictionary(resourceDictionary);
                }
            }
        }
    }
}

// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the BrowserBehaviour type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace  $rootnamespace$.Behaviours
{
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    ///  Defines the BrowserBehaviour type.
    /// </summary>
    public class BrowserBehaviour
    {
        /// <summary>
        /// The Url property
        /// </summary>
        public static readonly DependencyProperty UrlProperty = DependencyProperty.RegisterAttached(
                "Url",
                typeof(string),
                typeof(BrowserBehaviour),
                new FrameworkPropertyMetadata(OnUrlChanged));

        /// <summary>
        /// Gets the Url.
        /// </summary>
        /// <param name="webBrowser">The web browser.</param>
        /// <returns>The html in the browser.</returns>
        [AttachedPropertyBrowsableForType(typeof(WebBrowser))]
        public static string GetUrl(WebBrowser webBrowser)
        {
            return (string)webBrowser.GetValue(UrlProperty);
        }

        /// <summary>
        /// Sets the Url.
        /// </summary>
        /// <param name="webBrowser">The web browser.</param>
        /// <param name="value">The value.</param>
        public static void SetUrl(
            WebBrowser webBrowser,
            string value)
        {
            if (string.IsNullOrEmpty(value) == false)
            {
                webBrowser.SetValue(UrlProperty, value);
            }
        }

        /// <summary>
        /// Called when Url changed.
        /// </summary>
        /// <param name="dependencyObject">The dependency object.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        public static void OnUrlChanged(
            DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs e)
        {
            WebBrowser webBrowser = dependencyObject as WebBrowser;

            if (webBrowser != null)
            {
                if (e.NewValue != null)
                {
                    string url = e.NewValue as string;

                    if (url != null)
                    {
                        webBrowser.Navigate(url);
                    }
                }
            }
        }
    }
}
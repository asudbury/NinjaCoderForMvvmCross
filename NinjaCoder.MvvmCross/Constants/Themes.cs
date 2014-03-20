// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the Themes type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Constants
{
    using System;
    using System.Windows;

    /// <summary>
    ///  Defines the Themes type.
    /// </summary>
    internal static class Themes
    {
        /// <summary>
        /// Gets the light resource.
        /// </summary>
        public static ResourceDictionary LightResource
        {
            get
            {
                return new ResourceDictionary
                {
                    Source = new Uri("pack://application:,,,/MahApps.Metro;component/Styles/Accents/BaseLight.xaml")
                };
            }
        }

        /// <summary>
        /// Gets the dark resource.
        /// </summary>
        public static ResourceDictionary DarkResource
        {
            get
            {
                return new ResourceDictionary
                {
                    Source = new Uri("pack://application:,,,/MahApps.Metro;component/Styles/Accents/BaseDark.xaml")
                };
            }
        }
    }
}

// --------------------------------------------------------------------------------------------------------------------
// <summary>
//  Defines the BoolToVisibleOrCollapsedConverter type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Scorchio.Infrastructure.Wpf.Converters
{
    using System;
    using System.Windows;
    using System.Windows.Data;

    /// <summary>
    ///  Defines the BoolToVisibleOrCollapsedConverter type.
    /// </summary>
    public class BoolToVisibleOrCollapsedConverter : IValueConverter
    {
        /// <summary>
        /// Converts a value.
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>
        /// A converted value. If the method returns null, the valid null value is used.
        /// </returns>
        public object Convert(
            object value, 
            Type targetType, 
            object parameter, 
            System.Globalization.CultureInfo culture)
        {
            bool bValue = (bool)value;

            return bValue ? Visibility.Visible : Visibility.Collapsed;
        }

        /// <summary>
        /// Converts a value.
        /// </summary>
        /// <param name="value">The value that is produced by the binding target.</param>
        /// <param name="targetType">The type to convert to.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>
        /// A converted value. If the method returns null, the valid null value is used.
        /// </returns>
        public object ConvertBack(
            object value, 
            Type targetType, 
            object parameter, 
            System.Globalization.CultureInfo culture)
        {
            Visibility visibility = (Visibility)value;

            return visibility == Visibility.Visible;
        }
    }
}

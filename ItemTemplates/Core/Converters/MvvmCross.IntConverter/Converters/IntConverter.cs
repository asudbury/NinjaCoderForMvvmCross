// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the IntConverter type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace MvvmCross.IntConverter.Converters
{
    using System;
    using System.Globalization;

    using Cirrious.CrossCore.Converters;

    /// <summary>
    ///    Defines the Int Converter type.
    /// </summary>
    public class IntConverter : MvxValueConverter
    {
        /// <summary>
        /// Converts the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="targetType">Type of the target.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="culture">The culture.</param>
        /// <returns>The converted value.</returns>
        public override object Convert(
            object value, 
            Type targetType, 
            object parameter, 
            CultureInfo culture)
        {
            var intValue = (int)value;
            return intValue.ToString(CultureInfo.CurrentUICulture);
        }

        /// <summary>
        /// Converts the back.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="targetType">Type of the target.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="culture">The culture.</param>
        /// <returns>The converted value.</returns>
        public override object ConvertBack(
            object value, 
            Type targetType, 
            object parameter, 
            CultureInfo culture)
        {
            var stringValue = (string)value;
            return int.Parse(stringValue, CultureInfo.CurrentUICulture);
        }
    }
}
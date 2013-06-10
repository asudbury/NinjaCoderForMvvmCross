// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the FloatConverter type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace MvvmCross.FloatConverter.Converters
{
    using System;
    using System.Globalization;

    using Cirrious.CrossCore.Converters;

    /// <summary>
    ///    Defines the FloatConverter type.
    /// </summary>
    public class FloatConverter : MvxValueConverter
    {
        /// <summary>
        /// Converts the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="targetType">Type of the target.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="culture">The culture.</param>
        /// <returns>The converted Value.</returns>
        public override object Convert(
            object value, 
            Type targetType, 
            object parameter, 
            CultureInfo culture)
        {
            float floatValue = (float)value;
            return floatValue.ToString(CultureInfo.CurrentUICulture);
        }

        /// <summary>
        /// Converts the back.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="targetType">Type of the target.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="culture">The culture.</param>
        /// <returns>The converted Value.</returns>
        public override object ConvertBack(
            object value, 
            Type targetType, 
            object parameter, 
            CultureInfo culture)
        {
            string stringValue = (string)value;
            return float.Parse(stringValue, CultureInfo.CurrentUICulture);
        }
    }
}
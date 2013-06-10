// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the String Length Value Converter type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace MvvmCross.StringLengthValueConverter.Converters
{
    using System;
    using System.Globalization;

    using Cirrious.CrossCore.Converters;

    /// <summary>
    ///    Defines the String Length Value Converter type.
    /// </summary>
    public class StringLengthValueConverter : MvxValueConverter
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
            var stringValue = value as string;

            return string.IsNullOrEmpty(stringValue) ? 0 : stringValue.Length;
        }
    }
}
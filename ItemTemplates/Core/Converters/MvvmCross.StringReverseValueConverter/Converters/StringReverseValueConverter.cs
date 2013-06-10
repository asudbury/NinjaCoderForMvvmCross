// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the String Reverse Value Converter type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace MvvmCross.StringReverseValueConverter.Converters
{
    using System;
    using System.Globalization;
    using System.Linq;

    using Cirrious.CrossCore.Converters;

    /// <summary>
    ///    Defines the String Reverse Value Converter type.
    /// </summary>
    public class StringReverseValueConverter : MvxValueConverter
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

            if (string.IsNullOrEmpty(stringValue))
            {
                return string.Empty;
            }

            //// note that the ToCharArray is needed in WinRT!
            return new string(stringValue.ToCharArray().Reverse().ToArray());
        }
    }
}
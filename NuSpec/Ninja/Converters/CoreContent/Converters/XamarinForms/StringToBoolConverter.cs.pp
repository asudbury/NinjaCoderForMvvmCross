// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the StringToBoolConverter type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace $rootnamespace$.Converters
{
    using System;
    using System.Globalization;
    using Xamarin.Forms;

    /// <summary>
    ///    Defines the StringToBoolConverter type.
    /// </summary>
    public class StringToBoolConverter : IValueConverter
    {
        /// <summary>
        /// Convert.
        /// </summary>
        /// <param name="value">The boolean value which gets converted</param>
        /// <param name="targetType">Type: Visibility</param>
        /// <param name="parameter">Params, none required</param>
        /// <param name="culture">The CultureInfo</param>
        public object Convert(
            object value, 
            Type targetType, 
            object parameter, 
            CultureInfo culture)
        {
            if (value != null)
            {
                switch (value.ToString().ToLower())
                {
                    case "y":
                        return true;

                    case "n":
                        return false;
                }
            }

            return false;
        }

        /// <summary>
        /// Convert Back.
        /// </summary>
        /// <param name="value">The boolean value which gets converted</param>
        /// <param name="targetType">Type: Visibility</param>
        /// <param name="parameter">Params, none required</param>
        /// <param name="culture">The CultureInfo</param>
        public object ConvertBack(
            object value, 
            Type targetType, 
            object parameter, 
            CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
using System;
using System.Globalization;
using System.Windows.Data;

namespace Phonebook_Application.Converters
{
    internal class DimensionConverter : IValueConverter
    {
        /// <summary>
        /// Convert Function
        /// ----------------
        /// 1. Converts the dimension like height, width to make controls responsive.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns>
        ///     1. returns 75 percentage of height.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double height = (double)value;
            return .75 * height;
        }

        /// <summary>
        /// ConvertBack Function
        /// --------------------
        /// 1. Not implemented.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

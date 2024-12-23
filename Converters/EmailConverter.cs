using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Phonebook_Application.Converters
{
    internal class EmailConverter : IValueConverter
    {
        /// <summary>
        /// Email Convert Function
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns>
        ///     1. returns empty string when value is null from viewmodel.
        ///     2. returns lowercase of value if value is not null from viewmodel.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == null ? string.Empty : (object)value.ToString().ToLower();
        }

        /// <summary>
        /// Email ConvertBack Function
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns>
        ///     1. returns lowecase value to viewmodel.
        /// </returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString().ToLower();
        }
    }
}

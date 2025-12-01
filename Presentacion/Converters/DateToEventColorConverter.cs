using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Presentacion.Converters
{
    public class DateToEventColorConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            // values[0] is the date from the calendar
            // values[1] is the HashSet<DateTime> of dates with events
            
            if (values.Length < 2 || values[0] == null || values[1] == null)
                return Brushes.Transparent;

            if (values[0] is DateTime date && values[1] is HashSet<DateTime> datesWithEvents)
            {
                if (datesWithEvents.Contains(date.Date))
                {
                    // Return a dark blue brush for dates with events
                    return new SolidColorBrush(Color.FromRgb(0, 40, 152)); // #002898
                }
            }

            return Brushes.Transparent;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

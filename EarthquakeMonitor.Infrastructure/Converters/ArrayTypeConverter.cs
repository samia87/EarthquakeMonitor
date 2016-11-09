using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace EarthquakeMonitor.Infrastructure.Converters
{
    public class StringEnumerableTypeConverter : IValueConverter
    {
       
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string retVal = "";
            var ses = value as IEnumerable<string>;
            if(ses != null)
                foreach (var s in ses)
                {
                    retVal += s + ",";
                }
            retVal = retVal.TrimEnd(new char[] {','});
            return retVal;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string list = value as string;
            if (list != null)
                return list.Split(',').ToList();
            return null;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace JungleControls
{
    public class TypeOfConverter : IValueConverter
    {
        public static readonly TypeOfConverter Instance = new TypeOfConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) { return (value == null) ? null : value.GetType(); }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) { throw new NotImplementedException(); }
    }
}

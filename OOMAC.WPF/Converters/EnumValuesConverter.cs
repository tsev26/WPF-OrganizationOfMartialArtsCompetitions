using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using static OOMAC.Domain.Models.Contestant;

namespace OOMAC.WPF.Converters
{
    public class EnumValuesConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is TechnicalSkill)
            {
                return (double)(TechnicalSkill)value;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (double)value;
        }

    }
}

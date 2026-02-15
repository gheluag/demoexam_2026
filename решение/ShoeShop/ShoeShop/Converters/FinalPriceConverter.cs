using ShoeShop.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ShoeShop.Converters
{
    public class FinalPriceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Product product)
            {
                if (product.Sale > 0)
                {
                    decimal finalPrice = product.Price * (100 - product.Sale) / 100;
                    return $"{finalPrice:N0} ₽";
                }
                return $"{product.Price:N0} ₽";
            }
            return "—";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }

}

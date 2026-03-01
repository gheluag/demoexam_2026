using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using System.IO;

namespace ShoeShop.Converters
{
    public class ImagePathConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string? fileName = value as string;

            string imagesFolder = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "Images"
            );

           
            if (string.IsNullOrEmpty(fileName))
                fileName = "picture.png";

            string fullPath = Path.Combine(imagesFolder, fileName);

            if (!File.Exists(fullPath))
                fullPath = Path.Combine(imagesFolder, "picture.png");

            return new BitmapImage(new Uri(fullPath, UriKind.Absolute));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using Xamarin.Forms;

namespace DispensadoresApp.Converters
{
    public class ImageConverter : IValueConverter //Esta clase convierte una url de una imagen en una imagen como tal
    {
        static WebClient Client = new WebClient();
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if (value != null)
                {
                    var byteArray = Client.DownloadData(value.ToString());
                    return ImageSource.FromStream(() => new MemoryStream(byteArray));
                }
                else
                {
                    return ImageSource.FromStream(() => new MemoryStream());
                }
            }
            catch (Exception e)
            {
                return ImageSource.FromStream(() => new MemoryStream());
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }

}

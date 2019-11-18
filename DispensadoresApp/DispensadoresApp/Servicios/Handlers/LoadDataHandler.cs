using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DispensadoresApp.Servicios.Handlers
{
    public class LoadDataHandler
    {
        public async Task LoadData(string key)
        {
            try
            {
                var secureValue = await SecureStorage.GetAsync(key);
                if (secureValue != null)
                {
                    Application.Current.Properties[key] = secureValue;
                }
            }
            catch (Exception)
            {

            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using DispensadoresApp.Droid.Servicios;
using DispensadoresApp.Servicios.BaseDatos;
using Xamarin.Forms;

[assembly: Dependency(typeof(BaseDatosAndroid))]

namespace DispensadoresApp.Droid.Servicios
{
    public class BaseDatosAndroid : IBaseDatos
    {
        public string GetDatabasePath()
        {
            return Path.Combine(
                System.Environment.GetFolderPath(
                    System.Environment.SpecialFolder.Personal),
                DB.NombreBD);
        }
    }
}
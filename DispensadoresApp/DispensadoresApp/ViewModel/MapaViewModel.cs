using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DispensadoresApp.Modelos;
using DispensadoresApp.Servicios;
using DispensadoresApp.ViewModels;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace DispensadoresApp.ViewModels
{
    public class MapaViewModel : ViewModelBase
    {
        private string urlImagen;
        private string path;


        public string UrlImagen
        {
            get {return urlImagen; }
            set {
                urlImagen = value;
                OnPropertyChanged();
            }
        }
        public string Path
        {
            get { return path; }
            set
            {
                path = value;
                OnPropertyChanged();
            }
        }
        public MapaViewModel()
        {


        }

        public override async Task InitializeAsync(object navigationData)
        {
            UrlImagen = Application.Current.Properties["urlImagen"].ToString();
            var idEmpresa = (Application.Current.Properties["idEmpresa"].ToString()); //id de la empresa para enviar a lservicio get

            UrlImagen = GlobalSettings.BASE_URL_IMG_EMPRESA +UrlImagen+".jsf";//el converter transforma esta url en imagen 
            

        }
    }
}

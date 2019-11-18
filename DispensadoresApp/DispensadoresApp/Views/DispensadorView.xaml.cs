using DispensadoresApp.Modelos;
using DispensadoresApp.Models;
using DispensadoresApp.Servicios.Navegacion;
using DispensadoresApp.ViewModels;
using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DispensadoresApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DispensadorView : ContentPage
    {
        DispensadorViewModel context = new DispensadorViewModel();
        public DispensadorView()
        {
            InitializeComponent();
            BindingContext = context;

        }

        /*private  async void OnItemSelect(Object sender, ItemTappedEventArgs e)
        {
            var dispensador = e.Item as DispensadorModelo;

            //await Navigation.PushAsync(new DetalleDispensadorView(dispensador));
            TipoElementoModelo model;
            await Navigation.PushAsync(new ListadoView());
        }*/
    }
}
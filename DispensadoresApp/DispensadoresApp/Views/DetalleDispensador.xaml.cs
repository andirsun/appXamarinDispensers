using DispensadoresApp.Modelos;
using DispensadoresApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DispensadoresApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetalleDispensador : ContentPage
    {
        ElementoModelo elementoSelecionado = new ElementoModelo();
        DispensadorModelo DispensadorSeleccionado = new DispensadorModelo();
      

        public DetalleDispensador(DispensadorModelo dispensador)
        {
            InitializeComponent();
            Nombre.Text = dispensador.Nombre;
            Distancia.Text = dispensador.Distancia.ToString();
            DispensadorSeleccionado = dispensador;
            Lista.ItemsSource = dispensador.Elementos;
            //Lista2.ItemsSource = dispensador.Elementos2;
            //BindingContext = new DispensadorViewModel(dispensador);

        }

        private async void OnItemSelect(Object sender, ItemTappedEventArgs e)
        {
            var elemento = e.Item as ElementoModelo;
            elementoSelecionado = elemento;
            //BindingContext = new DispensadorViewModel(elementoSelecionado, DispensadorSeleccionado);
        }
        async void OnButtonClicked(object sender, EventArgs args)
        { 
            /*if(elementoSelecionado.Codigo_barras != null)
            {
                var paginaActual = Application.Current.MainPage;
                await paginaActual.Navigation.PushAsync(new DetalleDispositivoView(elementoSelecionado, DispensadorSeleccionado));
            }
            else
            {
                await DisplayAlert("Aviso", "Debe seleccionar un dispositivo", "OK");
            }*/
        }

        private void ViewCell_Tapped(object sender, EventArgs e)
        {
            var viewCell = (ViewCell)sender;
            if (viewCell.View != null)
            {
                viewCell.View.BackgroundColor = Color.FromHex("#0CB7F2");
            }
        }
    }
}
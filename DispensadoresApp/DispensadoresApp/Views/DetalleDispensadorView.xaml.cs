using DispensadoresApp.Modelos;
using DispensadoresApp.Servicios;
using DispensadoresApp.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DispensadoresApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetalleDispensadorView : ContentPage
    {
       
        
        DetalleDispensadorViewModel context = new DetalleDispensadorViewModel();

        public DetalleDispensadorView()
        {

            InitializeComponent();
            BindingContext = context;
        }

        /*public ObservableCollection<ElementoModelo> Elementos
        {
            get { return elementos; }
            set
            {
                elementos = value;
                OnPropertyChanged();
            }
        }*/
        /*private async void OnItemSelect(Object sender, ItemTappedEventArgs e)
        {
            var elemento = e.Item as ElementoModelo;
            elementoSelecionado = elemento;
            //BindingContext = new DispensadorViewModel(elementoSelecionado, DispensadorSeleccionado);
        }
        async void OnButtonClicked(object sender, EventArgs args)
        {
            var paginaActual = Application.Current.MainPage;
            await paginaActual.Navigation.PushAsync(new DetalleDispositivoView(elementoSelecionado, DispensadorSeleccionado));
      
        }

        private void ViewCell_Tapped(object sender, EventArgs e)
        {
            var viewCell = (ViewCell)sender;
            if (viewCell.View != null)
            {
                viewCell.View.BackgroundColor = Color.FromHex("#0CB7F2");
            }
        }
        */
        
    }
}
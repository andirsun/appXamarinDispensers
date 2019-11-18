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
    public partial class ListadoView : ContentPage
    {
        public ListadoView(TipoElementoModelo tipoElemento)
        {
            InitializeComponent();
            TipoLista.ItemsSource = tipoElemento.Elementos;
        }
    }
}
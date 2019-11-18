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
    public partial class DetalleDispositivoView : ContentPage
    {
        public DetalleDispositivoView(ElementoModelo elemento, DispensadorModelo dispensador)
        {
            InitializeComponent();
            NombreDispensador.Text = dispensador.Nombre;
            Distancia.Text = dispensador.Distancia.ToString();
            Nombre.Text = elemento.Nombre;
            Descripcion.Text = elemento.Descripcion;
            IconoGrande.Source = elemento.Path_Imagen;
            iconoTipo.Source = elemento.Path_Imagen;
            //Numero.Text = elemento.Numero.ToString();
            //Casillero.Text = elemento.Numero.ToString();
            BindingContext = new DispensadorViewModel();
        }
    }
}
using DispensadoresApp.ViewModels;
using Rg.Plugins.Popup.Pages;
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
    public partial class Reserva : PopupPage
    {
        DispensadorViewModel context = new DispensadorViewModel();
        public Reserva(int minutos)
        {
            InitializeComponent();
            DateTime hora = DateTime.Now;
            Fecha.Text = hora.AddMinutes(minutos).ToString();
            BindingContext = context;
        }
    }
}
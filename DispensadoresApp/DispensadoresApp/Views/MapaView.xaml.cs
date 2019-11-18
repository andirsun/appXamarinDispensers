using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DispensadoresApp.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DispensadoresApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapaView : ContentPage
    {
        MapaViewModel context = new MapaViewModel();
        public MapaView()
        {
            InitializeComponent();
            BindingContext = context;
        }
    }
}
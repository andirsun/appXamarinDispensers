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
    public partial class FailLogin : PopupPage
    {
        DispensadorViewModel context = new DispensadorViewModel();
        public FailLogin()
        {
            InitializeComponent();
            BindingContext = context;
        }
    }
}
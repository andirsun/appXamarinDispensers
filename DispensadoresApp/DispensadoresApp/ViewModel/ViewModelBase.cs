using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DispensadoresApp.ViewModels;
using DispensadoresApp.Servicios.Navegacion;

using System.Threading.Tasks;


namespace DispensadoresApp.ViewModel
{
    public class ViewModelBase : NotificationObject   /////Importar Notifi validate objetc
    {
        public INavigationService NavigationService { get; set; }

        //Constructor
        public ViewModelBase()
        {
            NavigationService = App.NavigationService;
        }

        //Métodos
        public virtual Task InitializeAsync(object navigationData)
        {
            return Task.FromResult(false);
        }

    }
}

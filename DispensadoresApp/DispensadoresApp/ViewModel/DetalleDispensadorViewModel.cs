using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using DispensadoresApp.Modelos;
using DispensadoresApp.Servicios;
using DispensadoresApp.ViewModels;
using DispensadoresApp.Views;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace DispensadoresApp.ViewModels
{
    public class DetalleDispensadorViewModel : ViewModelBase
    {

        
        private ObservableCollection<ElementoModelo> elementos;
        //Constructor
        public DetalleDispensadorViewModel()
        {
            

        }
        //Comands
        public Command DetalleDispositivo { get; set; }

        public override async Task InitializeAsync(object navigationData)
        {
            var idDispensador = navigationData.ToString();
            //Usuario = (Application.Current.Properties["UserName"].ToString());
            await ubicacionesCasilleros(1);
        }






        //Intialize Commands
        public void InicializarCommands()
        {
            DetalleDispositivo = new Command(async () => await goDetalleDispositivo(), () => true);
           
        }

        #region Getters y setters
        public ObservableCollection<ElementoModelo> Elementos
        {
            get { return elementos; }
            set
            {
                elementos = value;
                OnPropertyChanged();
            }
        }

        #endregion









        private async Task goDetalleDispositivo()
        {
            await NavigationService.NavigateToAsync<DetalleDispositivoViewModel>();
        
        }

        private async Task ubicacionesCasilleros(int idDispensador)
        {
            
            var url = GlobalSettings.BASE_URL + GlobalSettings.ELEMENTOS + idDispensador+"/1"; //Bug del servicio, no esta validando por usuario solo por casillero , por eso le queme un numero
            var ubicacion = new ElementoModelo();
            var getUbicaciones = new GetService<ElementoModelo>(url);
            Tuple<bool, string> ans = await getUbicaciones.SendRequest(ubicacion);
            if (ans.Item1)
            {
                List<ElementoModelo> listadoElementos= JsonConvert.DeserializeObject<List<ElementoModelo>>(ans.Item2);
                Elementos = new ObservableCollection<ElementoModelo>(listadoElementos);
            }
            

        }
    }
}

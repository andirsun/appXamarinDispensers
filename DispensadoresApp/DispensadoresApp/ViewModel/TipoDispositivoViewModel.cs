using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using DispensadoresApp.Modelos;
using DispensadoresApp.Servicios;
using DispensadoresApp.ViewModels;
using Newtonsoft.Json;
using Plugin.Geolocator;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DispensadoresApp.ViewModels
{
    class TipoDispositivoViewModel : ViewModelBase
    {

        private ObservableCollection<TipoElementoModelo> dispositivosDisponibles;

        public TipoDispositivoViewModel()
        {
            InicializarCommands();
        }
        public override async Task InitializeAsync(object navigationData)
        {
            var usuario = (Application.Current.Properties["UserName"].ToString());
            await EquipoDisponibles();
        }


        #region Commands
        public Command CommandListadoView { get; set; }
        
        #endregion
        public void InicializarCommands()
        {


            CommandListadoView = new Command<TipoElementoModelo>(async (item) => await goListadoView(item), (item) => true); //commanda cuando hay que enviar data
           
        }

        #region Getters y setter
        public ObservableCollection<TipoElementoModelo> DispositivosDisponibles
        {
            get { return dispositivosDisponibles; }
            set
            {
                dispositivosDisponibles = value;
                OnPropertyChanged();
            }
        }
        #endregion
        private async Task EquipoDisponibles()
        {
            var idUsuario= Application.Current.Properties["UserID"] as string;
            var parameters = GlobalSettings.BASE_URL+ GlobalSettings.TIPO_EQUIPO_DISPONIBLE_USUARIO + idUsuario;
            var tipoEquipo = new TipoElementoModelo();
            var servicioGetTipoEquipo = new GetService<TipoElementoModelo>(parameters);
            Tuple<bool, string> ans = await servicioGetTipoEquipo.SendRequest(tipoEquipo);
            if (ans.Item1)
            {
                List<TipoElementoModelo> dispositivos = JsonConvert.DeserializeObject<List<TipoElementoModelo>>(ans.Item2);
               
                for (int i = 0; i < dispositivos.Count; i++)
                {
                    var path = dispositivos[i].PathImagen;
                    dispositivos[i].PathImagen = GlobalSettings.BASE_URL_IMG+path+".jsf";
                    
                }
                DispositivosDisponibles = new ObservableCollection<TipoElementoModelo>(dispositivos);

            }
        }
        private async Task goListadoView(TipoElementoModelo item)
        {
            await NavigationService.NavigateToAsync<ListadoViewModel>(item);
        }
    }
}

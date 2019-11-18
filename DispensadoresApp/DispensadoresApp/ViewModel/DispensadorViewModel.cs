using DispensadoresApp.Modelos;
using DispensadoresApp.Models;
using DispensadoresApp.Servicios;
using DispensadoresApp.Servicios.ApiRest;
using DispensadoresApp.Servicios.BaseDatos;
using DispensadoresApp.Servicios.Geolocalizacion;
using DispensadoresApp.Views;
using Newtonsoft.Json;
using Plugin.Geolocator;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DispensadoresApp.ViewModels
{
    public class DispensadorViewModel : ViewModelBase
    {
        #region Atributos 
        private string usuario;
        //private string nombreDispensador;
        //private string numEquipos;
        private int numeroTotalEquipos; //el numero de equipos totales que hay en todos los casilleros
        private EmpresaModelo empresa;
        private ObservableCollection<DispensadorModelo> dispensadores;
        private List<TipoElementoModelo> tipo_Elemento;
        private List<ElementoModelo> elementos = new List<ElementoModelo>();
        private string nombreElemento;
        private List<ElementoModelo> tiposEquipos = new List<ElementoModelo>();
        public DispensadorModelo DispensadorActual { get; set; }

    
        #endregion

        #region Getters/Setters
        
        public int NumeroTotalEquipos 
        {
            get { return numeroTotalEquipos; }
            set
            {
                numeroTotalEquipos = value;
                OnPropertyChanged();
            }
        }
        public string Usuario
        {
            get { return usuario; }
            set { usuario = value;
                OnPropertyChanged();
            }
        }
        public EmpresaModelo Empresa
        {
            get { return empresa; }
            set { empresa = value; }
        }
        public ObservableCollection<DispensadorModelo> Dispensadores
        {
            get { return dispensadores; }
            set
            {
                dispensadores = value;
                OnPropertyChanged();
            }
        }
       
        public string NombreElemento
        {
            get
            {
                return nombreElemento;
            }
            set
            {
                nombreElemento = value; OnPropertyChanged();
            }
        }
        public List<TipoElementoModelo> Tipo_Elemento
        {
            get { return tipo_Elemento; }
            set { tipo_Elemento = value; OnPropertyChanged();}
        }
        public List<ElementoModelo> Elementos
        {
            get { return elementos; }
            set
            {
                elementos = value;
                OnPropertyChanged();
            }
        }
        public List<ElementoModelo> TiposEquipos
        {
            get { return tiposEquipos; }
            set
            {
                tiposEquipos = value;
                OnPropertyChanged();
            }
        }
        #endregion
        #region Constructores
        public DispensadorViewModel()
        {
            InicializarCommands();
        }
        public override async Task InitializeAsync(object navigationData)
        {
            Usuario = (Application.Current.Properties["UserName"].ToString());
            await listarDispensadores();
        }
        #endregion

        #region Inicializar Commands
        public void InicializarCommands()
        {
           
            //BusquedaTipoEquipoCommand = new Command(async () => await BusquedaTipoEquipo(), () => true);
            DispositivosCommandView = new Command(async () => await DispositivosPage(), () => true);
            DispensadorCommandView = new Command(async () => await DetalleDispensadorPage(), () => true);
            MapaCommandView = new Command(async () => await MapaPage(), () => true);
            PopUpCommandView = new Command(async () => await PopUpView(), () => true);
            ReservarCommand =  new Command(async () => await Reservar(), () => true);
        }      
        #endregion

        #region Commands
        public Command BusquedaTipoEquipoCommand { get; set; }
        public Command DispositivosCommandView { get; set; }
        public Command MapaCommandView { get; set; } 

        public Command DetalleDispositivoCommandView { get; set; }
        public Command DispensadorCommandView { get; set; }

        public Command  PopUpCommandView { get; set; }

        public Command ReservarCommand { get; set; }
        #endregion


        private async Task listarDispensadores()
        {
            var idEmpresa = Application.Current.Properties["idEmpresa"];
            var idUsuario = Application.Current.Properties["UserID"];
            var url = GlobalSettings.BASE_URL + GlobalSettings.DISPENSADORES_URL + idEmpresa +'/'+ idUsuario;
            var dispensador = new DispensadorModelo();
            var getDispensador = new GetService<DispensadorModelo>(url);
            Tuple<bool, string> ans = await getDispensador.SendRequest(dispensador);
            if (ans.Item1)
            {
                List<DispensadorModelo> listaDispensadores = JsonConvert.DeserializeObject<List<DispensadorModelo>>(ans.Item2);
                try
                {
                    var position = await Geolocation.GetLastKnownLocationAsync();//gt current location

                    if (position != null)
                    {
                        var locator = CrossGeolocator.Current;
                        locator.DesiredAccuracy = 50;
                        //var position = await locator.GetPositionAsync();
                        //var position = await Geolocation.GetLastKnownLocationAsync();
                        Location locacionUsuario = new Location(position.Latitude, position.Longitude);
                        var numDispensadores = listaDispensadores.Count;
                        NumeroTotalEquipos = 0;
                        for (int i = 0; i < numDispensadores; i++)
                        {
                            Location dispensadorLocacion = new Location(listaDispensadores[i].Latitud, listaDispensadores[i].Longitud);
                            listaDispensadores[i].Distancia = Math.Round(Location.CalculateDistance(dispensadorLocacion, locacionUsuario, DistanceUnits.Kilometers), 1);
                            NumeroTotalEquipos = NumeroTotalEquipos + listaDispensadores[i].NumEquipos;
                        }
                        listaDispensadores = listaDispensadores.OrderBy(item => item.Distancia).ToList<DispensadorModelo>();
                    }
                }
                catch (FeatureNotSupportedException fnsEx)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Tu telefono no tiene gps", "OK");
                }
                catch (FeatureNotEnabledException fneEx)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "No tienes encendido el gps", "OK");
                }
                catch (PermissionException pEx)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Debes conceder los permisos para acceder a tu ubicacion", "OK");
                }
                catch (Exception ex)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "No pudimos detectar tu ubicacion", "OK");
                }
                Dispensadores = new ObservableCollection<DispensadorModelo>(listaDispensadores);//displego la lista de dispensadores en la vista
            }

        }

        /*
        public async Task<DispensadorModelo> Dispensador(DispensadorModelo dispensador)
        {
            Elementos = await servicio_Dispensador.Lista_Elementos(dispensador);
            for (int j = 0; j < 8; j++)
            {
                Elementos1.Add(new ElementoModelo("descripción1", "código de barras", "serial", "nombre"));
            }
            for (int j = 0; j < 7; j++)
            {
                Elementos2.Add(new ElementoModelo("descripción2", "código de barras", "serial", "nombre"));
            }
            for (int i = 0; i < Elementos.Count; i++)
            {
                if (i < 8)
                {
                    Elementos1[i] = Elementos[i];
                }

                else
                {
                    Elementos2[i] = Elementos[i];
                }
            }


            for (int i = 0; i < Elementos1.Count; i++)
            {
                Elementos1[i].Numero = i + 1;
                if (i % 2 == 0)
                {
                    Elementos1[i].ColorFondo = Color.Beige;

                }
                else
                {
                    Elementos1[i].ColorFondo = Color.FromHex("#f0ac00");
                }
            }

            for (int i = 0; i < Elementos2.Count; i++)
            {
                Elementos2[i].Numero = Elementos1.Count + i + 1;
                if (i % 2 == 0)
                {
                    Elementos2[i].ColorFondo = Color.Beige;
                }
                else
                {
                    Elementos2[i].ColorFondo = Color.FromHex("#f0ac00");
                }
            }
            dispensador.Elementos = Elementos1;
            dispensador.Elementos2 = Elementos2;
            dispensador.Capacidad = Elementos.Count;
            return dispensador;
        }

        public async Task ElementosPorDispensador(DispensadorModelo dispensador)
        {
            Elementos = await servicio_Dispensador.Lista_Elementos(dispensador);
            for (int j = 0; j < 8; j++)
            {
                Elementos1.Add(new ElementoModelo("descripción1", "código de barras", "serial", "nombre"));
            }
            for (int j = 0; j < 7; j++)
            {
                Elementos2.Add(new ElementoModelo("descripción2", "código de barras", "serial", "nombre"));
            }
            for (int i = 0; i < Elementos.Count; i++)
            {
                if (i < 8)
                {
                    Elementos1[i] = Elementos[i];
                }

                else
                {
                    Elementos2[i] = Elementos[i];
                }
            }

            
            for (int i = 0; i < Elementos1.Count; i++)
            {
                Elementos1[i].Numero = i + 1;
                if (i % 2 == 0)
                {
                    Elementos1[i].ColorFondo = Color.Beige;
                    
                }
                else
                {
                    Elementos1[i].ColorFondo = Color.FromHex("#f0ac00");
                }                
            }

            for (int i = 0; i < Elementos2.Count; i++)
            {
                Elementos2[i].Numero = Elementos1.Count + i+1;
                if (i % 2 == 0)
                {
                    Elementos2[i].ColorFondo = Color.Beige;
                }
                else
                {
                    Elementos2[i].ColorFondo = Color.FromHex("#f0ac00");
                }
            }
        }
        */
      

        private async Task DispositivosPage()
        {
            await NavigationService.NavigateToAsync<TipoDispositivoViewModel>();
        }
        private async Task DetalleDispensadorPage()
        {
            await NavigationService.NavigateToAsync<DetalleDispensadorViewModel>(1);
        }
        private async Task MapaPage()
        {
            await NavigationService.NavigateToAsync<MapaViewModel>();
        }
        private async Task PopUpView()
        {
            await PopupNavigation.PushAsync(new Reserva(10));
        }
        private async Task Reservar()
        {
            await PopupNavigation.Instance.PopAsync();
        }
    }
}

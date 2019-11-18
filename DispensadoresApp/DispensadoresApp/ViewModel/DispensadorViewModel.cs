using DispensadoresApp.Modelos;
using DispensadoresApp.Models;
using DispensadoresApp.Servicios.ApiRest;
using DispensadoresApp.Servicios.BaseDatos;
using DispensadoresApp.Views;
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

namespace DispensadoresApp.ViewModel
{
    public class DispensadorViewModel : ViewModelBase
    {
        #region Atributos 
        private UsuarioModelo usuario;
        private EmpresaModelo empresa;
        private List<DispensadorModelo> dispensadores = new List<DispensadorModelo>();
        private List<TipoElementoModelo> tipo_Elemento;
        private List<ElementoModelo> elementos = new List<ElementoModelo>();
        private string nombreElemento;
        private List<ElementoModelo> tiposEquipos = new List<ElementoModelo>();
        private readonly UsuarioDB servicio_BD_Cliente;
        
        public DispensadorModelo DispensadorActual { get; set; }

    
        #endregion

        #region Getters/Setters
        public UsuarioModelo Usuario
        {
            get { return usuario; }
            set { usuario = value; }
        }

        public EmpresaModelo Empresa
        {
            get { return empresa; }
            set { empresa = value; }
        }

        public List<DispensadorModelo> Dispensadores
        {
            get
            {

                return dispensadores;
            }
            set { dispensadores = value; OnPropertyChanged(); }
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

        #region Servicios
        private readonly DispensadorApiRest servicio_Dispensador;
        #endregion

        #region Constructor
        public DispensadorViewModel()
        {
            servicio_Dispensador = new DispensadorApiRest();
            servicio_BD_Cliente = new UsuarioDB();
            BusquedaTipoEquipo();
            InicializarCommands();
        }

        public DispensadorViewModel(DispensadorModelo dispensador)
        {
            servicio_Dispensador = new DispensadorApiRest();
            servicio_BD_Cliente = new UsuarioDB();
            DispensadorActual = dispensador;
            inicio();
            //ElementosPorDispensador(dispensador);
            InicializarCommands();
        }

        public DispensadorViewModel(ElementoModelo elemento, DispensadorModelo dispensador)
        {
            servicio_Dispensador = new DispensadorApiRest();
            servicio_BD_Cliente = new UsuarioDB();
            NombreElemento = elemento.Nombre;
        }


        public DispensadorViewModel(UsuarioModelo usuario)
        {
            servicio_Dispensador = new DispensadorApiRest();
            servicio_BD_Cliente = new UsuarioDB();
            inicio();           
            
            InicializarCommands();
        }

        #endregion

        #region Inicializar Commands
        public void InicializarCommands()
        {
           
            //BusquedaTipoEquipoCommand = new Command(async () => await BusquedaTipoEquipo(), () => true);
            DispositivosCommandView = new Command(async () => await DispositivosPage(), () => true);
            MapaCommandView = new Command(async () => await MapaPage(), () => true);
            PopUpCommandView = new Command(async () => await PopUpView(), () => true);
            ReservarCommand =  new Command(async () => await Reservar(), () => true);
        }

        public async Task inicio()
        {
            usuario = servicio_BD_Cliente.BuscarUsuarioActual();
            
            Dispensadores = await servicio_Dispensador.Lista_Dispensadores(usuario);

            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 50;
            var position = await locator.GetPositionAsync();
            Location locacionUsuario = new Location(position.Latitude, position.Longitude);
            for (int i = 0; i < Dispensadores.Count; i++)
            {
                Location dispensadorLocacion = new Location(Dispensadores[i].Latitud, Dispensadores[i].Longitud);
                Dispensadores[i].Distancia = Math.Round(Location.CalculateDistance(dispensadorLocacion, locacionUsuario, DistanceUnits.Kilometers), 1);
            }
            Dispensadores = Dispensadores.OrderBy(item => item.Distancia).ToList<DispensadorModelo>();

            /*for (int i = 0; i < Dispensadores.Count; i++)
            {
                Dispensadores[i] = await Dispensador(Dispensadores[i]);
            }       */ 
        }

                
        #endregion

        #region Commands
        public Command BusquedaTipoEquipoCommand { get; set; }
        public Command DispositivosCommandView { get; set; }
        public Command MapaCommandView { get; set; } 

        public Command DetalleDispositivoCommandView { get; set; }

        public Command  PopUpCommandView { get; set; }

        public Command ReservarCommand { get; set; }

        public Command CancelarCommand { get; set; }
        #endregion

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
        public async Task BusquedaTipoEquipo()
        {
            usuario = servicio_BD_Cliente.BuscarUsuarioActual();
            Tipo_Elemento = await servicio_Dispensador.Lista_Tipo_Elemento(usuario);
            for (int i = 0; i < Tipo_Elemento.Count; i++)
            {
                Tipo_Elemento[i].Elementos = await servicio_Dispensador.Lista_Equipos_Tipo_Equipo(Tipo_Elemento[i]);
                Tipo_Elemento[i].TotalDisponibles = Tipo_Elemento[i].Elementos.Count;
            }
        }

        private async Task DispositivosPage()
        {
            var paginaActual = Application.Current.MainPage;

            await paginaActual.Navigation.PushAsync(new BusquedaView());
        }


        private async Task MapaPage()
        {
            var paginaActual = Application.Current.MainPage;
            await paginaActual.Navigation.PushAsync(new MapaView());
        }


        private async Task PopUpView()
        {

            await PopupNavigation.PushAsync(new Reserva(10));
        }

        private async Task Reservar()
        {
            await PopupNavigation.Instance.PopAsync();
        }
        public override async Task InitializeAsync(object navigationData)
        {
            
        }
    }
}
